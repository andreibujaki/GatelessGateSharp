#include "sha256.cl"
#include "wolf-sha512.cl"
#include "ripemd160.cl"

//#define SWAP32(x)		as_uint(as_uchar4(x).s3210)
//#define SWAP64(x)		as_ulong(as_uchar8(x).s76543210)

#define SWAP64(x)	(((x) >> 56) | (((x) >> 40) & 0x000000000000FF00UL) | (((x) >> 24) & 0x0000000000FF0000UL) \
          | (((x) >>  8) & 0x00000000FF000000UL) | (((x) <<  8) & 0x000000FF00000000UL) \
          | (((x) << 24) & 0x0000FF0000000000UL) | (((x) << 40) & 0x00FF000000000000UL) | (((x) << 56) & 0xFF00000000000000UL))

#define SWAP32(x)	((((x) >> 24) & 0xFFU) | (((x) >> 8) & 0xFF00U) | (((x) << 8) & 0xFF0000U) | (((x) << 24) & 0xFF000000U))



__attribute__((reqd_work_group_size(WORKSIZE, 1, 1)))
__kernel void search_combined(__global const uint *lbry_input, __global uint *lbry_output, uint lbry_start_nonce, ulong lbry_target)
{
	// SHA256 takes 16 uints of lbry_input per block - we have 112 bytes to process
	// 8 * 16 == 64, meaning two block transforms.

	uint SHA256Buf[16];
	uint gid = get_global_id(0) + lbry_start_nonce;

	// Remember the last four is the nonce - so 108 bytes / 4 bytes per dword
#pragma unroll
	for (int i = 0; i < 16; ++i) SHA256Buf[i] = lbry_input[i];



	// SHA256 initialization constants
	uint8 outbuf = (uint8)(0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A, 0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19);

#pragma unroll
	for (int i = 0; i < 3; ++i)
	{
		if (i == 1)
		{
#pragma unroll
			for (int i = 0; i < 11; ++i) SHA256Buf[i] = lbry_input[i + 16];
			SHA256Buf[11] = SWAP32(gid);
			SHA256Buf[12] = 0x80000000;
			SHA256Buf[13] = 0x00000000;
			SHA256Buf[14] = 0x00000000;
			SHA256Buf[15] = 0x00000380;
		}
		if (i == 2)
		{
			((uint8 *)SHA256Buf)[0] = outbuf;
			SHA256Buf[8] = 0x80000000;
#pragma unroll
			for (int i = 9; i < 15; ++i) SHA256Buf[i] = 0x00000000;
			SHA256Buf[15] = 0x00000100;
			outbuf = (uint8)(0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A, 0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19);
		}
		outbuf = sha256_round(((uint16 *)SHA256Buf)[0], outbuf);
	}

	outbuf.s0 = SWAP32(outbuf.s0);
	outbuf.s1 = SWAP32(outbuf.s1);
	outbuf.s2 = SWAP32(outbuf.s2);
	outbuf.s3 = SWAP32(outbuf.s3);
	outbuf.s4 = SWAP32(outbuf.s4);
	outbuf.s5 = SWAP32(outbuf.s5);
	outbuf.s6 = SWAP32(outbuf.s6);
	outbuf.s7 = SWAP32(outbuf.s7);



	ulong W[16] = { 0UL }, SHA512Out[8];
	//uint SHA256Buf[16];

	((uint8 *)W)[0] = outbuf;

	for (int i = 0; i < 4; ++i) W[i] = SWAP64(W[i]);

	W[4] = 0x8000000000000000UL;
	W[15] = 0x0000000000000100UL;

	for (int i = 0; i < 8; ++i) SHA512Out[i] = SHA512_INIT[i];

	SHA512Block(W, SHA512Out);

	for (int i = 0; i < 8; ++i) SHA512Out[i] = SWAP64(SHA512Out[i]);

	uint RMD160_0[16] = { 0U };
	uint RMD160_1[16] = { 0U };
	uint RMD160_0_Out[5], RMD160_1_Out[5];

	for (int i = 0; i < 4; ++i)
	{
		((ulong *)RMD160_0)[i] = SHA512Out[i];
		((ulong *)RMD160_1)[i] = SHA512Out[i + 4];
	}

	RMD160_0[8] = RMD160_1[8] = 0x00000080;
	RMD160_0[14] = RMD160_1[14] = 0x00000100;

	for (int i = 0; i < 5; ++i)
	{
		RMD160_0_Out[i] = RMD160_IV[i];
		RMD160_1_Out[i] = RMD160_IV[i];
	}

	RIPEMD160_ROUND_BODY(RMD160_0, RMD160_0_Out);
	RIPEMD160_ROUND_BODY(RMD160_1, RMD160_1_Out);

	for (int i = 0; i < 5; ++i) SHA256Buf[i] = SWAP32(RMD160_0_Out[i]);
	for (int i = 5; i < 10; ++i) SHA256Buf[i] = SWAP32(RMD160_1_Out[i - 5]);
	SHA256Buf[10] = 0x80000000;

	for (int i = 11; i < 15; ++i) SHA256Buf[i] = 0x00000000U;

	SHA256Buf[15] = 0x00000140;

	outbuf = (uint8)(0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A, 0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19);
	outbuf = sha256_round(((uint16 *)SHA256Buf)[0], outbuf);



	//uint SHA256Buf[16] = { 0U };
	//uint gid = get_global_id(0);
	//uint8 outbuf = ctx[get_global_id(0) - get_global_offset(0)];

	((uint8 *)SHA256Buf)[0] = outbuf;
	SHA256Buf[8] = 0x80000000;
	for (int i = 9; i < 15; ++i) SHA256Buf[i] = 0x00000000;
	SHA256Buf[15] = 0x00000100;

	outbuf = (uint8)(0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A, 0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19);
	outbuf = sha256_round(((uint16 *)SHA256Buf)[0], outbuf);

	outbuf.s6 = SWAP32(outbuf.s6);
	outbuf.s7 = SWAP32(outbuf.s7);

	//if(!(outbuf.s7)) lbry_output[atomic_inc(lbry_output+0xFF)] = SWAP32(gid);	
	if ((((ulong)outbuf.s7 << 32) | outbuf.s6) <= lbry_target) {
		int i = atomic_inc(lbry_output + 0xFF);
		lbry_output[i] = gid;
		lbry_output[256 + i * 8 + 0] = SWAP32(outbuf.s0);
		lbry_output[256 + i * 8 + 1] = SWAP32(outbuf.s1);
		lbry_output[256 + i * 8 + 2] = SWAP32(outbuf.s2);
		lbry_output[256 + i * 8 + 3] = SWAP32(outbuf.s3);
		lbry_output[256 + i * 8 + 4] = SWAP32(outbuf.s4);
		lbry_output[256 + i * 8 + 5] = SWAP32(outbuf.s5);
		lbry_output[256 + i * 8 + 6] = outbuf.s6;
		lbry_output[256 + i * 8 + 7] = outbuf.s7;
	}
}
