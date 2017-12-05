	.text
	.hsa_code_object_version 2,1
	.hsa_code_object_isa 8,0,3,"AMD","AMDGPU"
	.globl	amd_bitalign            ; -- Begin function amd_bitalign
	.p2align	2
	.type	amd_bitalign,@function
amd_bitalign:                           ; @amd_bitalign
; BB#0:                                 ; %entry
	s_waitcnt vmcnt(0) expcnt(0) lgkmcnt(0)
	;;#ASMSTART
	v_alignbit_b32 v0, v0, v2, v4
v_alignbit_b32 v1, v1, v3, v5
	;;#ASMEND
	s_setpc_b64 s[30:31]
.Lfunc_end0:
	.size	amd_bitalign, .Lfunc_end0-amd_bitalign
                                        ; -- End function
	.section	.AMDGPU.csdata
; Function info:
; codeLenInByte = 24
; NumSgprs: 32
; NumVgprs: 6
; ScratchSize: 0
	.text
	.globl	search                  ; -- Begin function search
	.p2align	8
	.type	search,@function
	.amdgpu_hsa_kernel search
search:                                 ; @search
	.amd_kernel_code_t
		amd_code_version_major = 1
		amd_code_version_minor = 1
		amd_machine_kind = 1
		amd_machine_version_major = 8
		amd_machine_version_minor = 0
		amd_machine_version_stepping = 3
		kernel_code_entry_byte_offset = 256
		kernel_code_prefetch_byte_size = 0
		max_scratch_backing_memory_byte_size = 0
		granulated_workitem_vgpr_count = 18
		granulated_wavefront_sgpr_count = 2
		priority = 0
		float_mode = 192
		priv = 0
		enable_dx10_clamp = 1
		debug_mode = 0
		enable_ieee_mode = 1
		enable_sgpr_private_segment_wave_byte_offset = 0
		user_sgpr_count = 8
		enable_trap_handler = 1
		enable_sgpr_workgroup_id_x = 1
		enable_sgpr_workgroup_id_y = 0
		enable_sgpr_workgroup_id_z = 0
		enable_sgpr_workgroup_info = 0
		enable_vgpr_workitem_id = 0
		enable_exception_msb = 0
		granulated_lds_size = 0
		enable_exception = 0
		enable_sgpr_private_segment_buffer = 1
		enable_sgpr_dispatch_ptr = 1
		enable_sgpr_queue_ptr = 0
		enable_sgpr_kernarg_segment_ptr = 1
		enable_sgpr_dispatch_id = 0
		enable_sgpr_flat_scratch_init = 0
		enable_sgpr_private_segment_size = 0
		enable_sgpr_grid_workgroup_count_x = 0
		enable_sgpr_grid_workgroup_count_y = 0
		enable_sgpr_grid_workgroup_count_z = 0
		enable_ordered_append_gds = 0
		private_element_size = 1
		is_ptr64 = 1
		is_dynamic_callstack = 0
		is_debug_enabled = 0
		is_xnack_enabled = 0
		workitem_private_segment_byte_size = 0
		workgroup_group_segment_byte_size = 5120
		gds_segment_byte_size = 0
		kernarg_segment_byte_size = 88
		workgroup_fbarrier_count = 0
		wavefront_sgpr_count = 24
		workitem_vgpr_count = 74
		reserved_vgpr_first = 0
		reserved_vgpr_count = 0
		reserved_sgpr_first = 0
		reserved_sgpr_count = 0
		debug_wavefront_private_segment_offset_sgpr = 0
		debug_private_segment_buffer_sgpr = 0
		kernarg_segment_alignment = 4
		group_segment_alignment = 4
		private_segment_alignment = 4
		wavefront_size = 6
		call_convention = -1
		runtime_loader_kernel_symbol = 0
	.end_amd_kernel_code_t
; BB#0:                                 ; %entry
	s_load_dwordx2 s[14:15], s[6:7], 0x10
	s_load_dword s9, s[6:7], 0x18
	s_load_dwordx2 s[16:17], s[6:7], 0x20
	s_load_dwordx2 s[12:13], s[6:7], 0x28
	s_load_dword s18, s[6:7], 0x30
	s_load_dword s20, s[6:7], 0x3c
	s_load_dword s0, s[4:5], 0x4
	s_load_dwordx2 s[10:11], s[6:7], 0x0
	s_load_dwordx2 s[4:5], s[6:7], 0x8
	v_mov_b32_e32 v3, s8
	s_waitcnt lgkmcnt(0)
	v_add_u32_e32 v1, vcc, s20, v0
	s_and_b32 s8, s0, 0xffff
	v_addc_u32_e64 v2, s[6:7], 0, 0, vcc
	v_mad_u64_u32 v[1:2], s[6:7], s8, v3, v[1:2]
	s_load_dwordx4 s[0:3], s[4:5], 0x0
	s_load_dwordx4 s[4:7], s[4:5], 0x10
	s_mov_b32 s19, 0
	v_mov_b32_e32 v2, 0
	v_lshlrev_b32_e32 v2, 4, v0
	s_mov_b32 s20, s19
	s_mov_b32 s21, s19
	v_and_b32_e32 v3, 0xfc0, v2
	v_mov_b32_e32 v20, s20
	v_mov_b32_e32 v2, s17
	v_mov_b32_e32 v21, s21
	s_waitcnt lgkmcnt(0)
	v_mov_b32_e32 v4, s6
	v_mov_b32_e32 v42, s1
	v_mov_b32_e32 v7, s3
	v_mov_b32_e32 v11, s5
	v_add_u32_e32 v43, vcc, s16, v1
	v_cmp_eq_u32_e64 s[16:17], s18, 0
	v_addc_u32_e32 v44, vcc, 0, v2, vcc
	v_cndmask_b32_e64 v2, 0, -1, s[16:17]
	s_mov_b32 s18, 1
	s_brev_b32 s17, 1
	s_mov_b32 s16, s19
	v_mov_b32_e32 v26, s19
	v_mov_b32_e32 v19, s17
	v_mov_b32_e32 v36, v21
	v_mov_b32_e32 v28, v21
	v_mov_b32_e32 v8, v20
	v_mov_b32_e32 v48, v21
	v_mov_b32_e32 v23, v21
	v_mov_b32_e32 v32, v21
	v_mov_b32_e32 v52, v21
	v_mov_b32_e32 v30, v21
	v_mov_b32_e32 v40, v21
	v_mov_b32_e32 v14, v20
	v_mov_b32_e32 v38, v21
	v_mov_b32_e32 v46, v21
	v_mov_b32_e32 v16, v20
	v_mov_b32_e32 v12, v20
	v_mov_b32_e32 v54, v21
	v_mov_b32_e32 v50, v21
	v_mov_b32_e32 v34, v21
	v_mov_b32_e32 v5, s7
	v_mov_b32_e32 v6, s2
	v_and_b32_e32 v24, 3, v0
	v_mov_b32_e32 v41, s0
	v_mov_b32_e32 v10, s4
	s_getpc_b64 s[6:7]
	s_add_u32 s6, s6, Keccak_f1600_RC@rel32@lo+4
	s_addc_u32 s7, s7, Keccak_f1600_RC@rel32@hi+4
	s_mov_b32 s2, s19
	v_mov_b32_e32 v25, s18
	v_mov_b32_e32 v18, s16
	v_mov_b32_e32 v35, v20
	v_mov_b32_e32 v27, v20
	v_mov_b32_e32 v9, v21
	v_mov_b32_e32 v47, v20
	v_mov_b32_e32 v22, v20
	v_mov_b32_e32 v31, v20
	v_mov_b32_e32 v51, v20
	v_mov_b32_e32 v29, v20
	v_mov_b32_e32 v39, v20
	v_mov_b32_e32 v15, v21
	v_mov_b32_e32 v37, v20
	v_mov_b32_e32 v45, v20
	v_mov_b32_e32 v17, v21
	v_mov_b32_e32 v13, v21
	v_mov_b32_e32 v53, v20
	v_mov_b32_e32 v49, v20
	v_mov_b32_e32 v33, v20
BB1_1:                                  ; %for.body29
                                        ; =>This Inner Loop Header: Depth=1
	v_cmp_ne_u32_e32 vcc, 0, v2
	s_cbranch_vccnz BB1_3
; BB#2:                                 ; %do.body30
                                        ;   in Loop: Header=BB1_1 Depth=1
	v_xor_b32_e32 v55, v14, v6
	v_xor_b32_e32 v56, v15, v7
	v_xor_b32_e32 v55, v55, v39
	v_xor_b32_e32 v56, v56, v40
	v_xor_b32_e32 v57, v9, v44
	v_xor_b32_e32 v58, v8, v43
	v_xor_b32_e32 v56, v56, v30
	v_xor_b32_e32 v55, v55, v29
	v_xor_b32_e32 v57, v57, v28
	v_xor_b32_e32 v58, v58, v27
	v_xor_b32_e32 v58, v58, v35
	v_xor_b32_e32 v57, v57, v36
	v_xor_b32_e32 v55, v55, v51
	v_xor_b32_e32 v56, v56, v52
	v_mov_b32_e32 v59, 31
	v_xor_b32_e32 v57, v57, v21
	;;#ASMSTART
	v_alignbit_b32 v60, v55, v56, v59
v_alignbit_b32 v61, v56, v55, v59
	;;#ASMEND
	v_xor_b32_e32 v58, v58, v20
	v_xor_b32_e32 v61, v61, v57
	v_xor_b32_e32 v60, v60, v58
	v_xor_b32_e32 v62, v25, v41
	v_xor_b32_e32 v63, v26, v42
	v_xor_b32_e32 v64, v60, v41
	v_xor_b32_e32 v65, v61, v42
	v_xor_b32_e32 v41, v60, v25
	v_xor_b32_e32 v25, v63, v34
	v_xor_b32_e32 v42, v61, v26
	v_xor_b32_e32 v26, v62, v33
	v_xor_b32_e32 v26, v26, v49
	v_xor_b32_e32 v25, v25, v50
	v_xor_b32_e32 v62, v60, v49
	v_xor_b32_e32 v63, v61, v50
	v_xor_b32_e32 v25, v25, v54
	v_xor_b32_e32 v49, v61, v54
	v_xor_b32_e32 v54, v12, v10
	v_xor_b32_e32 v26, v26, v53
	v_xor_b32_e32 v50, v60, v53
	v_xor_b32_e32 v53, v13, v11
	v_xor_b32_e32 v53, v53, v17
	v_xor_b32_e32 v54, v54, v16
	v_xor_b32_e32 v54, v54, v45
	v_xor_b32_e32 v53, v53, v46
	v_xor_b32_e32 v53, v53, v38
	v_xor_b32_e32 v54, v54, v37
	v_xor_b32_e32 v34, v61, v34
	v_xor_b32_e32 v33, v60, v33
	;;#ASMSTART
	v_alignbit_b32 v60, v54, v53, v59
v_alignbit_b32 v61, v53, v54, v59
	;;#ASMEND
	v_xor_b32_e32 v60, v60, v26
	v_xor_b32_e32 v61, v61, v25
	v_xor_b32_e32 v66, v61, v7
	v_xor_b32_e32 v67, v60, v6
	v_xor_b32_e32 v6, v60, v14
	v_xor_b32_e32 v7, v61, v15
	v_xor_b32_e32 v14, v29, v60
	v_xor_b32_e32 v29, v19, v5
	v_xor_b32_e32 v15, v30, v61
	v_xor_b32_e32 v30, v18, v4
	v_xor_b32_e32 v29, v29, v32
	v_xor_b32_e32 v30, v30, v31
	v_xor_b32_e32 v30, v30, v22
	v_xor_b32_e32 v29, v29, v23
	v_xor_b32_e32 v29, v29, v48
	v_xor_b32_e32 v30, v30, v47
	v_xor_b32_e32 v68, v61, v40
	v_xor_b32_e32 v69, v60, v39
	;;#ASMSTART
	v_alignbit_b32 v39, v30, v29, v59
v_alignbit_b32 v40, v29, v30, v59
	;;#ASMEND
	v_xor_b32_e32 v40, v40, v56
	v_xor_b32_e32 v39, v39, v55
	v_xor_b32_e32 v55, v39, v10
	v_xor_b32_e32 v56, v40, v11
	v_xor_b32_e32 v10, v39, v16
	v_xor_b32_e32 v11, v40, v17
	;;#ASMSTART
	v_alignbit_b32 v16, v58, v57, v59
v_alignbit_b32 v17, v57, v58, v59
	;;#ASMEND
	v_xor_b32_e32 v16, v16, v54
	v_xor_b32_e32 v17, v17, v53
	v_xor_b32_e32 v53, v16, v18
	v_mov_b32_e32 v18, 20
	;;#ASMSTART
	v_alignbit_b32 v57, v7, v6, v18
v_alignbit_b32 v58, v6, v7, v18
	;;#ASMEND
	v_mov_b32_e32 v6, 21
	v_xor_b32_e32 v52, v52, v61
	v_xor_b32_e32 v51, v51, v60
	v_xor_b32_e32 v60, v40, v13
	v_xor_b32_e32 v61, v39, v12
	v_xor_b32_e32 v12, v39, v37
	v_xor_b32_e32 v13, v40, v38
	v_xor_b32_e32 v37, v17, v5
	v_xor_b32_e32 v38, v16, v4
	v_xor_b32_e32 v4, v17, v23
	v_xor_b32_e32 v5, v16, v22
	;;#ASMSTART
	v_alignbit_b32 v70, v11, v10, v6
v_alignbit_b32 v71, v10, v11, v6
	;;#ASMEND
	v_mov_b32_e32 v6, 11
	;;#ASMSTART
	v_alignbit_b32 v4, v5, v4, v6
v_alignbit_b32 v5, v4, v5, v6
	;;#ASMEND
	v_xor_b32_e32 v54, v17, v19
	v_xor_b32_e32 v32, v17, v32
	v_xor_b32_e32 v31, v16, v31
	v_xor_b32_e32 v22, v16, v47
	v_xor_b32_e32 v23, v17, v48
	;;#ASMSTART
	v_alignbit_b32 v16, v26, v25, v59
v_alignbit_b32 v17, v25, v26, v59
	;;#ASMEND
	v_xor_b32_e32 v6, v5, v58
	v_and_b32_e32 v7, v5, v71
	v_xor_b32_e32 v16, v16, v30
	v_xor_b32_e32 v17, v17, v29
	v_xor_b32_e32 v7, v6, v7
	v_xor_b32_e32 v6, v4, v57
	v_and_b32_e32 v10, v4, v70
	v_xor_b32_e32 v29, v17, v44
	v_xor_b32_e32 v30, v16, v43
	v_xor_b32_e32 v43, v16, v27
	v_xor_b32_e32 v44, v17, v28
	v_xor_b32_e32 v6, v6, v10
	v_xor_b32_e32 v9, v17, v9
	v_xor_b32_e32 v27, v17, v36
	v_xor_b32_e32 v8, v16, v8
	v_xor_b32_e32 v28, v16, v35
	v_xor_b32_e32 v17, v17, v21
	v_xor_b32_e32 v16, v16, v20
	v_mov_b32_e32 v10, 18
	;;#ASMSTART
	v_alignbit_b32 v72, v16, v17, v10
v_alignbit_b32 v73, v17, v16, v10
	;;#ASMEND
	v_xor_b32_e32 v10, v73, v71
	v_and_b32_e32 v11, v5, v73
	v_xor_b32_e32 v11, v10, v11
	v_xor_b32_e32 v10, v72, v70
	v_and_b32_e32 v16, v4, v72
	v_xor_b32_e32 v10, v10, v16
	v_not_b32_e32 v16, v72
	v_and_b32_e32 v16, v64, v16
	v_not_b32_e32 v17, v73
	v_xor_b32_e32 v4, v16, v4
	v_mov_b32_e32 v16, 12
	v_mov_b32_e32 v18, 29
	v_and_b32_e32 v17, v65, v17
	;;#ASMSTART
	v_alignbit_b32 v8, v8, v9, v16
v_alignbit_b32 v9, v9, v8, v16
	;;#ASMEND
	v_mov_b32_e32 v16, 4
	v_xor_b32_e32 v5, v17, v5
	;;#ASMSTART
	v_alignbit_b32 v16, v38, v37, v16
v_alignbit_b32 v17, v37, v38, v16
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v18, v33, v34, v18
v_alignbit_b32 v19, v34, v33, v18
	;;#ASMEND
	v_xor_b32_e32 v20, v19, v17
	v_and_b32_e32 v21, v19, v9
	v_xor_b32_e32 v26, v20, v21
	v_xor_b32_e32 v20, v18, v16
	v_and_b32_e32 v21, v18, v8
	v_xor_b32_e32 v25, v20, v21
	v_mov_b32_e32 v20, 19
	;;#ASMSTART
	v_alignbit_b32 v20, v15, v14, v20
v_alignbit_b32 v21, v14, v15, v20
	;;#ASMEND
	v_xor_b32_e32 v14, v21, v9
	v_and_b32_e32 v15, v19, v21
	v_xor_b32_e32 v15, v14, v15
	v_xor_b32_e32 v14, v20, v8
	v_and_b32_e32 v33, v18, v20
	v_xor_b32_e32 v14, v14, v33
	v_mov_b32_e32 v33, 3
	;;#ASMSTART
	v_alignbit_b32 v33, v13, v12, v33
v_alignbit_b32 v34, v12, v13, v33
	;;#ASMEND
	v_not_b32_e32 v12, v20
	v_not_b32_e32 v13, v21
	v_and_b32_e32 v12, v33, v12
	v_and_b32_e32 v13, v34, v13
	v_xor_b32_e32 v13, v13, v19
	v_xor_b32_e32 v12, v12, v18
	v_xor_b32_e32 v18, v16, v20
	v_xor_b32_e32 v19, v17, v21
	v_and_b32_e32 v20, v17, v34
	v_xor_b32_e32 v19, v19, v20
	v_and_b32_e32 v20, v16, v33
	v_xor_b32_e32 v18, v18, v20
	v_xor_b32_e32 v20, v33, v8
	v_and_b32_e32 v8, v16, v8
	v_xor_b32_e32 v16, v34, v9
	v_and_b32_e32 v9, v17, v9
	v_xor_b32_e32 v9, v16, v9
	v_mov_b32_e32 v16, 14
	v_xor_b32_e32 v8, v20, v8
	;;#ASMSTART
	v_alignbit_b32 v20, v50, v49, v16
v_alignbit_b32 v21, v49, v50, v16
	;;#ASMEND
	v_mov_b32_e32 v16, 7
	;;#ASMSTART
	v_alignbit_b32 v35, v31, v32, v16
v_alignbit_b32 v36, v32, v31, v16
	;;#ASMEND
	v_mov_b32_e32 v16, 24
	;;#ASMSTART
	v_alignbit_b32 v27, v28, v27, v16
v_alignbit_b32 v28, v27, v28, v16
	;;#ASMEND
	v_xor_b32_e32 v16, v36, v21
	v_and_b32_e32 v17, v28, v21
	;;#ASMSTART
	v_alignbit_b32 v37, v67, v66, v59
v_alignbit_b32 v38, v66, v67, v59
	;;#ASMEND
	v_xor_b32_e32 v17, v16, v17
	v_xor_b32_e32 v16, v35, v20
	v_and_b32_e32 v31, v27, v20
	v_xor_b32_e32 v16, v16, v31
	v_xor_b32_e32 v31, v38, v28
	v_and_b32_e32 v32, v38, v21
	v_xor_b32_e32 v32, v31, v32
	v_xor_b32_e32 v31, v37, v27
	v_and_b32_e32 v33, v37, v20
	v_xor_b32_e32 v31, v31, v33
	v_mov_b32_e32 v33, 26
	;;#ASMSTART
	v_alignbit_b32 v47, v61, v60, v33
v_alignbit_b32 v48, v60, v61, v33
	;;#ASMEND
	v_not_b32_e32 v33, v47
	v_and_b32_e32 v33, v35, v33
	v_not_b32_e32 v35, v35
	v_and_b32_e32 v27, v27, v35
	v_not_b32_e32 v35, v36
	v_xor_b32_e32 v45, v39, v45
	v_xor_b32_e32 v39, v27, v47
	v_and_b32_e32 v28, v28, v35
	v_xor_b32_e32 v20, v47, v20
	v_and_b32_e32 v27, v37, v47
	v_xor_b32_e32 v46, v40, v46
	v_xor_b32_e32 v40, v28, v48
	v_xor_b32_e32 v27, v20, v27
	v_xor_b32_e32 v21, v48, v21
	v_and_b32_e32 v28, v38, v48
	v_mov_b32_e32 v20, 5
	v_not_b32_e32 v34, v48
	v_xor_b32_e32 v28, v21, v28
	;;#ASMSTART
	v_alignbit_b32 v20, v30, v29, v20
v_alignbit_b32 v21, v29, v30, v20
	;;#ASMEND
	v_mov_b32_e32 v29, 28
	v_and_b32_e32 v34, v36, v34
	;;#ASMSTART
	v_alignbit_b32 v35, v42, v41, v29
v_alignbit_b32 v36, v41, v42, v29
	;;#ASMEND
	v_mov_b32_e32 v29, 22
	v_xor_b32_e32 v34, v34, v38
	v_xor_b32_e32 v33, v33, v37
	;;#ASMSTART
	v_alignbit_b32 v37, v69, v68, v29
v_alignbit_b32 v38, v68, v69, v29
	;;#ASMEND
	v_xor_b32_e32 v29, v38, v21
	v_and_b32_e32 v30, v38, v36
	v_xor_b32_e32 v50, v29, v30
	v_xor_b32_e32 v29, v37, v20
	v_and_b32_e32 v30, v37, v35
	v_xor_b32_e32 v49, v29, v30
	v_mov_b32_e32 v29, 17
	;;#ASMSTART
	v_alignbit_b32 v41, v45, v46, v29
v_alignbit_b32 v42, v46, v45, v29
	;;#ASMEND
	v_xor_b32_e32 v29, v42, v36
	v_and_b32_e32 v30, v38, v42
	v_xor_b32_e32 v30, v29, v30
	v_xor_b32_e32 v29, v41, v35
	v_and_b32_e32 v45, v37, v41
	v_xor_b32_e32 v29, v29, v45
	v_mov_b32_e32 v45, 8
	;;#ASMSTART
	v_alignbit_b32 v47, v23, v22, v45
v_alignbit_b32 v48, v22, v23, v45
	;;#ASMEND
	v_not_b32_e32 v22, v41
	v_not_b32_e32 v23, v42
	v_and_b32_e32 v22, v47, v22
	v_and_b32_e32 v23, v48, v23
	v_xor_b32_e32 v45, v22, v37
	v_not_b32_e32 v22, v47
	v_xor_b32_e32 v46, v23, v38
	v_not_b32_e32 v23, v48
	v_and_b32_e32 v22, v20, v22
	v_and_b32_e32 v23, v21, v23
	v_xor_b32_e32 v37, v35, v47
	v_and_b32_e32 v20, v35, v20
	v_xor_b32_e32 v35, v36, v48
	v_and_b32_e32 v21, v36, v21
	v_xor_b32_e32 v36, v35, v21
	v_xor_b32_e32 v35, v37, v20
	v_mov_b32_e32 v37, 2
	v_mov_b32_e32 v20, 25
	v_xor_b32_e32 v23, v23, v42
	v_xor_b32_e32 v22, v22, v41
	;;#ASMSTART
	v_alignbit_b32 v41, v56, v55, v37
v_alignbit_b32 v42, v55, v56, v37
	;;#ASMEND
	v_mov_b32_e32 v37, 9
	;;#ASMSTART
	v_alignbit_b32 v20, v44, v43, v20
v_alignbit_b32 v21, v43, v44, v20
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v43, v54, v53, v37
v_alignbit_b32 v44, v53, v54, v37
	;;#ASMEND
	v_xor_b32_e32 v37, v42, v21
	v_and_b32_e32 v38, v44, v21
	v_xor_b32_e32 v54, v37, v38
	v_xor_b32_e32 v37, v41, v20
	v_and_b32_e32 v38, v43, v20
	v_xor_b32_e32 v53, v37, v38
	v_mov_b32_e32 v37, 23
	;;#ASMSTART
	v_alignbit_b32 v55, v63, v62, v37
v_alignbit_b32 v56, v62, v63, v37
	;;#ASMEND
	v_mov_b32_e32 v37, 30
	;;#ASMSTART
	v_alignbit_b32 v59, v51, v52, v37
v_alignbit_b32 v60, v52, v51, v37
	;;#ASMEND
	v_xor_b32_e32 v37, v60, v21
	v_and_b32_e32 v38, v60, v56
	s_ashr_i32 s3, s2, 31
	s_add_i32 s0, s2, 1
	s_lshl_b64 s[2:3], s[2:3], 3
	v_xor_b32_e32 v38, v37, v38
	v_xor_b32_e32 v37, v59, v20
	v_and_b32_e32 v47, v59, v55
	s_add_u32 s2, s2, s6
	v_not_b32_e32 v20, v20
	v_not_b32_e32 v21, v21
	v_xor_b32_e32 v37, v37, v47
	v_xor_b32_e32 v47, v56, v42
	v_and_b32_e32 v48, v60, v42
	s_addc_u32 s3, s3, s7
	v_xor_b32_e32 v48, v47, v48
	v_and_b32_e32 v21, v56, v21
	v_xor_b32_e32 v47, v55, v41
	v_and_b32_e32 v51, v59, v41
	v_and_b32_e32 v20, v55, v20
	v_xor_b32_e32 v47, v47, v51
	v_xor_b32_e32 v52, v21, v44
	v_xor_b32_e32 v51, v20, v43
	s_load_dwordx2 s[2:3], s[2:3], 0x0
	v_xor_b32_e32 v20, v43, v59
	v_and_b32_e32 v41, v43, v41
	v_xor_b32_e32 v21, v44, v60
	v_and_b32_e32 v42, v44, v42
	v_xor_b32_e32 v21, v21, v42
	v_xor_b32_e32 v20, v20, v41
	v_xor_b32_e32 v41, v70, v64
	v_and_b32_e32 v42, v70, v57
	v_xor_b32_e32 v41, v41, v42
	v_xor_b32_e32 v42, v71, v65
	v_and_b32_e32 v43, v71, v58
	v_xor_b32_e32 v42, v42, v43
	v_not_b32_e32 v43, v64
	v_not_b32_e32 v44, v65
	v_and_b32_e32 v43, v57, v43
	v_and_b32_e32 v44, v58, v44
	s_waitcnt lgkmcnt(0)
	v_xor_b32_e32 v41, s2, v41
	v_xor_b32_e32 v42, s3, v42
	v_xor_b32_e32 v44, v44, v73
	v_xor_b32_e32 v43, v43, v72
	s_mov_b32 s2, s0
BB1_3:                                  ; %if.end
                                        ;   in Loop: Header=BB1_1 Depth=1
	s_cmp_lt_i32 s2, 23
	s_cbranch_scc1 BB1_1
; BB#4:                                 ; %for.cond.cleanup28
	v_xor_b32_e32 v10, v12, v10
	v_xor_b32_e32 v10, v10, v16
	v_xor_b32_e32 v10, v10, v45
	v_xor_b32_e32 v45, v15, v7
	v_xor_b32_e32 v40, v45, v40
	v_xor_b32_e32 v45, v14, v6
	v_xor_b32_e32 v39, v45, v39
	v_xor_b32_e32 v43, v8, v43
	v_xor_b32_e32 v44, v9, v44
	v_xor_b32_e32 v11, v13, v11
	v_xor_b32_e32 v39, v39, v29
	v_xor_b32_e32 v40, v40, v30
	v_xor_b32_e32 v18, v18, v4
	v_xor_b32_e32 v28, v44, v28
	v_xor_b32_e32 v27, v43, v27
	v_xor_b32_e32 v18, v18, v31
	v_xor_b32_e32 v25, v25, v41
	v_xor_b32_e32 v26, v26, v42
	v_xor_b32_e32 v11, v11, v17
	v_xor_b32_e32 v19, v19, v5
	v_xor_b32_e32 v27, v27, v35
	v_xor_b32_e32 v28, v28, v36
	v_xor_b32_e32 v40, v40, v52
	v_xor_b32_e32 v39, v39, v51
	v_mov_b32_e32 v31, 31
	v_xor_b32_e32 v19, v19, v32
	v_xor_b32_e32 v26, v26, v34
	v_xor_b32_e32 v25, v25, v33
	v_xor_b32_e32 v11, v11, v46
	v_xor_b32_e32 v28, v28, v21
	;;#ASMSTART
	v_alignbit_b32 v32, v39, v40, v31
v_alignbit_b32 v35, v40, v39, v31
	;;#ASMEND
	v_xor_b32_e32 v27, v27, v20
	v_xor_b32_e32 v25, v25, v49
	v_xor_b32_e32 v26, v26, v50
	v_xor_b32_e32 v35, v35, v28
	v_xor_b32_e32 v32, v32, v27
	v_xor_b32_e32 v11, v11, v38
	v_xor_b32_e32 v10, v10, v37
	v_xor_b32_e32 v18, v18, v22
	v_xor_b32_e32 v19, v19, v23
	v_xor_b32_e32 v36, v32, v41
	v_xor_b32_e32 v26, v26, v54
	v_xor_b32_e32 v25, v25, v53
	v_xor_b32_e32 v41, v35, v42
	v_xor_b32_e32 v34, v35, v34
	v_xor_b32_e32 v35, v32, v33
	;;#ASMSTART
	v_alignbit_b32 v32, v10, v11, v31
v_alignbit_b32 v33, v11, v10, v31
	;;#ASMEND
	v_xor_b32_e32 v32, v32, v25
	v_xor_b32_e32 v33, v33, v26
	v_xor_b32_e32 v19, v19, v48
	v_xor_b32_e32 v18, v18, v47
	v_xor_b32_e32 v42, v33, v7
	v_xor_b32_e32 v43, v32, v6
	;;#ASMSTART
	v_alignbit_b32 v6, v18, v19, v31
v_alignbit_b32 v7, v19, v18, v31
	;;#ASMEND
	v_xor_b32_e32 v6, v6, v39
	v_xor_b32_e32 v7, v7, v40
	v_xor_b32_e32 v13, v7, v13
	v_xor_b32_e32 v12, v6, v12
	v_xor_b32_e32 v16, v6, v16
	v_xor_b32_e32 v17, v7, v17
	v_xor_b32_e32 v37, v6, v37
	v_xor_b32_e32 v38, v7, v38
	;;#ASMSTART
	v_alignbit_b32 v6, v27, v28, v31
v_alignbit_b32 v7, v28, v27, v31
	;;#ASMEND
	v_xor_b32_e32 v6, v6, v10
	v_xor_b32_e32 v7, v7, v11
	v_xor_b32_e32 v11, v6, v4
	v_mov_b32_e32 v4, 26
	v_xor_b32_e32 v10, v7, v5
	v_xor_b32_e32 v23, v7, v23
	v_xor_b32_e32 v22, v6, v22
	;;#ASMSTART
	v_alignbit_b32 v6, v12, v13, v4
v_alignbit_b32 v7, v13, v12, v4
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v12, v25, v26, v31
v_alignbit_b32 v13, v26, v25, v31
	;;#ASMEND
	v_xor_b32_e32 v12, v12, v18
	v_xor_b32_e32 v13, v13, v19
	v_xor_b32_e32 v19, v12, v8
	v_xor_b32_e32 v14, v32, v14
	v_xor_b32_e32 v15, v33, v15
	v_mov_b32_e32 v8, 20
	;;#ASMSTART
	v_alignbit_b32 v25, v15, v14, v8
v_alignbit_b32 v26, v14, v15, v8
	;;#ASMEND
	v_mov_b32_e32 v8, 21
	;;#ASMSTART
	v_alignbit_b32 v4, v43, v42, v31
v_alignbit_b32 v5, v42, v43, v31
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v31, v17, v16, v8
v_alignbit_b32 v39, v16, v17, v8
	;;#ASMEND
	v_mov_b32_e32 v8, 11
	;;#ASMSTART
	v_alignbit_b32 v14, v22, v23, v8
v_alignbit_b32 v15, v23, v22, v8
	;;#ASMEND
	v_xor_b32_e32 v18, v13, v9
	v_xor_b32_e32 v8, v15, v26
	v_and_b32_e32 v9, v15, v39
	v_xor_b32_e32 v9, v8, v9
	v_xor_b32_e32 v8, v14, v25
	v_and_b32_e32 v16, v14, v31
	v_xor_b32_e32 v8, v8, v16
	v_xor_b32_e32 v13, v13, v21
	v_xor_b32_e32 v12, v12, v20
	v_mov_b32_e32 v16, 18
	;;#ASMSTART
	v_alignbit_b32 v16, v12, v13, v16
v_alignbit_b32 v17, v13, v12, v16
	;;#ASMEND
	v_xor_b32_e32 v12, v17, v39
	v_and_b32_e32 v13, v15, v17
	v_xor_b32_e32 v13, v12, v13
	v_xor_b32_e32 v12, v16, v31
	v_and_b32_e32 v20, v14, v16
	v_xor_b32_e32 v12, v12, v20
	v_not_b32_e32 v20, v16
	v_and_b32_e32 v20, v36, v20
	v_xor_b32_e32 v14, v20, v14
	v_not_b32_e32 v20, v36
	v_not_b32_e32 v21, v17
	v_and_b32_e32 v20, v25, v20
	v_and_b32_e32 v21, v41, v21
	v_xor_b32_e32 v16, v20, v16
	v_mov_b32_e32 v20, 12
	v_xor_b32_e32 v15, v21, v15
	v_not_b32_e32 v21, v41
	v_xor_b32_e32 v44, v29, v32
	v_xor_b32_e32 v45, v30, v33
	;;#ASMSTART
	v_alignbit_b32 v30, v19, v18, v20
v_alignbit_b32 v29, v18, v19, v20
	;;#ASMEND
	v_mov_b32_e32 v18, 4
	v_and_b32_e32 v21, v26, v21
	;;#ASMSTART
	v_alignbit_b32 v33, v11, v10, v18
v_alignbit_b32 v32, v10, v11, v18
	;;#ASMEND
	v_mov_b32_e32 v10, 29
	v_xor_b32_e32 v17, v21, v17
	;;#ASMSTART
	v_alignbit_b32 v20, v35, v34, v10
v_alignbit_b32 v21, v34, v35, v10
	;;#ASMEND
	v_xor_b32_e32 v10, v21, v32
	v_and_b32_e32 v11, v21, v29
	v_xor_b32_e32 v11, v10, v11
	v_xor_b32_e32 v10, v20, v33
	v_and_b32_e32 v18, v20, v30
	v_xor_b32_e32 v10, v10, v18
	v_mov_b32_e32 v18, 19
	;;#ASMSTART
	v_alignbit_b32 v28, v45, v44, v18
v_alignbit_b32 v27, v44, v45, v18
	;;#ASMEND
	v_xor_b32_e32 v18, v27, v29
	v_and_b32_e32 v19, v21, v27
	v_xor_b32_e32 v19, v18, v19
	v_xor_b32_e32 v18, v28, v30
	v_and_b32_e32 v22, v20, v28
	v_xor_b32_e32 v18, v18, v22
	v_mov_b32_e32 v22, 3
	;;#ASMSTART
	v_alignbit_b32 v35, v38, v37, v22
v_alignbit_b32 v34, v37, v38, v22
	;;#ASMEND
	v_not_b32_e32 v22, v28
	v_not_b32_e32 v23, v27
	v_and_b32_e32 v22, v35, v22
	v_and_b32_e32 v23, v34, v23
	v_xor_b32_e32 v21, v23, v21
	v_xor_b32_e32 v20, v22, v20
	v_and_b32_e32 v22, v39, v26
	v_xor_b32_e32 v23, v41, v39
	v_and_b32_e32 v25, v31, v25
	v_xor_b32_e32 v26, v36, v31
	v_xor_b32_e32 v22, v23, v22
	v_xor_b32_e32 v25, v26, v25
	v_xor_b32_e32 v23, 0x80000000, v22
	v_xor_b32_e32 v22, 0x80008008, v25
	v_cmp_eq_u32_e64 s[0:1], 0, v24
	s_and_saveexec_b64 s[2:3], s[0:1]
	; mask branch BB1_6
BB1_5:                                  ; %if.then884
	s_mov_b32 m0, -1
	ds_write2_b64 v3, v[22:23], v[8:9] offset1:1
	ds_write2_b64 v3, v[12:13], v[14:15] offset0:2 offset1:3
	ds_write2_b64 v3, v[16:17], v[10:11] offset0:4 offset1:5
	ds_write2_b64 v3, v[18:19], v[20:21] offset0:6 offset1:7
BB1_6:                                  ; %if.end898
	s_or_b64 exec, exec, s[2:3]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_mov_b32 m0, -1
	ds_read_b32 v26, v3
	v_and_b32_e32 v25, 1, v0
	v_lshlrev_b32_e32 v25, 5, v25
	v_add_u32_e32 v31, vcc, v3, v25
	s_mov_b32 s8, 0x1000193
	ds_read2_b64 v[36:39], v31 offset1:1
	s_waitcnt lgkmcnt(1)
	v_mul_lo_i32 v25, v26, s8
	ds_read2_b64 v[41:44], v31 offset0:2 offset1:3
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v25, v25, v36
	v_mul_lo_i32 v56, v36, s8
	v_cvt_f32_u32_e32 v36, s9
	v_mul_lo_i32 v53, v37, s8
	v_mul_lo_i32 v54, v38, s8
	v_mul_lo_i32 v55, v39, s8
	v_rcp_iflag_f32_e32 v36, v36
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v42, v42, s8
	v_mul_lo_i32 v43, v43, s8
	v_mul_f32_e32 v36, 0x4f800000, v36
	v_cvt_u32_f32_e32 v36, v36
	v_mul_lo_i32 v44, v44, s8
	v_xor_b32_e32 v27, v32, v27
	v_mul_hi_u32 v37, v36, s9
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_mul_lo_i32 v37, v36, s9
	v_sub_u32_e32 v38, vcc, 0, v37
	v_cndmask_b32_e64 v37, v37, v38, s[2:3]
	v_mul_hi_u32 v37, v37, v36
	v_add_u32_e32 v38, vcc, v37, v36
	v_subrev_u32_e32 v36, vcc, v37, v36
	v_cndmask_b32_e64 v40, v36, v38, s[2:3]
	v_mul_hi_u32 v36, v40, v25
	v_mul_lo_i32 v36, v36, s9
	v_cmp_ge_u32_e64 s[2:3], v25, v36
	v_subrev_u32_e32 v25, vcc, v36, v25
	v_cmp_le_u32_e32 vcc, s9, v25
	v_cndmask_b32_e64 v36, 0, -1, s[2:3]
	v_cndmask_b32_e64 v37, 0, -1, vcc
	v_and_b32_e32 v37, v37, v36
	v_cmp_eq_u32_e64 s[2:3], 0, v36
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v36, vcc, s9, v25
	v_cndmask_b32_e64 v36, v36, v25, s[4:5]
	v_add_u32_e32 v25, vcc, s9, v25
	v_cndmask_b32_e64 v25, v36, v25, s[2:3]
	v_lshlrev_b32_e32 v36, 2, v0
	v_and_b32_e32 v0, 0xfc, v0
	ds_write_b32 v36, v25 offset:4096
	v_lshlrev_b32_e32 v37, 2, v0
	ds_read_b32 v38, v37 offset:4096
	v_mov_b32_e32 v25, 0
	v_mov_b32_e32 v39, v25
	v_mov_b32_e32 v0, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[38:39], 7, v[38:39]
	v_add_u32_e32 v45, vcc, s14, v38
	v_addc_u32_e32 v0, vcc, v0, v39, vcc
	v_lshlrev_b64 v[38:39], 5, v[24:25]
	v_add_u32_e32 v45, vcc, v45, v38
	v_addc_u32_e32 v46, vcc, v0, v39, vcc
	v_add_u32_e32 v49, vcc, 16, v45
	v_addc_u32_e32 v50, vcc, 0, v46, vcc
	flat_load_dwordx4 v[45:48], v[45:46]
	flat_load_dwordx4 v[49:52], v[49:50]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v46, v46, v53
	v_xor_b32_e32 v49, v49, v41
	v_xor_b32_e32 v41, 1, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v50, v50, v42
	v_xor_b32_e32 v51, v51, v43
	v_xor_b32_e32 v0, v45, v56
	v_xor_b32_e32 v41, v46, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v45, v48, v55
	v_xor_b32_e32 v47, v47, v54
	v_mul_lo_i32 v54, v45, s8
	v_mul_lo_i32 v42, v42, s9
	v_xor_b32_e32 v52, v52, v44
	v_mul_lo_i32 v53, v47, s8
	v_mul_lo_i32 v55, v46, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v43, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v43, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v43, s15
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v43, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_mul_lo_i32 v50, v50, s8
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v52, s8
	v_xor_b32_e32 v55, v42, v55
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v43, v43, v53
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 2, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v54
	v_xor_b32_e32 v41, v43, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v55, s8
	v_mul_lo_i32 v53, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v54, v42, v54
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v44, v44, v53
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 3, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v44, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v53, v54, s8
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 4, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v46, v50
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v41, 5, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v48, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v49, v47, v49
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v49, v47, v49
	v_xor_b32_e32 v51, v46, v41
	v_xor_b32_e32 v41, 6, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v52, v45, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v50, v48, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v48, v50
	v_xor_b32_e32 v51, v47, v41
	v_xor_b32_e32 v41, 7, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v46, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 8, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v41, v0
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 9, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v41, v53, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 10, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v43, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v44, s8
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v44, v44, v54
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 11, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v44, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 12, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	s_mov_b32 m0, -1
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v46, v50
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v41, 13, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v48, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v49, v47, v49
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v49, v47, v49
	v_xor_b32_e32 v51, v46, v41
	v_xor_b32_e32 v41, 14, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v52, v45, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v50, v48, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v48, v50
	v_xor_b32_e32 v51, v47, v41
	v_xor_b32_e32 v41, 15, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v46, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 16, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v41, v0
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 17, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v41, v53, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 18, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v43, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v44, s8
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v44, v44, v54
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 19, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v44, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 20, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v46, v50
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v41, 21, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v48, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v49, v47, v49
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v49, v47, v49
	v_xor_b32_e32 v51, v46, v41
	v_xor_b32_e32 v41, 22, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v52, v45, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v50, v48, v50
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v0, v41, v0
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v48, v50
	v_xor_b32_e32 v51, v47, v41
	v_xor_b32_e32 v41, 23, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v46, v42
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v0, v0, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v54
	v_xor_b32_e32 v54, v41, v0
	v_mul_lo_i32 v0, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v50, v48, v0
	v_xor_b32_e32 v0, 24, v26
	v_mul_lo_i32 v0, v0, s8
	v_xor_b32_e32 v51, v47, v41
	v_xor_b32_e32 v52, v46, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v0, v0, v54
	v_mul_hi_u32 v41, v40, v0
	v_mov_b32_e32 v45, s15
	v_xor_b32_e32 v44, v44, v55
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v41, v41, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v0, v41
	v_subrev_u32_e32 v0, vcc, v41, v0
	v_cmp_le_u32_e32 vcc, s9, v0
	v_cndmask_b32_e64 v41, 0, -1, s[2:3]
	v_cndmask_b32_e64 v42, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v41
	v_and_b32_e32 v41, v42, v41
	v_cmp_eq_u32_e64 s[4:5], 0, v41
	v_subrev_u32_e32 v41, vcc, s9, v0
	v_cndmask_b32_e64 v41, v41, v0, s[4:5]
	v_add_u32_e32 v0, vcc, s9, v0
	v_cndmask_b32_e64 v0, v41, v0, s[2:3]
	ds_write_b32 v36, v0 offset:4096
	v_or_b32_e32 v0, 0x100c, v36
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 25, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v53, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	s_mov_b32 m0, -1
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 26, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v43, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v56, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 27, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v56
	v_xor_b32_e32 v41, v44, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 28, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v46, v50
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v41, 29, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v48, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v49, v47, v49
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v49, v47, v49
	v_xor_b32_e32 v51, v46, v41
	v_xor_b32_e32 v41, 30, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v52, v45, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v48, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v48, v50
	v_xor_b32_e32 v51, v47, v41
	v_xor_b32_e32 v41, 31, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v46, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v55
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 32, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v41, v54
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 33, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v53, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 34, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v43, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v56, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 35, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v56
	v_xor_b32_e32 v41, v44, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 36, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v46, v50
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v41, 37, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v48, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v49, v47, v49
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v49, v47, v49
	v_xor_b32_e32 v51, v46, v41
	v_xor_b32_e32 v41, 38, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v52, v45, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v48, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	s_mov_b32 m0, -1
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v48, v50
	v_xor_b32_e32 v51, v47, v41
	v_xor_b32_e32 v41, 39, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v46, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v55
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4096
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 40, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v41, v54
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 41, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v53, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 42, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v43, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v56, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 43, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v56
	v_xor_b32_e32 v41, v44, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 44, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v46, v50
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v41, 45, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v48, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v49, v47, v49
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v49, v47, v49
	v_xor_b32_e32 v51, v46, v41
	v_xor_b32_e32 v41, 46, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v52, v45, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v48, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v48, v50
	v_xor_b32_e32 v51, v47, v41
	v_xor_b32_e32 v41, 47, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v46, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v55
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4100
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 48, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v41, v54
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 49, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v53, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 50, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v43, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v56, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 51, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v56
	v_xor_b32_e32 v41, v44, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	s_mov_b32 m0, -1
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 52, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v46, v50
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v41, 53, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v48, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v49, v47, v49
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v49, v47, v49
	v_xor_b32_e32 v51, v46, v41
	v_xor_b32_e32 v41, 54, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v52, v45, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v48, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v48, v50
	v_xor_b32_e32 v51, v47, v41
	v_xor_b32_e32 v41, 55, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v46, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v55
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v37 offset:4104
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 56, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v41, v54
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 57, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v53, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 58, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v43, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v44, s8
	v_mul_lo_i32 v56, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v44, v44, v55
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 59, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v43, v43, v56
	v_xor_b32_e32 v41, v44, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v50, v50, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v52, s8
	v_xor_b32_e32 v49, v45, v49
	v_xor_b32_e32 v51, v48, v41
	v_xor_b32_e32 v41, 60, v26
	v_mul_lo_i32 v41, v41, s8
	v_xor_b32_e32 v52, v47, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v50, v46, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v50, v46, v50
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v41, 61, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v49, v52, s8
	v_xor_b32_e32 v52, v48, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v50, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v49, v47, v49
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_mul_lo_i32 v54, v54, s8
	v_mul_lo_i32 v53, v53, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v49, v49, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v53, v42, v53
	v_mul_lo_i32 v42, v51, s8
	v_xor_b32_e32 v49, v47, v49
	v_xor_b32_e32 v51, v46, v41
	v_xor_b32_e32 v41, 62, v26
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v50, v52, s8
	v_xor_b32_e32 v52, v45, v42
	v_xor_b32_e32 v44, v44, v56
	v_xor_b32_e32 v41, v49, v41
	v_mul_hi_u32 v42, v40, v41
	v_xor_b32_e32 v43, v43, v55
	v_xor_b32_e32 v50, v48, v50
	v_mul_lo_i32 v55, v43, s8
	v_mul_lo_i32 v42, v42, s9
	v_mul_lo_i32 v56, v44, s8
	v_xor_b32_e32 v26, 63, v26
	v_mul_lo_i32 v50, v50, s8
	v_cmp_ge_u32_e64 s[2:3], v41, v42
	v_subrev_u32_e32 v41, vcc, v42, v41
	v_cmp_le_u32_e32 vcc, s9, v41
	v_cndmask_b32_e64 v42, 0, -1, s[2:3]
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v42
	v_and_b32_e32 v42, v45, v42
	v_cmp_eq_u32_e64 s[4:5], 0, v42
	v_subrev_u32_e32 v42, vcc, s9, v41
	v_cndmask_b32_e64 v42, v42, v41, s[4:5]
	v_add_u32_e32 v41, vcc, s9, v41
	v_cndmask_b32_e64 v41, v42, v41, s[2:3]
	ds_write_b32 v36, v41 offset:4096
	ds_read_b32 v41, v0
	v_mov_b32_e32 v42, v25
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v26, v26, s8
	v_mul_lo_i32 v54, v54, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[41:42], 7, v[41:42]
	v_add_u32_e32 v41, vcc, s14, v41
	v_addc_u32_e32 v42, vcc, v45, v42, vcc
	v_add_u32_e32 v41, vcc, v41, v38
	v_addc_u32_e32 v42, vcc, v42, v39, vcc
	v_add_u32_e32 v45, vcc, 16, v41
	v_addc_u32_e32 v46, vcc, 0, v42, vcc
	flat_load_dwordx4 v[41:44], v[41:42]
	flat_load_dwordx4 v[45:48], v[45:46]
	v_mul_lo_i32 v53, v53, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v48, v48, v50
	v_xor_b32_e32 v26, v48, v26
	v_mul_hi_u32 v40, v40, v26
	v_xor_b32_e32 v53, v41, v54
	v_mul_lo_i32 v41, v49, s8
	v_mul_lo_i32 v49, v51, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v51, v52, s8
	v_xor_b32_e32 v47, v47, v41
	v_xor_b32_e32 v43, v43, v55
	v_cmp_ge_u32_e64 s[2:3], v26, v40
	v_subrev_u32_e32 v26, vcc, v40, v26
	v_cmp_le_u32_e32 vcc, s9, v26
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v26
	v_cndmask_b32_e64 v40, v40, v26, s[4:5]
	v_add_u32_e32 v26, vcc, s9, v26
	v_cndmask_b32_e64 v26, v40, v26, s[2:3]
	ds_write_b32 v36, v26 offset:4096
	ds_read_b32 v40, v0
	v_mov_b32_e32 v41, v25
	v_mov_b32_e32 v26, s15
	v_xor_b32_e32 v44, v44, v56
	v_mul_lo_i32 v52, v43, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[40:41]
	v_add_u32_e32 v40, vcc, s14, v40
	v_addc_u32_e32 v26, vcc, v26, v41, vcc
	v_add_u32_e32 v38, vcc, v40, v38
	v_addc_u32_e32 v39, vcc, v26, v39, vcc
	v_add_u32_e32 v43, vcc, 16, v38
	v_mul_lo_i32 v26, v53, s8
	v_mul_lo_i32 v53, v44, s8
	v_xor_b32_e32 v49, v46, v49
	v_xor_b32_e32 v51, v45, v51
	v_mul_lo_i32 v50, v42, s8
	v_addc_u32_e32 v44, vcc, 0, v39, vcc
	flat_load_dwordx4 v[39:42], v[38:39]
	flat_load_dwordx4 v[43:46], v[43:44]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_movk_i32 s2, 0x1000
	v_xor_b32_e32 v26, v39, v26
	v_mul_lo_i32 v39, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v26, v26, s8
	v_xor_b32_e32 v38, v42, v53
	v_mul_lo_i32 v42, v47, s8
	v_xor_b32_e32 v40, v40, v50
	v_mul_lo_i32 v47, v49, s8
	v_xor_b32_e32 v43, v43, v48
	v_xor_b32_e32 v26, v26, v40
	v_mul_lo_i32 v40, v43, s8
	v_mul_lo_i32 v26, v26, s8
	v_xor_b32_e32 v44, v44, v47
	v_xor_b32_e32 v41, v41, v52
	v_xor_b32_e32 v43, v40, v44
	v_xor_b32_e32 v40, v26, v41
	v_mul_lo_i32 v26, v43, s8
	v_xor_b32_e32 v42, v45, v42
	v_mul_lo_i32 v40, v40, s8
	v_xor_b32_e32 v39, v46, v39
	v_xor_b32_e32 v41, v26, v42
	v_mul_lo_i32 v41, v41, s8
	v_lshlrev_b32_e32 v26, 3, v24
	v_add_u32_e32 v26, vcc, v26, v3
	v_xor_b32_e32 v38, v40, v38
	v_xor_b32_e32 v39, v41, v39
	ds_write_b64 v26, v[38:39]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v38, v33, v28
	v_and_b32_e32 v28, v32, v34
	v_and_b32_e32 v39, v33, v35
	v_xor_b32_e32 v34, v34, v29
	v_and_b32_e32 v29, v32, v29
	v_xor_b32_e32 v35, v35, v30
	v_and_b32_e32 v33, v33, v30
	v_xor_b32_e32 v28, v27, v28
	v_xor_b32_e32 v30, v34, v29
	v_xor_b32_e32 v29, v35, v33
	v_xor_b32_e32 v27, v38, v39
	v_or_b32_e32 v32, s2, v36
	v_or_b32_e32 v33, s2, v37
	s_and_saveexec_b64 s[2:3], s[0:1]
	; mask branch BB1_8
BB1_7:                                  ; %if.then1083
	s_mov_b32 m0, -1
	ds_read2_b64 v[27:30], v3 offset1:1
	ds_read2_b64 v[4:7], v3 offset0:2 offset1:3
BB1_8:                                  ; %if.end1099
	s_or_b64 exec, exec, s[2:3]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_cmp_eq_u32_e64 s[0:1], 1, v24
	s_and_saveexec_b64 s[2:3], s[0:1]
	; mask branch BB1_10
BB1_9:                                  ; %if.then884.1
	s_mov_b32 m0, -1
	ds_write2_b64 v3, v[22:23], v[8:9] offset1:1
	ds_write2_b64 v3, v[12:13], v[14:15] offset0:2 offset1:3
	ds_write2_b64 v3, v[16:17], v[10:11] offset0:4 offset1:5
	ds_write2_b64 v3, v[18:19], v[20:21] offset0:6 offset1:7
BB1_10:                                 ; %if.end898.1
	s_or_b64 exec, exec, s[2:3]
	v_cvt_f32_u32_e32 v35, s9
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_mov_b32 m0, -1
	v_rcp_iflag_f32_e32 v35, v35
	ds_read_b32 v36, v3
	s_mov_b32 s8, 0x1000193
	ds_read2_b64 v[37:40], v31 offset1:1
	v_mul_f32_e32 v35, 0x4f800000, v35
	v_cvt_u32_f32_e32 v35, v35
	s_waitcnt lgkmcnt(1)
	v_mul_lo_i32 v34, v36, s8
	ds_read2_b64 v[41:44], v31 offset0:2 offset1:3
	s_waitcnt lgkmcnt(1)
	v_mul_lo_i32 v55, v37, s8
	v_mul_lo_i32 v53, v38, s8
	v_xor_b32_e32 v34, v34, v37
	v_mul_hi_u32 v37, v35, s9
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_mul_lo_i32 v54, v39, s8
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_mul_lo_i32 v37, v35, s9
	v_mov_b32_e32 v39, 0
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v40, v40, s8
	v_sub_u32_e32 v38, vcc, 0, v37
	v_cndmask_b32_e64 v37, v37, v38, s[2:3]
	v_mul_hi_u32 v37, v37, v35
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v42, v42, s8
	v_add_u32_e32 v38, vcc, v37, v35
	v_subrev_u32_e32 v35, vcc, v37, v35
	v_cndmask_b32_e64 v37, v35, v38, s[2:3]
	v_mul_hi_u32 v35, v37, v34
	v_mul_lo_i32 v35, v35, s9
	v_cmp_ge_u32_e64 s[2:3], v34, v35
	v_subrev_u32_e32 v34, vcc, v35, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v35, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_and_b32_e32 v38, v38, v35
	v_cmp_eq_u32_e64 s[2:3], 0, v35
	v_cmp_eq_u32_e64 s[4:5], 0, v38
	v_subrev_u32_e32 v35, vcc, s9, v34
	v_cndmask_b32_e64 v35, v35, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v35, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[34:35], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v34
	v_addc_u32_e32 v46, vcc, v45, v35, vcc
	v_lshlrev_b64 v[34:35], 5, v[24:25]
	v_add_u32_e32 v45, vcc, v38, v34
	v_addc_u32_e32 v46, vcc, v46, v35, vcc
	v_add_u32_e32 v49, vcc, 16, v45
	v_addc_u32_e32 v50, vcc, 0, v46, vcc
	flat_load_dwordx4 v[45:48], v[45:46]
	flat_load_dwordx4 v[49:52], v[49:50]
	v_mul_lo_i32 v38, v44, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v48, v48, v40
	v_xor_b32_e32 v52, v52, v38
	v_xor_b32_e32 v38, 1, v36
	v_mul_lo_i32 v38, v38, s8
	v_mul_lo_i32 v40, v43, s8
	v_xor_b32_e32 v46, v46, v53
	v_xor_b32_e32 v49, v49, v41
	v_xor_b32_e32 v38, v46, v38
	v_xor_b32_e32 v51, v51, v40
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v50, v50, v42
	v_mov_b32_e32 v42, s15
	v_xor_b32_e32 v45, v45, v55
	v_mul_lo_i32 v40, v40, s9
	v_xor_b32_e32 v47, v47, v54
	v_mul_lo_i32 v53, v45, s8
	v_mul_lo_i32 v54, v46, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v42, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v47, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v52, s8
	v_xor_b32_e32 v43, v43, v48
	v_mul_lo_i32 v48, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 2, v36
	v_mul_lo_i32 v38, v38, s8
	v_mul_lo_i32 v40, v51, s8
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v38, v42, v38
	v_xor_b32_e32 v50, v46, v40
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v53, s8
	v_mul_lo_i32 v53, v54, s8
	v_mul_lo_i32 v54, v42, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v52, v40, v52
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v53, v41, v53
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 3, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 4, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 5, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 6, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 7, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 8, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 9, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 10, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 11, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 12, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	s_mov_b32 m0, -1
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 13, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 14, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 15, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 16, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 17, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 18, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 19, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 20, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 21, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 22, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 23, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 24, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 25, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	s_mov_b32 m0, -1
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 26, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 27, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 28, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 29, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 30, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 31, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 32, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 33, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 34, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 35, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 36, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 37, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 38, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	s_mov_b32 m0, -1
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 39, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 40, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 41, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 42, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 43, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 44, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 45, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 46, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 47, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 48, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 49, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 50, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 51, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	s_mov_b32 m0, -1
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 52, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 53, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 54, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 55, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 56, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 57, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 58, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 59, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 60, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 61, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v54, v45, v38
	v_xor_b32_e32 v38, 62, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v57, v46, v48
	v_xor_b32_e32 v55, v44, v40
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v38, v57, v38
	v_mul_hi_u32 v40, v37, v38
	v_mul_lo_i32 v41, v50, s8
	v_mov_b32_e32 v44, s15
	v_mul_lo_i32 v46, v52, s8
	v_mul_lo_i32 v40, v40, s9
	v_xor_b32_e32 v56, v47, v41
	v_mul_lo_i32 v47, v42, s8
	v_mul_lo_i32 v52, v43, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	v_xor_b32_e32 v36, 63, v36
	v_mul_lo_i32 v36, v36, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[48:51], v[44:45]
	v_mul_lo_i32 v38, v53, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v44, v43, v52
	v_xor_b32_e32 v46, v41, v46
	v_mul_lo_i32 v41, v54, s8
	v_mul_lo_i32 v52, v56, s8
	v_xor_b32_e32 v45, v42, v47
	v_xor_b32_e32 v47, v40, v38
	v_xor_b32_e32 v42, v49, v41
	v_xor_b32_e32 v41, v51, v52
	v_xor_b32_e32 v36, v41, v36
	v_mul_hi_u32 v37, v37, v36
	v_mul_lo_i32 v38, v57, s8
	v_mul_lo_i32 v43, v55, s8
	v_mul_lo_i32 v49, v46, s8
	v_mul_lo_i32 v37, v37, s9
	v_xor_b32_e32 v40, v50, v38
	v_xor_b32_e32 v43, v48, v43
	v_mul_lo_i32 v48, v47, s8
	v_cmp_ge_u32_e64 s[2:3], v36, v37
	v_subrev_u32_e32 v36, vcc, v37, v36
	v_cmp_le_u32_e32 vcc, s9, v36
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v36
	v_cndmask_b32_e64 v37, v37, v36, s[4:5]
	v_add_u32_e32 v36, vcc, s9, v36
	v_cndmask_b32_e64 v36, v37, v36, s[2:3]
	ds_write_b32 v32, v36
	ds_read_b32 v38, v0
	v_mul_lo_i32 v50, v45, s8
	v_mul_lo_i32 v51, v44, s8
	v_mul_lo_i32 v40, v40, s8
	v_mul_lo_i32 v41, v41, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[36:37], 7, v[38:39]
	v_mov_b32_e32 v38, s15
	v_add_u32_e32 v36, vcc, s14, v36
	v_addc_u32_e32 v37, vcc, v38, v37, vcc
	v_add_u32_e32 v34, vcc, v36, v34
	v_addc_u32_e32 v35, vcc, v37, v35, vcc
	v_add_u32_e32 v38, vcc, 16, v34
	v_addc_u32_e32 v39, vcc, 0, v35, vcc
	flat_load_dwordx4 v[34:37], v[34:35]
	flat_load_dwordx4 v[44:47], v[38:39]
	v_mul_lo_i32 v38, v43, s8
	v_mul_lo_i32 v39, v42, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v34, v34, v48
	v_xor_b32_e32 v38, v44, v38
	v_mul_lo_i32 v34, v34, s8
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v35, v35, v49
	v_xor_b32_e32 v39, v45, v39
	v_xor_b32_e32 v34, v34, v35
	v_xor_b32_e32 v35, v38, v39
	v_mul_lo_i32 v34, v34, s8
	v_mul_lo_i32 v35, v35, s8
	v_xor_b32_e32 v36, v36, v50
	v_xor_b32_e32 v38, v46, v40
	v_xor_b32_e32 v34, v34, v36
	v_xor_b32_e32 v35, v35, v38
	v_mul_lo_i32 v34, v34, s8
	v_mul_lo_i32 v35, v35, s8
	v_xor_b32_e32 v37, v37, v51
	v_xor_b32_e32 v41, v47, v41
	v_xor_b32_e32 v34, v34, v37
	v_xor_b32_e32 v35, v35, v41
	ds_write_b64 v26, v[34:35]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_and_saveexec_b64 s[2:3], s[0:1]
	; mask branch BB1_12
BB1_11:                                 ; %if.then1083.1
	s_mov_b32 m0, -1
	ds_read2_b64 v[27:30], v3 offset1:1
	ds_read2_b64 v[4:7], v3 offset0:2 offset1:3
BB1_12:                                 ; %if.end1099.1
	s_or_b64 exec, exec, s[2:3]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_cmp_eq_u32_e64 s[0:1], 2, v24
	s_and_saveexec_b64 s[2:3], s[0:1]
	; mask branch BB1_14
BB1_13:                                 ; %if.then884.2
	s_mov_b32 m0, -1
	ds_write2_b64 v3, v[22:23], v[8:9] offset1:1
	ds_write2_b64 v3, v[12:13], v[14:15] offset0:2 offset1:3
	ds_write2_b64 v3, v[16:17], v[10:11] offset0:4 offset1:5
	ds_write2_b64 v3, v[18:19], v[20:21] offset0:6 offset1:7
BB1_14:                                 ; %if.end898.2
	s_or_b64 exec, exec, s[2:3]
	v_cvt_f32_u32_e32 v35, s9
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_mov_b32 m0, -1
	v_rcp_iflag_f32_e32 v35, v35
	ds_read_b32 v36, v3
	s_mov_b32 s8, 0x1000193
	ds_read2_b64 v[37:40], v31 offset1:1
	v_mul_f32_e32 v35, 0x4f800000, v35
	v_cvt_u32_f32_e32 v35, v35
	s_waitcnt lgkmcnt(1)
	v_mul_lo_i32 v34, v36, s8
	ds_read2_b64 v[41:44], v31 offset0:2 offset1:3
	s_waitcnt lgkmcnt(1)
	v_mul_lo_i32 v55, v37, s8
	v_mul_lo_i32 v53, v38, s8
	v_xor_b32_e32 v34, v34, v37
	v_mul_hi_u32 v37, v35, s9
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_mul_lo_i32 v54, v39, s8
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_mul_lo_i32 v37, v35, s9
	v_mov_b32_e32 v39, 0
	v_mov_b32_e32 v45, s15
	v_mul_lo_i32 v40, v40, s8
	v_sub_u32_e32 v38, vcc, 0, v37
	v_cndmask_b32_e64 v37, v37, v38, s[2:3]
	v_mul_hi_u32 v37, v37, v35
	v_mul_lo_i32 v41, v41, s8
	v_mul_lo_i32 v42, v42, s8
	v_add_u32_e32 v38, vcc, v37, v35
	v_subrev_u32_e32 v35, vcc, v37, v35
	v_cndmask_b32_e64 v37, v35, v38, s[2:3]
	v_mul_hi_u32 v35, v37, v34
	v_mul_lo_i32 v35, v35, s9
	v_cmp_ge_u32_e64 s[2:3], v34, v35
	v_subrev_u32_e32 v34, vcc, v35, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v35, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_and_b32_e32 v38, v38, v35
	v_cmp_eq_u32_e64 s[2:3], 0, v35
	v_cmp_eq_u32_e64 s[4:5], 0, v38
	v_subrev_u32_e32 v35, vcc, s9, v34
	v_cndmask_b32_e64 v35, v35, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v35, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[34:35], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v34
	v_addc_u32_e32 v46, vcc, v45, v35, vcc
	v_lshlrev_b64 v[34:35], 5, v[24:25]
	v_add_u32_e32 v45, vcc, v38, v34
	v_addc_u32_e32 v46, vcc, v46, v35, vcc
	v_add_u32_e32 v49, vcc, 16, v45
	v_addc_u32_e32 v50, vcc, 0, v46, vcc
	flat_load_dwordx4 v[45:48], v[45:46]
	flat_load_dwordx4 v[49:52], v[49:50]
	v_mul_lo_i32 v38, v44, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v48, v48, v40
	v_xor_b32_e32 v52, v52, v38
	v_xor_b32_e32 v38, 1, v36
	v_mul_lo_i32 v38, v38, s8
	v_mul_lo_i32 v40, v43, s8
	v_xor_b32_e32 v46, v46, v53
	v_xor_b32_e32 v49, v49, v41
	v_xor_b32_e32 v38, v46, v38
	v_xor_b32_e32 v51, v51, v40
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v50, v50, v42
	v_mov_b32_e32 v42, s15
	v_xor_b32_e32 v45, v45, v55
	v_mul_lo_i32 v40, v40, s9
	v_xor_b32_e32 v47, v47, v54
	v_mul_lo_i32 v53, v45, s8
	v_mul_lo_i32 v54, v46, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v42, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v47, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v52, s8
	v_xor_b32_e32 v43, v43, v48
	v_mul_lo_i32 v48, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 2, v36
	v_mul_lo_i32 v38, v38, s8
	v_mul_lo_i32 v40, v51, s8
	v_xor_b32_e32 v54, v41, v54
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v38, v42, v38
	v_xor_b32_e32 v50, v46, v40
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v53, s8
	v_mul_lo_i32 v53, v54, s8
	v_mul_lo_i32 v54, v42, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v52, v40, v52
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v53, v41, v53
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 3, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 4, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 5, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 6, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 7, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 8, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 9, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 10, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 11, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 12, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	s_mov_b32 m0, -1
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 13, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 14, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 15, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 16, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 17, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 18, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 19, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 20, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 21, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 22, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 23, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 24, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 25, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	s_mov_b32 m0, -1
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 26, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 27, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 28, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 29, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 30, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 31, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 32, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 33, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 34, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 35, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 36, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 37, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 38, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	s_mov_b32 m0, -1
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 39, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 40, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 41, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 42, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 43, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 44, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 45, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 46, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 47, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 48, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 49, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 50, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 51, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	s_mov_b32 m0, -1
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 52, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 53, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v45, v38
	v_xor_b32_e32 v38, 54, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v46, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v44, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v47, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v46, v38
	v_xor_b32_e32 v38, 55, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v47, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v45, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v44, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 56, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v48, v51, s8
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v38, v38, v53
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v48, v44, v48
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v52, s8
	v_mul_lo_i32 v52, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v52
	v_xor_b32_e32 v52, v41, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 57, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v52, v38
	v_mul_hi_u32 v40, v37, v38
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_mul_lo_i32 v48, v48, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 58, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v42, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v42, s8
	v_mul_lo_i32 v53, v53, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	v_mul_lo_i32 v48, v48, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v43, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v43, v43, v38
	v_mul_lo_i32 v38, v49, s8
	v_xor_b32_e32 v53, v40, v53
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 59, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v38, v43, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v42, v42, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v48, v48, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v49, s8
	v_mul_lo_i32 v40, v50, s8
	v_xor_b32_e32 v48, v44, v48
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v47, v38
	v_xor_b32_e32 v38, 60, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v50, v46, v40
	v_mul_lo_i32 v41, v51, s8
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v45, v41
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v49, v44, v38
	v_xor_b32_e32 v38, 61, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v48, v45, v48
	v_mul_lo_i32 v41, v50, s8
	v_xor_b32_e32 v50, v47, v40
	v_xor_b32_e32 v38, v48, v38
	v_mul_hi_u32 v40, v37, v38
	v_xor_b32_e32 v51, v46, v41
	v_mov_b32_e32 v44, s15
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v40, v40, s9
	v_mul_lo_i32 v54, v43, s8
	v_mul_lo_i32 v52, v52, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_mul_lo_i32 v38, v53, s8
	v_mul_lo_i32 v53, v42, s8
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[44:47], v[44:45]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v42, v42, v53
	v_xor_b32_e32 v53, v40, v38
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v43, v43, v54
	v_mul_lo_i32 v48, v51, s8
	v_mul_lo_i32 v40, v49, s8
	v_xor_b32_e32 v54, v45, v38
	v_xor_b32_e32 v38, 62, v36
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v57, v46, v48
	v_xor_b32_e32 v55, v44, v40
	v_xor_b32_e32 v52, v41, v52
	v_xor_b32_e32 v38, v57, v38
	v_mul_hi_u32 v40, v37, v38
	v_mul_lo_i32 v41, v50, s8
	v_mov_b32_e32 v44, s15
	v_mul_lo_i32 v46, v52, s8
	v_mul_lo_i32 v40, v40, s9
	v_xor_b32_e32 v56, v47, v41
	v_mul_lo_i32 v47, v42, s8
	v_mul_lo_i32 v52, v43, s8
	v_cmp_ge_u32_e64 s[2:3], v38, v40
	v_subrev_u32_e32 v38, vcc, v40, v38
	v_cmp_le_u32_e32 vcc, s9, v38
	v_cndmask_b32_e64 v40, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v40
	v_and_b32_e32 v40, v41, v40
	v_cmp_eq_u32_e64 s[4:5], 0, v40
	v_subrev_u32_e32 v40, vcc, s9, v38
	v_cndmask_b32_e64 v40, v40, v38, s[4:5]
	v_add_u32_e32 v38, vcc, s9, v38
	v_cndmask_b32_e64 v38, v40, v38, s[2:3]
	ds_write_b32 v32, v38
	ds_read_b32 v38, v0
	v_xor_b32_e32 v36, 63, v36
	v_mul_lo_i32 v36, v36, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[40:41], 7, v[38:39]
	v_add_u32_e32 v38, vcc, s14, v40
	v_addc_u32_e32 v41, vcc, v44, v41, vcc
	v_add_u32_e32 v40, vcc, v38, v34
	v_addc_u32_e32 v41, vcc, v41, v35, vcc
	v_add_u32_e32 v44, vcc, 16, v40
	v_addc_u32_e32 v45, vcc, 0, v41, vcc
	flat_load_dwordx4 v[40:43], v[40:41]
	flat_load_dwordx4 v[48:51], v[44:45]
	v_mul_lo_i32 v38, v53, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v44, v43, v52
	v_xor_b32_e32 v46, v41, v46
	v_mul_lo_i32 v41, v54, s8
	v_mul_lo_i32 v52, v56, s8
	v_xor_b32_e32 v45, v42, v47
	v_xor_b32_e32 v47, v40, v38
	v_xor_b32_e32 v42, v49, v41
	v_xor_b32_e32 v41, v51, v52
	v_xor_b32_e32 v36, v41, v36
	v_mul_hi_u32 v37, v37, v36
	v_mul_lo_i32 v38, v57, s8
	v_mul_lo_i32 v43, v55, s8
	v_mul_lo_i32 v49, v46, s8
	v_mul_lo_i32 v37, v37, s9
	v_xor_b32_e32 v40, v50, v38
	v_xor_b32_e32 v43, v48, v43
	v_mul_lo_i32 v48, v47, s8
	v_cmp_ge_u32_e64 s[2:3], v36, v37
	v_subrev_u32_e32 v36, vcc, v37, v36
	v_cmp_le_u32_e32 vcc, s9, v36
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v36
	v_cndmask_b32_e64 v37, v37, v36, s[4:5]
	v_add_u32_e32 v36, vcc, s9, v36
	v_cndmask_b32_e64 v36, v37, v36, s[2:3]
	ds_write_b32 v32, v36
	ds_read_b32 v38, v0
	v_mul_lo_i32 v50, v45, s8
	v_mul_lo_i32 v51, v44, s8
	v_mul_lo_i32 v40, v40, s8
	v_mul_lo_i32 v41, v41, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[36:37], 7, v[38:39]
	v_mov_b32_e32 v38, s15
	v_add_u32_e32 v36, vcc, s14, v36
	v_addc_u32_e32 v37, vcc, v38, v37, vcc
	v_add_u32_e32 v34, vcc, v36, v34
	v_addc_u32_e32 v35, vcc, v37, v35, vcc
	v_add_u32_e32 v38, vcc, 16, v34
	v_addc_u32_e32 v39, vcc, 0, v35, vcc
	flat_load_dwordx4 v[34:37], v[34:35]
	flat_load_dwordx4 v[44:47], v[38:39]
	v_mul_lo_i32 v38, v43, s8
	v_mul_lo_i32 v39, v42, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v34, v34, v48
	v_xor_b32_e32 v38, v44, v38
	v_mul_lo_i32 v34, v34, s8
	v_mul_lo_i32 v38, v38, s8
	v_xor_b32_e32 v35, v35, v49
	v_xor_b32_e32 v39, v45, v39
	v_xor_b32_e32 v34, v34, v35
	v_xor_b32_e32 v35, v38, v39
	v_mul_lo_i32 v34, v34, s8
	v_mul_lo_i32 v35, v35, s8
	v_xor_b32_e32 v36, v36, v50
	v_xor_b32_e32 v38, v46, v40
	v_xor_b32_e32 v34, v34, v36
	v_xor_b32_e32 v35, v35, v38
	v_mul_lo_i32 v34, v34, s8
	v_mul_lo_i32 v35, v35, s8
	v_xor_b32_e32 v37, v37, v51
	v_xor_b32_e32 v41, v47, v41
	v_xor_b32_e32 v34, v34, v37
	v_xor_b32_e32 v35, v35, v41
	ds_write_b64 v26, v[34:35]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_and_saveexec_b64 s[2:3], s[0:1]
	; mask branch BB1_16
BB1_15:                                 ; %if.then1083.2
	s_mov_b32 m0, -1
	ds_read2_b64 v[27:30], v3 offset1:1
	ds_read2_b64 v[4:7], v3 offset0:2 offset1:3
BB1_16:                                 ; %if.end1099.2
	s_or_b64 exec, exec, s[2:3]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_cmp_eq_u32_e64 s[0:1], 3, v24
	s_and_saveexec_b64 s[2:3], s[0:1]
	; mask branch BB1_18
BB1_17:                                 ; %if.then884.3
	s_mov_b32 m0, -1
	ds_write2_b64 v3, v[22:23], v[8:9] offset1:1
	ds_write2_b64 v3, v[12:13], v[14:15] offset0:2 offset1:3
	ds_write2_b64 v3, v[16:17], v[10:11] offset0:4 offset1:5
	ds_write2_b64 v3, v[18:19], v[20:21] offset0:6 offset1:7
BB1_18:                                 ; %if.end898.3
	s_or_b64 exec, exec, s[2:3]
	v_cvt_f32_u32_e32 v34, s9
	s_waitcnt lgkmcnt(0)
	s_mov_b32 m0, -1
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_rcp_iflag_f32_e32 v34, v34
	ds_read_b32 v36, v3
	s_mov_b32 s8, 0x1000193
	v_lshlrev_b64 v[24:25], 5, v[24:25]
	v_mul_f32_e32 v34, 0x4f800000, v34
	v_cvt_u32_f32_e32 v34, v34
	s_waitcnt lgkmcnt(0)
	v_mul_lo_i32 v45, v36, s8
	v_mul_lo_i32 v35, v34, s9
	v_mul_hi_u32 v37, v34, s9
	v_sub_u32_e32 v38, vcc, 0, v35
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_cndmask_b32_e64 v35, v35, v38, s[2:3]
	ds_read2_b64 v[37:40], v31 offset1:1
	v_mul_hi_u32 v35, v35, v34
	ds_read2_b64 v[41:44], v31 offset0:2 offset1:3
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	v_add_u32_e32 v31, vcc, v35, v34
	v_subrev_u32_e32 v34, vcc, v35, v34
	v_xor_b32_e32 v45, v45, v37
	v_cndmask_b32_e64 v31, v34, v31, s[2:3]
	v_mul_hi_u32 v34, v31, v45
	v_mul_lo_i32 v49, v38, s8
	v_mul_lo_i32 v52, v37, s8
	v_mul_lo_i32 v50, v39, s8
	v_mul_lo_i32 v34, v34, s9
	v_mov_b32_e32 v39, s15
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v41, v41, s8
	v_cmp_ge_u32_e32 vcc, v45, v34
	v_cndmask_b32_e64 v35, 0, -1, vcc
	v_subrev_u32_e32 v34, vcc, v34, v45
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v45, 0, -1, vcc
	v_and_b32_e32 v45, v45, v35
	v_cmp_eq_u32_e64 s[2:3], 0, v45
	v_subrev_u32_e32 v45, vcc, s9, v34
	v_cndmask_b32_e64 v45, v45, v34, s[2:3]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cmp_eq_u32_e32 vcc, 0, v35
	v_cndmask_b32_e32 v34, v45, v34, vcc
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	v_mov_b32_e32 v35, 0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v39, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v45, vcc, 16, v37
	v_addc_u32_e32 v46, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[45:48], v[45:46]
	v_mul_lo_i32 v34, v44, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v52, v37, v52
	v_xor_b32_e32 v48, v48, v34
	v_xor_b32_e32 v34, 1, v36
	v_mul_lo_i32 v34, v34, s8
	v_mul_lo_i32 v37, v43, s8
	v_xor_b32_e32 v49, v38, v49
	v_mul_lo_i32 v38, v42, s8
	v_xor_b32_e32 v34, v49, v34
	v_xor_b32_e32 v47, v47, v37
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v46, v46, v38
	v_xor_b32_e32 v45, v45, v41
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v37, v37, s9
	v_xor_b32_e32 v40, v40, v51
	v_xor_b32_e32 v39, v39, v50
	v_mul_lo_i32 v50, v40, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v51, v52, s8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v34
	v_mul_lo_i32 v34, v48, s8
	v_xor_b32_e32 v49, v38, v49
	v_mul_lo_i32 v38, v46, s8
	v_xor_b32_e32 v51, v37, v51
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 2, v36
	v_mul_lo_i32 v34, v34, s8
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v39, v34
	v_xor_b32_e32 v47, v43, v37
	v_mul_hi_u32 v37, v31, v34
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v50
	v_mul_lo_i32 v50, v51, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v39, s8
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v40, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 3, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v40, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v39, v39, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v45, v45, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v46, s8
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 4, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v41, v34
	v_xor_b32_e32 v34, 5, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v42, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v44, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v43, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v42, v34
	v_xor_b32_e32 v34, 6, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v43, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v41, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v44, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v43, v34
	v_xor_b32_e32 v34, 7, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v44, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v42, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v41, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 8, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v45, v48, s8
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v34, v34, v50
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v45, v41, v45
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v49, s8
	v_mul_lo_i32 v49, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v49
	v_xor_b32_e32 v49, v38, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 9, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v49, v34
	v_mul_hi_u32 v37, v31, v34
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 10, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v39, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v39, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v40, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 11, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v40, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v39, v39, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v45, v45, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v46, s8
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 12, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	s_mov_b32 m0, -1
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v41, v34
	v_xor_b32_e32 v34, 13, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v42, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v44, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v43, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v42, v34
	v_xor_b32_e32 v34, 14, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v43, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v41, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v44, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v43, v34
	v_xor_b32_e32 v34, 15, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v44, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v42, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v41, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 16, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v45, v48, s8
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v34, v34, v50
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v45, v41, v45
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v49, s8
	v_mul_lo_i32 v49, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v49
	v_xor_b32_e32 v49, v38, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 17, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v49, v34
	v_mul_hi_u32 v37, v31, v34
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 18, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v39, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v39, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v40, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 19, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v40, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v39, v39, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v45, v45, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v46, s8
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 20, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v41, v34
	v_xor_b32_e32 v34, 21, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v42, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v44, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v43, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v42, v34
	v_xor_b32_e32 v34, 22, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v43, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v41, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v44, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v43, v34
	v_xor_b32_e32 v34, 23, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v44, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v42, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v41, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 24, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v45, v48, s8
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v34, v34, v50
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v45, v41, v45
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v49, s8
	v_mul_lo_i32 v49, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v49
	v_xor_b32_e32 v49, v38, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 25, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v49, v34
	v_mul_hi_u32 v37, v31, v34
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	s_mov_b32 m0, -1
	ds_read_b32 v34, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 26, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v39, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v39, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v0
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v40, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 27, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v40, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v39, v39, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v45, v45, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v46, s8
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 28, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v41, v34
	v_xor_b32_e32 v34, 29, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v42, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v44, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v43, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v42, v34
	v_xor_b32_e32 v34, 30, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v43, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v41, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v44, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v43, v34
	v_xor_b32_e32 v34, 31, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v44, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v42, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v41, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v0
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 32, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v45, v48, s8
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v34, v34, v50
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v45, v41, v45
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v49, s8
	v_mul_lo_i32 v49, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v49
	v_xor_b32_e32 v49, v38, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 33, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v49, v34
	v_mul_hi_u32 v37, v31, v34
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 34, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v39, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v39, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v40, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 35, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v40, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v39, v39, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v45, v45, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v46, s8
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 36, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v41, v34
	v_xor_b32_e32 v34, 37, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v42, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v44, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v43, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v42, v34
	v_xor_b32_e32 v34, 38, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v43, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v41, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v44, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	s_mov_b32 m0, -1
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v43, v34
	v_xor_b32_e32 v34, 39, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v44, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v42, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v41, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 40, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v45, v48, s8
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v34, v34, v50
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v45, v41, v45
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v49, s8
	v_mul_lo_i32 v49, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v49
	v_xor_b32_e32 v49, v38, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 41, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v49, v34
	v_mul_hi_u32 v37, v31, v34
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 42, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v39, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v39, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v40, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 43, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v40, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v39, v39, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v45, v45, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v46, s8
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 44, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v41, v34
	v_xor_b32_e32 v34, 45, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v42, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v44, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v43, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v42, v34
	v_xor_b32_e32 v34, 46, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v43, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v41, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v44, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v43, v34
	v_xor_b32_e32 v34, 47, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v44, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v42, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v41, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:4
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 48, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v45, v48, s8
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v34, v34, v50
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v45, v41, v45
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v49, s8
	v_mul_lo_i32 v49, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v49
	v_xor_b32_e32 v49, v38, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 49, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v49, v34
	v_mul_hi_u32 v37, v31, v34
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_mul_lo_i32 v45, v45, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 50, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v39, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v39, s8
	v_mul_lo_i32 v50, v50, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v40, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v34
	v_mul_lo_i32 v34, v46, s8
	v_xor_b32_e32 v50, v37, v50
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 51, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v34, v40, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v39, v39, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v45, v45, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	s_mov_b32 m0, -1
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v46, s8
	v_mul_lo_i32 v37, v47, s8
	v_xor_b32_e32 v45, v41, v45
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v44, v34
	v_xor_b32_e32 v34, 52, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v43, v37
	v_mul_lo_i32 v38, v48, s8
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v42, v38
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v41, v34
	v_xor_b32_e32 v34, 53, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v42, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v44, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v43, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v50, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v48, s8
	v_mul_lo_i32 v37, v46, s8
	v_xor_b32_e32 v49, v38, v49
	v_xor_b32_e32 v46, v42, v34
	v_xor_b32_e32 v34, 54, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v45, v43, v45
	v_mul_lo_i32 v38, v47, s8
	v_xor_b32_e32 v47, v41, v37
	v_xor_b32_e32 v34, v45, v34
	v_mul_hi_u32 v37, v31, v34
	v_xor_b32_e32 v48, v44, v38
	v_mov_b32_e32 v41, s15
	v_xor_b32_e32 v40, v40, v51
	v_mul_lo_i32 v37, v37, s9
	v_mul_lo_i32 v51, v40, s8
	v_mul_lo_i32 v49, v49, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v37
	v_subrev_u32_e32 v34, vcc, v37, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v37, 0, -1, s[2:3]
	v_cndmask_b32_e64 v38, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v37
	v_and_b32_e32 v37, v38, v37
	v_cmp_eq_u32_e64 s[4:5], 0, v37
	v_subrev_u32_e32 v37, vcc, s9, v34
	v_cndmask_b32_e64 v37, v37, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v37, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[37:38], 7, v[34:35]
	v_add_u32_e32 v34, vcc, s14, v37
	v_addc_u32_e32 v38, vcc, v41, v38, vcc
	v_add_u32_e32 v37, vcc, v34, v24
	v_addc_u32_e32 v38, vcc, v38, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v37
	v_mul_lo_i32 v34, v50, s8
	v_mul_lo_i32 v50, v39, s8
	v_addc_u32_e32 v42, vcc, 0, v38, vcc
	flat_load_dwordx4 v[37:40], v[37:38]
	flat_load_dwordx4 v[41:44], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v37, v37, v34
	v_mul_lo_i32 v34, v45, s8
	v_mul_lo_i32 v45, v46, s8
	v_mul_lo_i32 v46, v47, s8
	v_mul_lo_i32 v47, v48, s8
	v_xor_b32_e32 v48, v43, v34
	v_xor_b32_e32 v34, 55, v36
	v_mul_lo_i32 v34, v34, s8
	v_xor_b32_e32 v47, v44, v47
	v_xor_b32_e32 v46, v41, v46
	v_xor_b32_e32 v45, v42, v45
	v_xor_b32_e32 v34, v47, v34
	v_mul_hi_u32 v41, v31, v34
	v_xor_b32_e32 v40, v40, v51
	v_xor_b32_e32 v39, v39, v50
	v_xor_b32_e32 v38, v38, v49
	v_mul_lo_i32 v41, v41, s9
	v_mul_lo_i32 v49, v37, s8
	v_mul_lo_i32 v50, v38, s8
	v_mul_lo_i32 v51, v39, s8
	v_cmp_ge_u32_e64 s[2:3], v34, v41
	v_subrev_u32_e32 v34, vcc, v41, v34
	v_cmp_le_u32_e32 vcc, s9, v34
	v_cndmask_b32_e64 v41, 0, -1, s[2:3]
	v_cndmask_b32_e64 v42, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v41
	v_and_b32_e32 v41, v42, v41
	v_cmp_eq_u32_e64 s[4:5], 0, v41
	v_subrev_u32_e32 v41, vcc, s9, v34
	v_cndmask_b32_e64 v41, v41, v34, s[4:5]
	v_add_u32_e32 v34, vcc, s9, v34
	v_cndmask_b32_e64 v34, v41, v34, s[2:3]
	ds_write_b32 v32, v34
	ds_read_b32 v34, v33 offset:8
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v52, v40, s8
	v_mul_lo_i32 v46, v46, s8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[33:34], 7, v[34:35]
	v_add_u32_e32 v33, vcc, s14, v33
	v_addc_u32_e32 v34, vcc, v41, v34, vcc
	v_add_u32_e32 v33, vcc, v33, v24
	v_addc_u32_e32 v34, vcc, v34, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v33
	v_addc_u32_e32 v42, vcc, 0, v34, vcc
	flat_load_dwordx4 v[37:40], v[33:34]
	flat_load_dwordx4 v[41:44], v[41:42]
	v_mul_lo_i32 v33, v47, s8
	v_mul_lo_i32 v34, v48, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v37, v37, v49
	v_xor_b32_e32 v47, v44, v33
	v_xor_b32_e32 v33, 56, v36
	v_mul_lo_i32 v33, v33, s8
	v_xor_b32_e32 v48, v43, v34
	v_xor_b32_e32 v46, v41, v46
	v_xor_b32_e32 v45, v42, v45
	v_xor_b32_e32 v33, v33, v37
	v_mul_hi_u32 v34, v31, v33
	v_xor_b32_e32 v40, v40, v52
	v_xor_b32_e32 v39, v39, v51
	v_xor_b32_e32 v38, v38, v50
	v_mul_lo_i32 v34, v34, s9
	v_mul_lo_i32 v49, v38, s8
	v_mul_lo_i32 v50, v39, s8
	v_mul_lo_i32 v51, v40, s8
	v_cmp_ge_u32_e64 s[2:3], v33, v34
	v_subrev_u32_e32 v33, vcc, v34, v33
	v_cmp_le_u32_e32 vcc, s9, v33
	v_cndmask_b32_e64 v34, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v34
	v_and_b32_e32 v34, v41, v34
	v_cmp_eq_u32_e64 s[4:5], 0, v34
	v_subrev_u32_e32 v34, vcc, s9, v33
	v_cndmask_b32_e64 v34, v34, v33, s[4:5]
	v_add_u32_e32 v33, vcc, s9, v33
	v_cndmask_b32_e64 v33, v34, v33, s[2:3]
	ds_write_b32 v32, v33
	ds_read_b32 v34, v0
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v52, v37, s8
	v_mul_lo_i32 v46, v46, s8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[33:34], 7, v[34:35]
	v_add_u32_e32 v33, vcc, s14, v33
	v_addc_u32_e32 v34, vcc, v41, v34, vcc
	v_add_u32_e32 v33, vcc, v33, v24
	v_addc_u32_e32 v34, vcc, v34, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v33
	v_addc_u32_e32 v42, vcc, 0, v34, vcc
	flat_load_dwordx4 v[37:40], v[33:34]
	flat_load_dwordx4 v[41:44], v[41:42]
	v_mul_lo_i32 v33, v47, s8
	v_mul_lo_i32 v34, v48, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v38, v38, v49
	v_xor_b32_e32 v47, v44, v33
	v_xor_b32_e32 v33, 57, v36
	v_mul_lo_i32 v33, v33, s8
	v_xor_b32_e32 v48, v43, v34
	v_xor_b32_e32 v46, v41, v46
	v_xor_b32_e32 v45, v42, v45
	v_xor_b32_e32 v33, v38, v33
	v_mul_hi_u32 v34, v31, v33
	v_xor_b32_e32 v37, v37, v52
	v_xor_b32_e32 v40, v40, v51
	v_xor_b32_e32 v39, v39, v50
	v_mul_lo_i32 v34, v34, s9
	v_mul_lo_i32 v49, v39, s8
	v_mul_lo_i32 v50, v40, s8
	v_mul_lo_i32 v51, v37, s8
	v_cmp_ge_u32_e64 s[2:3], v33, v34
	v_subrev_u32_e32 v33, vcc, v34, v33
	v_cmp_le_u32_e32 vcc, s9, v33
	v_cndmask_b32_e64 v34, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v34
	v_and_b32_e32 v34, v41, v34
	v_cmp_eq_u32_e64 s[4:5], 0, v34
	v_subrev_u32_e32 v34, vcc, s9, v33
	v_cndmask_b32_e64 v34, v34, v33, s[4:5]
	v_add_u32_e32 v33, vcc, s9, v33
	v_cndmask_b32_e64 v33, v34, v33, s[2:3]
	ds_write_b32 v32, v33
	ds_read_b32 v34, v0
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v52, v38, s8
	v_mul_lo_i32 v46, v46, s8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[33:34], 7, v[34:35]
	v_add_u32_e32 v33, vcc, s14, v33
	v_addc_u32_e32 v34, vcc, v41, v34, vcc
	v_add_u32_e32 v33, vcc, v33, v24
	v_addc_u32_e32 v34, vcc, v34, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v33
	v_addc_u32_e32 v42, vcc, 0, v34, vcc
	flat_load_dwordx4 v[37:40], v[33:34]
	flat_load_dwordx4 v[41:44], v[41:42]
	v_mul_lo_i32 v33, v47, s8
	v_mul_lo_i32 v34, v48, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v39, v39, v49
	v_xor_b32_e32 v47, v44, v33
	v_xor_b32_e32 v33, 58, v36
	v_mul_lo_i32 v33, v33, s8
	v_xor_b32_e32 v48, v43, v34
	v_xor_b32_e32 v46, v41, v46
	v_xor_b32_e32 v45, v42, v45
	v_xor_b32_e32 v33, v39, v33
	v_mul_hi_u32 v34, v31, v33
	v_xor_b32_e32 v38, v38, v52
	v_xor_b32_e32 v37, v37, v51
	v_xor_b32_e32 v40, v40, v50
	v_mul_lo_i32 v34, v34, s9
	v_mul_lo_i32 v49, v40, s8
	v_mul_lo_i32 v50, v37, s8
	v_mul_lo_i32 v51, v38, s8
	v_cmp_ge_u32_e64 s[2:3], v33, v34
	v_subrev_u32_e32 v33, vcc, v34, v33
	v_cmp_le_u32_e32 vcc, s9, v33
	v_cndmask_b32_e64 v34, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v34
	v_and_b32_e32 v34, v41, v34
	v_cmp_eq_u32_e64 s[4:5], 0, v34
	v_subrev_u32_e32 v34, vcc, s9, v33
	v_cndmask_b32_e64 v34, v34, v33, s[4:5]
	v_add_u32_e32 v33, vcc, s9, v33
	v_cndmask_b32_e64 v33, v34, v33, s[2:3]
	ds_write_b32 v32, v33
	ds_read_b32 v34, v0
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v52, v39, s8
	v_mul_lo_i32 v46, v46, s8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[33:34], 7, v[34:35]
	v_add_u32_e32 v33, vcc, s14, v33
	v_addc_u32_e32 v34, vcc, v41, v34, vcc
	v_add_u32_e32 v33, vcc, v33, v24
	v_addc_u32_e32 v34, vcc, v34, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v33
	v_addc_u32_e32 v42, vcc, 0, v34, vcc
	flat_load_dwordx4 v[37:40], v[33:34]
	flat_load_dwordx4 v[41:44], v[41:42]
	v_mul_lo_i32 v33, v47, s8
	v_mul_lo_i32 v34, v48, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v49
	v_xor_b32_e32 v47, v44, v33
	v_xor_b32_e32 v33, 59, v36
	v_mul_lo_i32 v33, v33, s8
	v_xor_b32_e32 v48, v43, v34
	v_xor_b32_e32 v46, v41, v46
	v_xor_b32_e32 v45, v42, v45
	v_xor_b32_e32 v33, v40, v33
	v_mul_hi_u32 v34, v31, v33
	v_xor_b32_e32 v39, v39, v52
	v_xor_b32_e32 v38, v38, v51
	v_xor_b32_e32 v37, v37, v50
	v_mul_lo_i32 v34, v34, s9
	v_mul_lo_i32 v49, v37, s8
	v_mul_lo_i32 v50, v38, s8
	v_mul_lo_i32 v51, v39, s8
	v_cmp_ge_u32_e64 s[2:3], v33, v34
	v_subrev_u32_e32 v33, vcc, v34, v33
	v_cmp_le_u32_e32 vcc, s9, v33
	v_cndmask_b32_e64 v34, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v34
	v_and_b32_e32 v34, v41, v34
	v_cmp_eq_u32_e64 s[4:5], 0, v34
	v_subrev_u32_e32 v34, vcc, s9, v33
	v_cndmask_b32_e64 v34, v34, v33, s[4:5]
	v_add_u32_e32 v33, vcc, s9, v33
	v_cndmask_b32_e64 v33, v34, v33, s[2:3]
	ds_write_b32 v32, v33
	ds_read_b32 v34, v0
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v52, v40, s8
	v_mul_lo_i32 v46, v46, s8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[33:34], 7, v[34:35]
	v_add_u32_e32 v33, vcc, s14, v33
	v_addc_u32_e32 v34, vcc, v41, v34, vcc
	v_add_u32_e32 v33, vcc, v33, v24
	v_addc_u32_e32 v34, vcc, v34, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v33
	v_addc_u32_e32 v42, vcc, 0, v34, vcc
	flat_load_dwordx4 v[37:40], v[33:34]
	flat_load_dwordx4 v[41:44], v[41:42]
	v_mul_lo_i32 v33, v47, s8
	v_mul_lo_i32 v34, v48, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v52
	v_xor_b32_e32 v47, v44, v33
	v_xor_b32_e32 v33, 60, v36
	v_mul_lo_i32 v33, v33, s8
	v_xor_b32_e32 v46, v41, v46
	v_xor_b32_e32 v48, v43, v34
	v_xor_b32_e32 v39, v39, v51
	v_xor_b32_e32 v33, v46, v33
	v_mul_hi_u32 v34, v31, v33
	v_xor_b32_e32 v38, v38, v50
	v_xor_b32_e32 v37, v37, v49
	v_xor_b32_e32 v45, v42, v45
	v_mul_lo_i32 v34, v34, s9
	v_mul_lo_i32 v49, v37, s8
	v_mul_lo_i32 v50, v38, s8
	v_mul_lo_i32 v51, v39, s8
	v_cmp_ge_u32_e64 s[2:3], v33, v34
	v_subrev_u32_e32 v33, vcc, v34, v33
	v_cmp_le_u32_e32 vcc, s9, v33
	v_cndmask_b32_e64 v34, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v34
	v_and_b32_e32 v34, v41, v34
	v_cmp_eq_u32_e64 s[4:5], 0, v34
	v_subrev_u32_e32 v34, vcc, s9, v33
	v_cndmask_b32_e64 v34, v34, v33, s[4:5]
	v_add_u32_e32 v33, vcc, s9, v33
	v_cndmask_b32_e64 v33, v34, v33, s[2:3]
	ds_write_b32 v32, v33
	ds_read_b32 v34, v0
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v52, v40, s8
	v_mul_lo_i32 v45, v45, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[33:34], 7, v[34:35]
	v_add_u32_e32 v33, vcc, s14, v33
	v_addc_u32_e32 v34, vcc, v41, v34, vcc
	v_add_u32_e32 v33, vcc, v33, v24
	v_addc_u32_e32 v34, vcc, v34, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v33
	v_addc_u32_e32 v42, vcc, 0, v34, vcc
	flat_load_dwordx4 v[37:40], v[33:34]
	flat_load_dwordx4 v[41:44], v[41:42]
	v_mul_lo_i32 v33, v46, s8
	v_mul_lo_i32 v34, v47, s8
	v_mul_lo_i32 v46, v48, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v40, v40, v52
	v_xor_b32_e32 v47, v41, v33
	v_xor_b32_e32 v33, 61, v36
	v_mul_lo_i32 v33, v33, s8
	v_xor_b32_e32 v45, v42, v45
	v_xor_b32_e32 v48, v44, v34
	v_xor_b32_e32 v39, v39, v51
	v_xor_b32_e32 v33, v45, v33
	v_mul_hi_u32 v34, v31, v33
	v_xor_b32_e32 v38, v38, v50
	v_xor_b32_e32 v37, v37, v49
	v_xor_b32_e32 v46, v43, v46
	v_mul_lo_i32 v34, v34, s9
	v_mul_lo_i32 v49, v37, s8
	v_mul_lo_i32 v50, v38, s8
	v_mul_lo_i32 v51, v39, s8
	v_cmp_ge_u32_e64 s[2:3], v33, v34
	v_subrev_u32_e32 v33, vcc, v34, v33
	v_cmp_le_u32_e32 vcc, s9, v33
	v_cndmask_b32_e64 v34, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v34
	v_and_b32_e32 v34, v41, v34
	v_cmp_eq_u32_e64 s[4:5], 0, v34
	v_subrev_u32_e32 v34, vcc, s9, v33
	v_cndmask_b32_e64 v34, v34, v33, s[4:5]
	v_add_u32_e32 v33, vcc, s9, v33
	v_cndmask_b32_e64 v33, v34, v33, s[2:3]
	ds_write_b32 v32, v33
	ds_read_b32 v34, v0
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v52, v40, s8
	v_mul_lo_i32 v46, v46, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[33:34], 7, v[34:35]
	v_add_u32_e32 v33, vcc, s14, v33
	v_addc_u32_e32 v34, vcc, v41, v34, vcc
	v_add_u32_e32 v33, vcc, v33, v24
	v_addc_u32_e32 v34, vcc, v34, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v33
	v_addc_u32_e32 v42, vcc, 0, v34, vcc
	flat_load_dwordx4 v[37:40], v[33:34]
	flat_load_dwordx4 v[41:44], v[41:42]
	v_mul_lo_i32 v33, v45, s8
	v_mul_lo_i32 v34, v47, s8
	v_mul_lo_i32 v45, v48, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_xor_b32_e32 v38, v38, v50
	v_xor_b32_e32 v47, v42, v33
	v_xor_b32_e32 v33, 62, v36
	v_mul_lo_i32 v33, v33, s8
	v_xor_b32_e32 v50, v43, v46
	v_xor_b32_e32 v48, v41, v34
	v_xor_b32_e32 v40, v40, v52
	v_xor_b32_e32 v33, v50, v33
	v_mul_hi_u32 v34, v31, v33
	v_xor_b32_e32 v39, v39, v51
	v_xor_b32_e32 v37, v37, v49
	v_xor_b32_e32 v49, v44, v45
	v_mul_lo_i32 v34, v34, s9
	v_mul_lo_i32 v51, v37, s8
	v_mul_lo_i32 v52, v38, s8
	v_mul_lo_i32 v53, v39, s8
	v_cmp_ge_u32_e64 s[2:3], v33, v34
	v_subrev_u32_e32 v33, vcc, v34, v33
	v_cmp_le_u32_e32 vcc, s9, v33
	v_cndmask_b32_e64 v34, 0, -1, s[2:3]
	v_cndmask_b32_e64 v41, 0, -1, vcc
	v_cmp_eq_u32_e64 s[2:3], 0, v34
	v_and_b32_e32 v34, v41, v34
	v_cmp_eq_u32_e64 s[4:5], 0, v34
	v_subrev_u32_e32 v34, vcc, s9, v33
	v_cndmask_b32_e64 v34, v34, v33, s[4:5]
	v_add_u32_e32 v33, vcc, s9, v33
	v_cndmask_b32_e64 v33, v34, v33, s[2:3]
	ds_write_b32 v32, v33
	ds_read_b32 v34, v0
	v_mov_b32_e32 v41, s15
	v_mul_lo_i32 v54, v40, s8
	v_xor_b32_e32 v36, 63, v36
	v_mul_lo_i32 v36, v36, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[33:34], 7, v[34:35]
	v_add_u32_e32 v33, vcc, s14, v33
	v_addc_u32_e32 v34, vcc, v41, v34, vcc
	v_add_u32_e32 v33, vcc, v33, v24
	v_addc_u32_e32 v34, vcc, v34, v25, vcc
	v_add_u32_e32 v41, vcc, 16, v33
	v_addc_u32_e32 v42, vcc, 0, v34, vcc
	flat_load_dwordx4 v[37:40], v[33:34]
	flat_load_dwordx4 v[43:46], v[41:42]
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	v_mul_lo_i32 v41, v48, s8
	s_mov_b32 s4, 1
	v_xor_b32_e32 v33, v40, v54
	v_xor_b32_e32 v40, v38, v52
	v_mul_lo_i32 v38, v47, s8
	v_mul_lo_i32 v47, v49, s8
	v_xor_b32_e32 v34, v39, v53
	v_xor_b32_e32 v42, v37, v51
	v_xor_b32_e32 v39, v44, v38
	v_xor_b32_e32 v38, v46, v47
	v_xor_b32_e32 v36, v38, v36
	v_mul_hi_u32 v31, v31, v36
	v_mul_lo_i32 v37, v50, s8
	v_mul_lo_i32 v44, v34, s8
	v_xor_b32_e32 v41, v43, v41
	v_mul_lo_i32 v31, v31, s9
	v_xor_b32_e32 v37, v45, v37
	v_mul_lo_i32 v45, v33, s8
	v_mov_b32_e32 v43, s15
	v_cmp_ge_u32_e32 vcc, v36, v31
	v_cndmask_b32_e64 v33, 0, -1, vcc
	v_subrev_u32_e32 v31, vcc, v31, v36
	v_cmp_le_u32_e32 vcc, s9, v31
	v_cndmask_b32_e64 v34, 0, -1, vcc
	v_and_b32_e32 v34, v34, v33
	v_cmp_eq_u32_e64 s[2:3], 0, v34
	v_subrev_u32_e32 v34, vcc, s9, v31
	v_cndmask_b32_e64 v34, v34, v31, s[2:3]
	v_add_u32_e32 v31, vcc, s9, v31
	v_cmp_eq_u32_e32 vcc, 0, v33
	v_cndmask_b32_e32 v31, v34, v31, vcc
	ds_write_b32 v32, v31
	ds_read_b32 v34, v0
	v_mul_lo_i32 v46, v37, s8
	v_mul_lo_i32 v47, v38, s8
	v_mul_lo_i32 v42, v42, s8
	v_mul_lo_i32 v41, v41, s8
	s_waitcnt lgkmcnt(0)
	v_lshlrev_b64 v[31:32], 7, v[34:35]
	v_add_u32_e32 v0, vcc, s14, v31
	v_addc_u32_e32 v31, vcc, v43, v32, vcc
	v_add_u32_e32 v24, vcc, v0, v24
	v_addc_u32_e32 v25, vcc, v31, v25, vcc
	v_add_u32_e32 v35, vcc, 16, v24
	v_addc_u32_e32 v36, vcc, 0, v25, vcc
	flat_load_dwordx4 v[31:34], v[24:25]
	flat_load_dwordx4 v[35:38], v[35:36]
	v_mul_lo_i32 v40, v40, s8
	v_mul_lo_i32 v39, v39, s8
	s_waitcnt lgkmcnt(0)
	s_waitcnt vmcnt(0) lgkmcnt(0)
	s_barrier
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_mov_b32 s2, 0
	v_xor_b32_e32 v31, v31, v42
	v_xor_b32_e32 v24, v33, v44
	v_xor_b32_e32 v33, v35, v41
	v_mul_lo_i32 v31, v31, s8
	v_mul_lo_i32 v33, v33, s8
	v_xor_b32_e32 v25, v32, v40
	v_xor_b32_e32 v0, v34, v45
	v_xor_b32_e32 v34, v36, v39
	v_xor_b32_e32 v25, v31, v25
	v_xor_b32_e32 v31, v33, v34
	v_mul_lo_i32 v25, v25, s8
	v_mul_lo_i32 v31, v31, s8
	v_xor_b32_e32 v33, v37, v46
	v_xor_b32_e32 v32, v38, v47
	v_xor_b32_e32 v24, v25, v24
	v_xor_b32_e32 v25, v31, v33
	v_mul_lo_i32 v24, v24, s8
	v_mul_lo_i32 v25, v25, s8
	v_xor_b32_e32 v24, v24, v0
	v_xor_b32_e32 v25, v25, v32
	ds_write_b64 v26, v[24:25]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_and_saveexec_b64 s[8:9], s[0:1]
	; mask branch BB1_20
BB1_19:                                 ; %if.then1083.3
	s_mov_b32 m0, -1
	ds_read2_b64 v[27:30], v3 offset1:1
	ds_read2_b64 v[4:7], v3 offset0:2 offset1:3
BB1_20:                                 ; %if.end1099.3
	s_or_b64 exec, exec, s[8:9]
	s_waitcnt lgkmcnt(0)
	s_waitcnt lgkmcnt(0)
	s_barrier
	s_mov_b32 s0, s2
	s_mov_b32 s1, s2
	v_mov_b32_e32 v50, s1
	v_mov_b32_e32 v49, s0
	s_mov_b32 s5, s2
	s_brev_b32 s3, 1
	v_mov_b32_e32 v38, s5
	v_mov_b32_e32 v25, s3
	v_mov_b32_e32 v43, v49
	v_mov_b32_e32 v41, v49
	v_mov_b32_e32 v52, v50
	v_mov_b32_e32 v45, v49
	v_mov_b32_e32 v39, v49
	v_mov_b32_e32 v31, v49
	v_mov_b32_e32 v54, v50
	v_mov_b32_e32 v47, v49
	v_mov_b32_e32 v35, v49
	v_mov_b32_e32 v33, v49
	v_mov_b32_e32 v37, s4
	v_mov_b32_e32 v24, s2
	v_mov_b32_e32 v44, v50
	v_mov_b32_e32 v42, v50
	v_mov_b32_e32 v51, v49
	v_mov_b32_e32 v46, v50
	v_mov_b32_e32 v40, v50
	v_mov_b32_e32 v32, v50
	v_mov_b32_e32 v53, v49
	v_mov_b32_e32 v48, v50
	v_mov_b32_e32 v36, v50
	v_mov_b32_e32 v34, v50
BB1_21:                                 ; %for.body1122
                                        ; =>This Inner Loop Header: Depth=1
	v_cmp_ne_u32_e32 vcc, 0, v2
	s_cbranch_vccnz BB1_23
; BB#22:                                ; %do.body1125
                                        ;   in Loop: Header=BB1_21 Depth=1
	v_xor_b32_e32 v0, v11, v23
	v_xor_b32_e32 v3, v10, v22
	v_xor_b32_e32 v58, v30, v17
	v_xor_b32_e32 v59, v29, v16
	v_xor_b32_e32 v0, v0, v5
	v_xor_b32_e32 v3, v3, v4
	v_xor_b32_e32 v55, v20, v12
	v_xor_b32_e32 v56, v28, v15
	v_xor_b32_e32 v58, v58, v42
	v_xor_b32_e32 v59, v59, v41
	v_xor_b32_e32 v3, v3, v33
	v_xor_b32_e32 v0, v0, v34
	v_xor_b32_e32 v55, v55, v37
	v_xor_b32_e32 v56, v56, v40
	v_xor_b32_e32 v59, v59, v43
	v_xor_b32_e32 v58, v58, v44
	v_xor_b32_e32 v55, v55, v47
	v_xor_b32_e32 v56, v56, v46
	v_xor_b32_e32 v0, v0, v36
	v_xor_b32_e32 v3, v3, v35
	v_mov_b32_e32 v60, 31
	v_xor_b32_e32 v58, v58, v50
	v_xor_b32_e32 v59, v59, v49
	v_xor_b32_e32 v26, v21, v13
	v_xor_b32_e32 v57, v27, v14
	v_xor_b32_e32 v55, v55, v53
	;;#ASMSTART
	v_alignbit_b32 v61, v59, v58, v60
v_alignbit_b32 v62, v58, v59, v60
	;;#ASMEND
	v_xor_b32_e32 v56, v56, v52
	;;#ASMSTART
	v_alignbit_b32 v63, v3, v0, v60
v_alignbit_b32 v64, v0, v3, v60
	;;#ASMEND
	v_xor_b32_e32 v26, v26, v38
	v_xor_b32_e32 v57, v57, v39
	v_xor_b32_e32 v61, v61, v55
	v_xor_b32_e32 v64, v64, v56
	v_xor_b32_e32 v26, v26, v48
	v_xor_b32_e32 v57, v57, v45
	v_xor_b32_e32 v65, v61, v27
	v_xor_b32_e32 v27, v64, v30
	v_xor_b32_e32 v30, v19, v9
	v_xor_b32_e32 v30, v30, v7
	v_xor_b32_e32 v26, v26, v54
	v_xor_b32_e32 v57, v57, v51
	v_xor_b32_e32 v62, v62, v26
	v_xor_b32_e32 v30, v30, v25
	v_xor_b32_e32 v63, v63, v57
	v_xor_b32_e32 v66, v62, v28
	v_xor_b32_e32 v28, v63, v29
	v_xor_b32_e32 v29, v18, v8
	v_xor_b32_e32 v30, v30, v32
	;;#ASMSTART
	v_alignbit_b32 v56, v57, v56, v60
v_alignbit_b32 v57, v56, v57, v60
	;;#ASMEND
	v_xor_b32_e32 v57, v57, v30
	v_xor_b32_e32 v29, v29, v6
	v_xor_b32_e32 v29, v29, v24
	v_xor_b32_e32 v69, v57, v21
	v_xor_b32_e32 v21, v57, v54
	v_xor_b32_e32 v54, v64, v42
	v_xor_b32_e32 v42, v63, v43
	;;#ASMSTART
	v_alignbit_b32 v26, v55, v26, v60
v_alignbit_b32 v43, v26, v55, v60
	;;#ASMEND
	v_xor_b32_e32 v0, v43, v0
	v_xor_b32_e32 v3, v26, v3
	v_xor_b32_e32 v29, v29, v31
	v_xor_b32_e32 v56, v56, v29
	v_xor_b32_e32 v18, v3, v18
	v_xor_b32_e32 v19, v0, v19
	v_mov_b32_e32 v26, 20
	v_xor_b32_e32 v67, v56, v12
	v_xor_b32_e32 v68, v57, v13
	v_xor_b32_e32 v12, v56, v37
	v_xor_b32_e32 v13, v57, v38
	;;#ASMSTART
	v_alignbit_b32 v26, v19, v18, v26
v_alignbit_b32 v55, v18, v19, v26
	;;#ASMEND
	v_mov_b32_e32 v18, 21
	v_xor_b32_e32 v70, v56, v20
	v_xor_b32_e32 v20, v56, v53
	v_xor_b32_e32 v37, v62, v15
	v_xor_b32_e32 v38, v61, v14
	v_xor_b32_e32 v14, v62, v46
	v_xor_b32_e32 v15, v61, v45
	v_xor_b32_e32 v48, v57, v48
	v_xor_b32_e32 v47, v56, v47
	;;#ASMSTART
	v_alignbit_b32 v56, v13, v12, v18
v_alignbit_b32 v57, v12, v13, v18
	;;#ASMEND
	v_mov_b32_e32 v12, 11
	;;#ASMSTART
	v_alignbit_b32 v14, v15, v14, v12
v_alignbit_b32 v15, v14, v15, v12
	;;#ASMEND
	v_xor_b32_e32 v43, v0, v9
	v_xor_b32_e32 v9, v15, v55
	v_and_b32_e32 v12, v15, v57
	v_xor_b32_e32 v53, v63, v41
	v_xor_b32_e32 v41, v64, v44
	v_xor_b32_e32 v9, v9, v12
	v_xor_b32_e32 v44, v3, v8
	v_xor_b32_e32 v8, v14, v26
	v_and_b32_e32 v12, v14, v56
	v_xor_b32_e32 v45, v61, v51
	v_xor_b32_e32 v46, v62, v52
	v_xor_b32_e32 v51, v64, v17
	v_xor_b32_e32 v52, v63, v16
	v_xor_b32_e32 v8, v8, v12
	v_xor_b32_e32 v16, v64, v50
	v_xor_b32_e32 v17, v63, v49
	v_mov_b32_e32 v12, 18
	;;#ASMSTART
	v_alignbit_b32 v16, v17, v16, v12
v_alignbit_b32 v17, v16, v17, v12
	;;#ASMEND
	v_xor_b32_e32 v12, v17, v57
	v_and_b32_e32 v13, v15, v17
	v_xor_b32_e32 v13, v12, v13
	v_xor_b32_e32 v12, v16, v56
	v_and_b32_e32 v18, v14, v16
	v_xor_b32_e32 v12, v12, v18
	;;#ASMSTART
	v_alignbit_b32 v18, v29, v30, v60
v_alignbit_b32 v19, v30, v29, v60
	;;#ASMEND
	v_xor_b32_e32 v19, v19, v58
	v_xor_b32_e32 v18, v18, v59
	v_xor_b32_e32 v22, v18, v22
	v_not_b32_e32 v29, v16
	v_xor_b32_e32 v23, v19, v23
	v_not_b32_e32 v30, v17
	v_and_b32_e32 v29, v22, v29
	v_and_b32_e32 v30, v23, v30
	v_xor_b32_e32 v15, v30, v15
	v_not_b32_e32 v30, v23
	v_xor_b32_e32 v14, v29, v14
	v_not_b32_e32 v29, v22
	v_and_b32_e32 v29, v26, v29
	v_and_b32_e32 v30, v55, v30
	v_xor_b32_e32 v49, v18, v10
	v_mov_b32_e32 v10, 12
	v_xor_b32_e32 v17, v30, v17
	v_xor_b32_e32 v16, v29, v16
	;;#ASMSTART
	v_alignbit_b32 v29, v28, v27, v10
v_alignbit_b32 v30, v27, v28, v10
	;;#ASMEND
	v_mov_b32_e32 v10, 4
	v_xor_b32_e32 v58, v18, v33
	v_xor_b32_e32 v59, v19, v34
	v_xor_b32_e32 v33, v19, v36
	v_xor_b32_e32 v34, v18, v35
	;;#ASMSTART
	v_alignbit_b32 v35, v38, v37, v10
v_alignbit_b32 v36, v37, v38, v10
	;;#ASMEND
	v_xor_b32_e32 v5, v19, v5
	v_xor_b32_e32 v4, v18, v4
	v_mov_b32_e32 v10, 29
	;;#ASMSTART
	v_alignbit_b32 v4, v4, v5, v10
v_alignbit_b32 v5, v5, v4, v10
	;;#ASMEND
	v_xor_b32_e32 v50, v19, v11
	v_xor_b32_e32 v10, v5, v36
	v_and_b32_e32 v11, v5, v30
	v_xor_b32_e32 v11, v10, v11
	v_xor_b32_e32 v10, v4, v35
	v_and_b32_e32 v18, v4, v29
	v_xor_b32_e32 v10, v10, v18
	v_xor_b32_e32 v18, v24, v3
	v_xor_b32_e32 v19, v25, v0
	v_mov_b32_e32 v24, 19
	;;#ASMSTART
	v_alignbit_b32 v24, v19, v18, v24
v_alignbit_b32 v25, v18, v19, v24
	;;#ASMEND
	v_xor_b32_e32 v18, v25, v30
	v_and_b32_e32 v19, v5, v25
	v_xor_b32_e32 v19, v18, v19
	v_xor_b32_e32 v18, v24, v29
	v_and_b32_e32 v27, v4, v24
	v_xor_b32_e32 v18, v18, v27
	v_mov_b32_e32 v27, 3
	;;#ASMSTART
	v_alignbit_b32 v37, v21, v20, v27
v_alignbit_b32 v38, v20, v21, v27
	;;#ASMEND
	v_not_b32_e32 v20, v24
	v_not_b32_e32 v21, v25
	v_and_b32_e32 v20, v37, v20
	v_and_b32_e32 v21, v38, v21
	v_xor_b32_e32 v21, v21, v5
	v_xor_b32_e32 v20, v20, v4
	v_xor_b32_e32 v4, v35, v24
	v_xor_b32_e32 v5, v36, v25
	v_and_b32_e32 v24, v35, v37
	v_and_b32_e32 v25, v36, v38
	v_xor_b32_e32 v28, v5, v25
	v_xor_b32_e32 v27, v4, v24
	v_xor_b32_e32 v4, v38, v30
	v_and_b32_e32 v5, v36, v30
	v_xor_b32_e32 v30, v4, v5
	v_xor_b32_e32 v4, v37, v29
	v_and_b32_e32 v5, v35, v29
	v_xor_b32_e32 v29, v4, v5
	v_mov_b32_e32 v4, 14
	;;#ASMSTART
	v_alignbit_b32 v24, v34, v33, v4
v_alignbit_b32 v25, v33, v34, v4
	;;#ASMEND
	v_xor_b32_e32 v40, v62, v40
	v_xor_b32_e32 v39, v61, v39
	v_mov_b32_e32 v4, 7
	;;#ASMSTART
	v_alignbit_b32 v33, v39, v40, v4
v_alignbit_b32 v34, v40, v39, v4
	;;#ASMEND
	v_mov_b32_e32 v4, 24
	;;#ASMSTART
	v_alignbit_b32 v35, v42, v41, v4
v_alignbit_b32 v36, v41, v42, v4
	;;#ASMEND
	v_xor_b32_e32 v4, v34, v25
	v_and_b32_e32 v5, v36, v25
	v_xor_b32_e32 v38, v4, v5
	v_xor_b32_e32 v4, v33, v24
	v_and_b32_e32 v5, v35, v24
	;;#ASMSTART
	v_alignbit_b32 v41, v44, v43, v60
v_alignbit_b32 v42, v43, v44, v60
	;;#ASMEND
	v_xor_b32_e32 v37, v4, v5
	v_xor_b32_e32 v4, v42, v36
	v_and_b32_e32 v5, v42, v25
	v_xor_b32_e32 v40, v4, v5
	v_xor_b32_e32 v4, v41, v35
	v_and_b32_e32 v5, v41, v24
	v_xor_b32_e32 v39, v4, v5
	v_mov_b32_e32 v4, 26
	;;#ASMSTART
	v_alignbit_b32 v43, v70, v69, v4
v_alignbit_b32 v44, v69, v70, v4
	;;#ASMEND
	v_not_b32_e32 v4, v44
	v_and_b32_e32 v4, v34, v4
	v_xor_b32_e32 v5, v4, v42
	v_not_b32_e32 v4, v43
	v_and_b32_e32 v4, v33, v4
	v_not_b32_e32 v33, v33
	v_and_b32_e32 v33, v35, v33
	v_xor_b32_e32 v31, v31, v3
	v_xor_b32_e32 v3, v3, v6
	v_xor_b32_e32 v6, v33, v43
	v_xor_b32_e32 v24, v43, v24
	v_and_b32_e32 v33, v41, v43
	v_not_b32_e32 v34, v34
	v_xor_b32_e32 v4, v4, v41
	v_xor_b32_e32 v41, v24, v33
	v_mov_b32_e32 v24, 5
	v_and_b32_e32 v34, v36, v34
	;;#ASMSTART
	v_alignbit_b32 v35, v52, v51, v24
v_alignbit_b32 v36, v51, v52, v24
	;;#ASMEND
	v_mov_b32_e32 v24, 28
	v_xor_b32_e32 v32, v32, v0
	v_xor_b32_e32 v0, v0, v7
	v_xor_b32_e32 v7, v34, v44
	v_xor_b32_e32 v25, v44, v25
	v_and_b32_e32 v34, v42, v44
	;;#ASMSTART
	v_alignbit_b32 v43, v50, v49, v24
v_alignbit_b32 v44, v49, v50, v24
	;;#ASMEND
	v_mov_b32_e32 v24, 22
	;;#ASMSTART
	v_alignbit_b32 v0, v3, v0, v24
v_alignbit_b32 v3, v0, v3, v24
	;;#ASMEND
	v_xor_b32_e32 v42, v25, v34
	v_xor_b32_e32 v24, v3, v36
	v_and_b32_e32 v25, v3, v44
	v_xor_b32_e32 v34, v24, v25
	v_xor_b32_e32 v24, v0, v35
	v_and_b32_e32 v25, v0, v43
	v_xor_b32_e32 v33, v24, v25
	v_mov_b32_e32 v24, 17
	;;#ASMSTART
	v_alignbit_b32 v49, v47, v48, v24
v_alignbit_b32 v50, v48, v47, v24
	;;#ASMEND
	v_xor_b32_e32 v24, v50, v44
	v_and_b32_e32 v25, v3, v50
	v_xor_b32_e32 v25, v24, v25
	v_xor_b32_e32 v24, v49, v43
	v_and_b32_e32 v47, v0, v49
	v_xor_b32_e32 v24, v24, v47
	v_mov_b32_e32 v47, 8
	;;#ASMSTART
	v_alignbit_b32 v51, v46, v45, v47
v_alignbit_b32 v52, v45, v46, v47
	;;#ASMEND
	v_not_b32_e32 v46, v50
	v_not_b32_e32 v45, v49
	v_and_b32_e32 v46, v52, v46
	v_and_b32_e32 v45, v51, v45
	v_xor_b32_e32 v48, v46, v3
	v_not_b32_e32 v3, v52
	v_and_b32_e32 v3, v36, v3
	v_xor_b32_e32 v47, v45, v0
	v_not_b32_e32 v0, v51
	v_and_b32_e32 v0, v35, v0
	v_xor_b32_e32 v46, v3, v50
	v_and_b32_e32 v3, v43, v35
	v_xor_b32_e32 v35, v44, v52
	v_and_b32_e32 v36, v44, v36
	v_xor_b32_e32 v45, v0, v49
	v_xor_b32_e32 v0, v43, v51
	v_xor_b32_e32 v44, v35, v36
	v_mov_b32_e32 v35, 2
	v_xor_b32_e32 v43, v0, v3
	v_mov_b32_e32 v0, 25
	;;#ASMSTART
	v_alignbit_b32 v49, v68, v67, v35
v_alignbit_b32 v50, v67, v68, v35
	;;#ASMEND
	v_mov_b32_e32 v35, 9
	;;#ASMSTART
	v_alignbit_b32 v0, v54, v53, v0
v_alignbit_b32 v3, v53, v54, v0
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v60, v66, v65, v35
v_alignbit_b32 v61, v65, v66, v35
	;;#ASMEND
	v_xor_b32_e32 v35, v50, v3
	v_and_b32_e32 v36, v61, v3
	v_xor_b32_e32 v36, v35, v36
	v_xor_b32_e32 v35, v49, v0
	v_and_b32_e32 v51, v60, v0
	v_xor_b32_e32 v35, v35, v51
	v_mov_b32_e32 v51, 23
	;;#ASMSTART
	v_alignbit_b32 v58, v59, v58, v51
v_alignbit_b32 v59, v58, v59, v51
	;;#ASMEND
	v_mov_b32_e32 v51, 30
	s_ashr_i32 s3, s2, 31
	;;#ASMSTART
	v_alignbit_b32 v62, v31, v32, v51
v_alignbit_b32 v63, v32, v31, v51
	;;#ASMEND
	s_add_i32 s0, s2, 1
	s_lshl_b64 s[2:3], s[2:3], 3
	s_add_u32 s2, s2, s6
	v_xor_b32_e32 v31, v63, v3
	v_and_b32_e32 v32, v63, v59
	s_addc_u32 s3, s3, s7
	v_xor_b32_e32 v54, v31, v32
	v_xor_b32_e32 v31, v62, v0
	v_and_b32_e32 v32, v62, v58
	v_xor_b32_e32 v53, v31, v32
	v_not_b32_e32 v0, v0
	v_not_b32_e32 v3, v3
	s_load_dwordx2 s[2:3], s[2:3], 0x0
	v_xor_b32_e32 v31, v59, v50
	v_and_b32_e32 v32, v63, v50
	v_xor_b32_e32 v52, v31, v32
	v_xor_b32_e32 v31, v58, v49
	v_and_b32_e32 v32, v62, v49
	v_and_b32_e32 v0, v58, v0
	v_and_b32_e32 v3, v59, v3
	v_xor_b32_e32 v51, v31, v32
	v_xor_b32_e32 v32, v3, v61
	v_xor_b32_e32 v31, v0, v60
	v_and_b32_e32 v3, v60, v49
	v_xor_b32_e32 v0, v60, v62
	v_xor_b32_e32 v49, v61, v63
	v_and_b32_e32 v50, v61, v50
	v_xor_b32_e32 v50, v49, v50
	v_xor_b32_e32 v49, v0, v3
	v_xor_b32_e32 v0, v57, v23
	v_and_b32_e32 v3, v57, v55
	v_xor_b32_e32 v22, v56, v22
	v_and_b32_e32 v23, v56, v26
	v_xor_b32_e32 v22, v22, v23
	v_xor_b32_e32 v0, v0, v3
	s_waitcnt lgkmcnt(0)
	v_xor_b32_e32 v22, s2, v22
	v_xor_b32_e32 v23, s3, v0
	s_mov_b32 s2, s0
BB1_23:                                 ; %if.end1605
                                        ;   in Loop: Header=BB1_21 Depth=1
	s_cmp_lt_i32 s2, 23
	s_cbranch_scc1 BB1_21
; BB#24:                                ; %for.cond.cleanup1121
	v_xor_b32_e32 v9, v19, v9
	v_xor_b32_e32 v8, v18, v8
	v_xor_b32_e32 v0, v27, v14
	v_xor_b32_e32 v6, v8, v6
	v_xor_b32_e32 v7, v9, v7
	v_xor_b32_e32 v2, v28, v15
	v_xor_b32_e32 v8, v20, v12
	v_xor_b32_e32 v9, v21, v13
	v_xor_b32_e32 v9, v9, v38
	v_xor_b32_e32 v8, v8, v37
	v_xor_b32_e32 v2, v2, v40
	v_xor_b32_e32 v0, v0, v39
	v_xor_b32_e32 v11, v11, v23
	v_xor_b32_e32 v10, v10, v22
	v_xor_b32_e32 v5, v11, v5
	v_xor_b32_e32 v3, v30, v17
	v_xor_b32_e32 v4, v10, v4
	v_xor_b32_e32 v8, v8, v47
	v_xor_b32_e32 v9, v9, v48
	v_xor_b32_e32 v0, v0, v45
	v_xor_b32_e32 v2, v2, v46
	v_xor_b32_e32 v5, v5, v34
	v_xor_b32_e32 v4, v4, v33
	v_xor_b32_e32 v7, v7, v25
	v_xor_b32_e32 v6, v6, v24
	v_xor_b32_e32 v3, v3, v42
	v_xor_b32_e32 v9, v9, v54
	v_xor_b32_e32 v8, v8, v53
	v_mov_b32_e32 v11, 31
	v_xor_b32_e32 v2, v2, v52
	v_xor_b32_e32 v0, v0, v51
	v_xor_b32_e32 v3, v3, v44
	v_xor_b32_e32 v4, v4, v35
	;;#ASMSTART
	v_alignbit_b32 v8, v8, v9, v11
v_alignbit_b32 v9, v9, v8, v11
	;;#ASMEND
	v_xor_b32_e32 v5, v5, v36
	v_xor_b32_e32 v6, v6, v31
	;;#ASMSTART
	v_alignbit_b32 v0, v0, v2, v11
v_alignbit_b32 v2, v2, v0, v11
	;;#ASMEND
	v_xor_b32_e32 v7, v7, v32
	v_xor_b32_e32 v5, v9, v5
	v_xor_b32_e32 v4, v8, v4
	v_xor_b32_e32 v2, v2, v7
	v_xor_b32_e32 v0, v0, v6
	v_xor_b32_e32 v14, v29, v16
	v_xor_b32_e32 v3, v3, v50
	;;#ASMSTART
	v_alignbit_b32 v6, v6, v7, v11
v_alignbit_b32 v7, v7, v6, v11
	;;#ASMEND
	v_xor_b32_e32 v3, v7, v3
	v_xor_b32_e32 v10, v14, v41
	v_xor_b32_e32 v4, v4, v18
	v_xor_b32_e32 v5, v5, v19
	v_mov_b32_e32 v7, 20
	v_xor_b32_e32 v8, v10, v43
	;;#ASMSTART
	v_alignbit_b32 v4, v5, v4, v7
v_alignbit_b32 v5, v4, v5, v7
	;;#ASMEND
	v_xor_b32_e32 v0, v0, v37
	v_xor_b32_e32 v2, v2, v38
	v_mov_b32_e32 v7, 21
	;;#ASMSTART
	v_alignbit_b32 v0, v2, v0, v7
v_alignbit_b32 v2, v0, v2, v7
	;;#ASMEND
	v_xor_b32_e32 v8, v8, v49
	v_xor_b32_e32 v3, v3, v23
	v_xor_b32_e32 v6, v6, v8
	v_and_b32_e32 v5, v2, v5
	v_xor_b32_e32 v2, v3, v2
	v_xor_b32_e32 v2, v2, v5
	v_xor_b32_e32 v6, v6, v22
	v_and_b32_e32 v4, v0, v4
	v_xor_b32_e32 v0, v6, v0
	v_xor_b32_e32 v2, 0x80000000, v2
	v_xor_b32_e32 v0, v0, v4
	v_lshrrev_b32_e32 v4, 16, v2
	v_lshlrev_b16_e32 v4, 8, v4
	v_xor_b32_e32 v0, 0x80008008, v0
	v_lshlrev_b16_e32 v5, 8, v2
	v_or_b32_sdwa v4, v2, v4 dst_sel:DWORD dst_unused:UNUSED_PAD src0_sel:BYTE_3 src1_sel:DWORD
	v_or_b32_sdwa v2, v2, v5 dst_sel:WORD_1 dst_unused:UNUSED_PAD src0_sel:BYTE_1 src1_sel:DWORD
	v_lshrrev_b32_e32 v5, 16, v0
	v_lshlrev_b16_e32 v3, 8, v0
	v_lshlrev_b16_e32 v5, 8, v5
	v_or_b32_sdwa v3, v0, v3 dst_sel:WORD_1 dst_unused:UNUSED_PAD src0_sel:BYTE_1 src1_sel:DWORD
	v_or_b32_sdwa v0, v0, v5 dst_sel:DWORD dst_unused:UNUSED_PAD src0_sel:BYTE_3 src1_sel:DWORD
	v_or_b32_sdwa v3, v0, v3 dst_sel:DWORD dst_unused:UNUSED_PAD src0_sel:WORD_0 src1_sel:DWORD
	v_or_b32_sdwa v2, v4, v2 dst_sel:DWORD dst_unused:UNUSED_PAD src0_sel:WORD_0 src1_sel:DWORD
	v_cmp_gt_u64_e32 vcc, s[12:13], v[2:3]
	s_and_saveexec_b64 s[0:1], vcc
	; mask branch BB1_26
	s_cbranch_execz BB1_26
BB1_25:                                 ; %if.then1931
	s_add_u32 s0, s10, 0x3fc
	s_addc_u32 s1, s11, 0
	v_mov_b32_e32 v3, s1
	v_mov_b32_e32 v0, 1
	v_mov_b32_e32 v2, s0
	s_waitcnt vmcnt(0)
	flat_atomic_add v0, v[2:3], v0 glc
	s_waitcnt vmcnt(0)
	buffer_wbinvl1_vol
	v_mov_b32_e32 v4, s11
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_min_u32_e32 v0, 0xfe, v0
	v_lshlrev_b32_e32 v0, 2, v0
	v_add_u32_e32 v2, vcc, s10, v0
	v_addc_u32_e32 v3, vcc, 0, v4, vcc
	flat_store_dword v[2:3], v1
BB1_26:                                 ; %if.end1937
	s_endpgm
.Lfunc_end1:
	.size	search, .Lfunc_end1-search
                                        ; -- End function
	.section	.AMDGPU.csdata
; Kernel info:
; codeLenInByte = 80612
; NumSgprs: 24
; NumVgprs: 74
; ScratchSize: 0
; FloatMode: 192
; IeeeMode: 1
; LDSByteSize: 5120 bytes/workgroup (compile time only)
; SGPRBlocks: 2
; VGPRBlocks: 18
; NumSGPRsForWavesPerEU: 24
; NumVGPRsForWavesPerEU: 74
; ReservedVGPRFirst: 0
; ReservedVGPRCount: 0
; COMPUTE_PGM_RSRC2:USER_SGPR: 8
; COMPUTE_PGM_RSRC2:TRAP_HANDLER: 1
; COMPUTE_PGM_RSRC2:TGID_X_EN: 1
; COMPUTE_PGM_RSRC2:TGID_Y_EN: 0
; COMPUTE_PGM_RSRC2:TGID_Z_EN: 0
; COMPUTE_PGM_RSRC2:TIDIG_COMP_CNT: 0
	.text
	.globl	GenerateDAG             ; -- Begin function GenerateDAG
	.p2align	8
	.type	GenerateDAG,@function
	.amdgpu_hsa_kernel GenerateDAG
GenerateDAG:                            ; @GenerateDAG
	.amd_kernel_code_t
		amd_code_version_major = 1
		amd_code_version_minor = 1
		amd_machine_kind = 1
		amd_machine_version_major = 8
		amd_machine_version_minor = 0
		amd_machine_version_stepping = 3
		kernel_code_entry_byte_offset = 256
		kernel_code_prefetch_byte_size = 0
		max_scratch_backing_memory_byte_size = 0
		granulated_workitem_vgpr_count = 23
		granulated_wavefront_sgpr_count = 3
		priority = 0
		float_mode = 192
		priv = 0
		enable_dx10_clamp = 1
		debug_mode = 0
		enable_ieee_mode = 1
		enable_sgpr_private_segment_wave_byte_offset = 1
		user_sgpr_count = 10
		enable_trap_handler = 1
		enable_sgpr_workgroup_id_x = 1
		enable_sgpr_workgroup_id_y = 0
		enable_sgpr_workgroup_id_z = 0
		enable_sgpr_workgroup_info = 0
		enable_vgpr_workitem_id = 0
		enable_exception_msb = 0
		granulated_lds_size = 0
		enable_exception = 0
		enable_sgpr_private_segment_buffer = 1
		enable_sgpr_dispatch_ptr = 1
		enable_sgpr_queue_ptr = 0
		enable_sgpr_kernarg_segment_ptr = 1
		enable_sgpr_dispatch_id = 0
		enable_sgpr_flat_scratch_init = 1
		enable_sgpr_private_segment_size = 0
		enable_sgpr_grid_workgroup_count_x = 0
		enable_sgpr_grid_workgroup_count_y = 0
		enable_sgpr_grid_workgroup_count_z = 0
		enable_ordered_append_gds = 0
		private_element_size = 1
		is_ptr64 = 1
		is_dynamic_callstack = 0
		is_debug_enabled = 0
		is_xnack_enabled = 0
		workitem_private_segment_byte_size = 80
		workgroup_group_segment_byte_size = 0
		gds_segment_byte_size = 0
		kernarg_segment_byte_size = 64
		workgroup_fbarrier_count = 0
		wavefront_sgpr_count = 26
		workitem_vgpr_count = 95
		reserved_vgpr_first = 0
		reserved_vgpr_count = 0
		reserved_sgpr_first = 0
		reserved_sgpr_count = 0
		debug_wavefront_private_segment_offset_sgpr = 0
		debug_private_segment_buffer_sgpr = 0
		kernarg_segment_alignment = 4
		group_segment_alignment = 4
		private_segment_alignment = 4
		wavefront_size = 6
		call_convention = -1
		runtime_loader_kernel_symbol = 0
	.end_amd_kernel_code_t
; BB#0:                                 ; %entry
	s_add_u32 s8, s8, s11
	s_mov_b32 flat_scratch_lo, s9
	s_lshr_b32 flat_scratch_hi, s8, 8
	s_load_dwordx2 s[8:9], s[6:7], 0x10
	s_load_dword s12, s[6:7], 0x18
	s_load_dword s13, s[6:7], 0x1c
	s_load_dword s16, s[6:7], 0x24
	s_load_dword s4, s[4:5], 0x4
	s_load_dword s5, s[6:7], 0x0
	s_load_dwordx2 s[14:15], s[6:7], 0x8
	s_waitcnt lgkmcnt(0)
	v_cvt_f32_u32_e32 v1, s12
	v_mov_b32_e32 v7, 0
	s_and_b32 s4, s4, 0xffff
	v_add_u32_e32 v0, vcc, s5, v0
	v_rcp_iflag_f32_e32 v1, v1
	v_add_u32_e32 v0, vcc, s16, v0
	s_mul_i32 s4, s4, s10
	v_add_u32_e32 v0, vcc, s4, v0
	v_mul_f32_e32 v1, 0x4f800000, v1
	v_cvt_u32_f32_e32 v1, v1
	v_mov_b32_e32 v8, s15
	s_getpc_b64 s[6:7]
	s_add_u32 s6, s6, Keccak_f1600_RC@rel32@lo+4
	s_addc_u32 s7, s7, Keccak_f1600_RC@rel32@hi+4
	s_mov_b32 s18, 0
	v_mul_lo_i32 v2, v1, s12
	v_mul_hi_u32 v3, v1, s12
	s_mov_b32 s19, s18
	v_mov_b32_e32 v28, s19
	v_sub_u32_e32 v4, vcc, 0, v2
	v_cmp_eq_u32_e64 s[4:5], 0, v3
	v_cndmask_b32_e64 v2, v2, v4, s[4:5]
	v_mul_hi_u32 v2, v2, v1
	v_mov_b32_e32 v27, s18
	s_brev_b32 s17, 1
	s_mov_b32 s16, 1
	v_add_u32_e32 v3, vcc, v2, v1
	v_subrev_u32_e32 v1, vcc, v2, v1
	v_cndmask_b32_e64 v2, v1, v3, s[4:5]
	v_mul_hi_u32 v4, v2, v0
	v_cmp_eq_u32_e64 s[4:5], s13, 0
	v_mov_b32_e32 v56, v28
	v_mov_b32_e32 v63, v28
	v_mul_lo_i32 v6, v4, s12
	v_mov_b32_e32 v48, v28
	v_mov_b32_e32 v50, v28
	v_mov_b32_e32 v54, v28
	v_subrev_u32_e32 v9, vcc, v6, v0
	v_cmp_ge_u32_e32 vcc, v0, v6
	v_cndmask_b32_e64 v6, 0, -1, vcc
	v_cmp_le_u32_e32 vcc, s12, v9
	v_cndmask_b32_e64 v10, 0, -1, vcc
	v_and_b32_e32 v10, v10, v6
	v_subrev_u32_e32 v11, vcc, s12, v9
	v_cmp_eq_u32_e32 vcc, 0, v10
	v_cndmask_b32_e32 v10, v11, v9, vcc
	v_add_u32_e32 v9, vcc, s12, v9
	v_cmp_eq_u32_e32 vcc, 0, v6
	v_cndmask_b32_e32 v6, v10, v9, vcc
	v_lshlrev_b64 v[6:7], 6, v[6:7]
	v_add_u32_e32 v29, vcc, s14, v6
	v_addc_u32_e32 v30, vcc, v8, v7, vcc
	flat_load_dwordx2 v[60:61], v[29:30]
	v_add_u32_e32 v31, vcc, 56, v29
	v_addc_u32_e32 v32, vcc, 0, v30, vcc
	v_add_u32_e32 v33, vcc, 48, v29
	v_addc_u32_e32 v34, vcc, 0, v30, vcc
	v_add_u32_e32 v35, vcc, 40, v29
	v_addc_u32_e32 v36, vcc, 0, v30, vcc
	v_add_u32_e32 v37, vcc, 32, v29
	v_addc_u32_e32 v38, vcc, 0, v30, vcc
	v_add_u32_e32 v39, vcc, 24, v29
	v_addc_u32_e32 v40, vcc, 0, v30, vcc
	v_add_u32_e32 v41, vcc, 16, v29
	v_addc_u32_e32 v42, vcc, 0, v30, vcc
	v_add_u32_e32 v43, vcc, 8, v29
	v_addc_u32_e32 v44, vcc, 0, v30, vcc
	v_mov_b32_e32 v65, v28
	v_mov_b32_e32 v58, v28
	v_cndmask_b32_e64 v1, 0, -1, s[4:5]
	v_mov_b32_e32 v3, 20
	v_mov_b32_e32 v4, 12
	v_mov_b32_e32 v5, 25
	v_mov_b32_e32 v6, 14
	v_mov_b32_e32 v7, 2
	v_mov_b32_e32 v8, 21
	v_mov_b32_e32 v9, 7
	v_mov_b32_e32 v10, 24
	v_mov_b32_e32 v11, 8
	v_mov_b32_e32 v12, 23
	v_mov_b32_e32 v13, 5
	v_mov_b32_e32 v14, 18
	v_mov_b32_e32 v15, 30
	v_mov_b32_e32 v16, 9
	v_mov_b32_e32 v17, 19
	v_mov_b32_e32 v18, 28
	v_mov_b32_e32 v19, 4
	v_mov_b32_e32 v20, 11
	v_mov_b32_e32 v21, 17
	v_mov_b32_e32 v22, 22
	v_mov_b32_e32 v23, 26
	v_mov_b32_e32 v24, 29
	v_mov_b32_e32 v25, 31
	v_mov_b32_e32 v26, 3
	v_mov_b32_e32 v55, v27
	v_mov_b32_e32 v62, v27
	v_mov_b32_e32 v47, v27
	v_mov_b32_e32 v49, v27
	v_mov_b32_e32 v53, v27
	v_mov_b32_e32 v64, v27
	v_mov_b32_e32 v57, v27
	s_waitcnt vmcnt(0) lgkmcnt(0)
	buffer_store_dword v60, off, s[0:3], s11 offset:16
	buffer_store_dword v61, off, s[0:3], s11 offset:20
	flat_load_dwordx2 v[29:30], v[31:32]
	v_xor_b32_e32 v45, v60, v0
	v_mov_b32_e32 v32, s17
	v_mov_b32_e32 v31, s16
	s_waitcnt vmcnt(0) lgkmcnt(0)
	buffer_store_dword v30, off, s[0:3], s11 offset:76
	buffer_store_dword v29, off, s[0:3], s11 offset:72
	s_waitcnt expcnt(0)
	flat_load_dwordx2 v[29:30], v[33:34]
	v_mov_b32_e32 v34, v28
	v_mov_b32_e32 v33, v27
	s_waitcnt vmcnt(0) lgkmcnt(0)
	buffer_store_dword v30, off, s[0:3], s11 offset:68
	buffer_store_dword v29, off, s[0:3], s11 offset:64
	s_waitcnt expcnt(0)
	flat_load_dwordx2 v[29:30], v[35:36]
	v_mov_b32_e32 v36, v28
	v_mov_b32_e32 v35, v27
	s_waitcnt vmcnt(0) lgkmcnt(0)
	buffer_store_dword v30, off, s[0:3], s11 offset:60
	buffer_store_dword v29, off, s[0:3], s11 offset:56
	s_waitcnt expcnt(0)
	flat_load_dwordx2 v[29:30], v[37:38]
	v_mov_b32_e32 v38, v28
	v_mov_b32_e32 v37, v27
	s_waitcnt vmcnt(0) lgkmcnt(0)
	buffer_store_dword v30, off, s[0:3], s11 offset:52
	buffer_store_dword v29, off, s[0:3], s11 offset:48
	s_waitcnt expcnt(0)
	flat_load_dwordx2 v[29:30], v[39:40]
	v_mov_b32_e32 v40, v28
	v_mov_b32_e32 v39, v27
	s_waitcnt vmcnt(0) lgkmcnt(0)
	buffer_store_dword v30, off, s[0:3], s11 offset:44
	buffer_store_dword v29, off, s[0:3], s11 offset:40
	s_waitcnt expcnt(0)
	flat_load_dwordx2 v[29:30], v[41:42]
	v_mov_b32_e32 v42, v28
	v_mov_b32_e32 v41, v27
	s_waitcnt vmcnt(0) lgkmcnt(0)
	buffer_store_dword v30, off, s[0:3], s11 offset:36
	buffer_store_dword v29, off, s[0:3], s11 offset:32
	s_waitcnt expcnt(0)
	flat_load_dwordx2 v[29:30], v[43:44]
	v_mov_b32_e32 v44, v28
	v_mov_b32_e32 v43, v27
	s_waitcnt vmcnt(0) lgkmcnt(0)
	buffer_store_dword v30, off, s[0:3], s11 offset:28
	buffer_store_dword v29, off, s[0:3], s11 offset:24
	buffer_store_dword v45, off, s[0:3], s11 offset:16
	buffer_load_dword v66, off, s[0:3], s11 offset:16
	buffer_load_dword v71, off, s[0:3], s11 offset:24
	buffer_load_dword v72, off, s[0:3], s11 offset:28
	buffer_load_dword v70, off, s[0:3], s11 offset:32
	buffer_load_dword v69, off, s[0:3], s11 offset:36
	buffer_load_dword v51, off, s[0:3], s11 offset:40
	buffer_load_dword v52, off, s[0:3], s11 offset:44
	buffer_load_dword v67, off, s[0:3], s11 offset:48
	buffer_load_dword v68, off, s[0:3], s11 offset:52
	buffer_load_dword v76, off, s[0:3], s11 offset:56
	buffer_load_dword v75, off, s[0:3], s11 offset:60
	buffer_load_dword v60, off, s[0:3], s11 offset:64
	buffer_load_dword v59, off, s[0:3], s11 offset:68
	buffer_load_dword v73, off, s[0:3], s11 offset:72
	buffer_load_dword v74, off, s[0:3], s11 offset:76
	s_waitcnt expcnt(0)
	v_mov_b32_e32 v46, v28
	v_mov_b32_e32 v30, v28
	v_mov_b32_e32 v45, v27
	v_mov_b32_e32 v29, v27
BB2_1:                                  ; %for.body18.i149
                                        ; =>This Inner Loop Header: Depth=1
	v_cmp_ne_u32_e32 vcc, 0, v1
	s_cbranch_vccnz BB2_3
; BB#2:                                 ; %do.body19.i329
                                        ;   in Loop: Header=BB2_1 Depth=1
	v_xor_b32_e32 v78, v41, v62
	v_xor_b32_e32 v79, v42, v63
	v_xor_b32_e32 v78, v78, v47
	v_xor_b32_e32 v79, v79, v48
	v_xor_b32_e32 v80, v45, v27
	v_xor_b32_e32 v84, v46, v28
	s_waitcnt vmcnt(2)
	v_xor_b32_e32 v79, v79, v59
	v_xor_b32_e32 v78, v78, v60
	v_xor_b32_e32 v80, v80, v35
	v_xor_b32_e32 v84, v84, v36
	v_xor_b32_e32 v80, v80, v29
	v_xor_b32_e32 v84, v84, v30
	v_xor_b32_e32 v78, v78, v71
	v_xor_b32_e32 v79, v79, v72
	v_xor_b32_e32 v80, v80, v67
	;;#ASMSTART
	v_alignbit_b32 v81, v78, v79, v25
v_alignbit_b32 v82, v79, v78, v25
	;;#ASMEND
	v_xor_b32_e32 v84, v84, v68
	v_xor_b32_e32 v81, v81, v80
	v_xor_b32_e32 v82, v82, v84
	v_xor_b32_e32 v77, v57, v64
	v_xor_b32_e32 v83, v58, v65
	v_xor_b32_e32 v87, v81, v57
	v_xor_b32_e32 v57, v54, v50
	v_xor_b32_e32 v88, v82, v58
	v_xor_b32_e32 v58, v53, v49
	v_xor_b32_e32 v57, v57, v34
	v_xor_b32_e32 v58, v58, v33
	v_xor_b32_e32 v77, v77, v43
	v_xor_b32_e32 v83, v83, v44
	s_waitcnt vmcnt(1)
	v_xor_b32_e32 v58, v58, v73
	s_waitcnt vmcnt(0)
	v_xor_b32_e32 v57, v57, v74
	v_xor_b32_e32 v77, v77, v76
	v_xor_b32_e32 v83, v83, v75
	v_xor_b32_e32 v57, v57, v69
	v_xor_b32_e32 v58, v58, v70
	v_xor_b32_e32 v77, v77, v66
	v_xor_b32_e32 v83, v83, v61
	v_xor_b32_e32 v85, v82, v44
	v_xor_b32_e32 v86, v81, v43
	v_xor_b32_e32 v43, v82, v65
	v_xor_b32_e32 v44, v81, v64
	;;#ASMSTART
	v_alignbit_b32 v64, v58, v57, v25
v_alignbit_b32 v65, v57, v58, v25
	;;#ASMEND
	v_xor_b32_e32 v65, v65, v83
	v_xor_b32_e32 v64, v64, v77
	v_xor_b32_e32 v66, v81, v66
	v_xor_b32_e32 v76, v81, v76
	v_xor_b32_e32 v81, v65, v48
	v_xor_b32_e32 v48, v37, v55
	v_xor_b32_e32 v61, v82, v61
	v_xor_b32_e32 v75, v82, v75
	v_xor_b32_e32 v82, v64, v47
	v_xor_b32_e32 v47, v38, v56
	v_xor_b32_e32 v47, v47, v40
	v_xor_b32_e32 v48, v48, v39
	v_xor_b32_e32 v48, v48, v31
	v_xor_b32_e32 v47, v47, v32
	v_xor_b32_e32 v47, v47, v52
	v_xor_b32_e32 v48, v48, v51
	v_xor_b32_e32 v72, v65, v72
	v_xor_b32_e32 v71, v64, v71
	v_xor_b32_e32 v60, v64, v60
	v_xor_b32_e32 v59, v65, v59
	v_xor_b32_e32 v41, v64, v41
	v_xor_b32_e32 v42, v65, v42
	v_xor_b32_e32 v63, v65, v63
	v_xor_b32_e32 v62, v64, v62
	;;#ASMSTART
	v_alignbit_b32 v64, v48, v47, v25
v_alignbit_b32 v65, v47, v48, v25
	;;#ASMEND
	v_xor_b32_e32 v65, v65, v79
	v_xor_b32_e32 v64, v64, v78
	v_xor_b32_e32 v78, v64, v33
	v_xor_b32_e32 v79, v65, v34
	v_xor_b32_e32 v33, v64, v49
	v_xor_b32_e32 v34, v65, v50
	;;#ASMSTART
	v_alignbit_b32 v49, v80, v84, v25
v_alignbit_b32 v50, v84, v80, v25
	;;#ASMEND
	v_xor_b32_e32 v49, v49, v58
	v_xor_b32_e32 v50, v50, v57
	v_xor_b32_e32 v70, v64, v70
	v_xor_b32_e32 v69, v65, v69
	v_xor_b32_e32 v74, v65, v74
	v_xor_b32_e32 v54, v65, v54
	v_xor_b32_e32 v73, v64, v73
	v_xor_b32_e32 v53, v64, v53
	v_xor_b32_e32 v52, v50, v52
	v_xor_b32_e32 v51, v49, v51
	v_xor_b32_e32 v64, v49, v31
	v_xor_b32_e32 v65, v50, v32
	;;#ASMSTART
	v_alignbit_b32 v31, v77, v83, v25
v_alignbit_b32 v32, v83, v77, v25
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v91, v51, v52, v19
v_alignbit_b32 v92, v52, v51, v19
	;;#ASMEND
	v_xor_b32_e32 v31, v31, v48
	v_xor_b32_e32 v32, v32, v47
	;;#ASMSTART
	v_alignbit_b32 v77, v34, v33, v26
v_alignbit_b32 v83, v33, v34, v26
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v89, v42, v41, v17
v_alignbit_b32 v90, v41, v42, v17
	;;#ASMEND
	v_xor_b32_e32 v80, v50, v38
	v_xor_b32_e32 v84, v49, v37
	v_xor_b32_e32 v37, v49, v55
	v_xor_b32_e32 v39, v49, v39
	v_xor_b32_e32 v40, v50, v40
	v_xor_b32_e32 v38, v50, v56
	v_xor_b32_e32 v49, v32, v68
	v_xor_b32_e32 v50, v31, v67
	v_xor_b32_e32 v67, v32, v28
	v_xor_b32_e32 v68, v31, v27
	v_xor_b32_e32 v30, v32, v30
	v_xor_b32_e32 v29, v31, v29
	v_xor_b32_e32 v27, v92, v90
	v_and_b32_e32 v28, v92, v83
	v_xor_b32_e32 v55, v31, v35
	v_xor_b32_e32 v56, v32, v36
	v_xor_b32_e32 v35, v32, v46
	v_xor_b32_e32 v32, v27, v28
	v_xor_b32_e32 v27, v91, v89
	v_and_b32_e32 v28, v91, v77
	;;#ASMSTART
	v_alignbit_b32 v93, v29, v30, v4
v_alignbit_b32 v94, v30, v29, v4
	;;#ASMEND
	v_xor_b32_e32 v36, v31, v45
	v_xor_b32_e32 v31, v27, v28
	v_xor_b32_e32 v27, v83, v94
	v_and_b32_e32 v28, v92, v94
	v_xor_b32_e32 v30, v27, v28
	v_xor_b32_e32 v27, v77, v93
	v_and_b32_e32 v28, v91, v93
	v_xor_b32_e32 v29, v27, v28
	;;#ASMSTART
	v_alignbit_b32 v27, v44, v43, v6
v_alignbit_b32 v28, v43, v44, v6
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v41, v39, v40, v9
v_alignbit_b32 v42, v40, v39, v9
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v35, v36, v35, v10
v_alignbit_b32 v36, v35, v36, v10
	;;#ASMEND
	v_xor_b32_e32 v33, v42, v28
	v_and_b32_e32 v34, v36, v28
	;;#ASMSTART
	v_alignbit_b32 v45, v71, v72, v25
v_alignbit_b32 v46, v72, v71, v25
	;;#ASMEND
	v_xor_b32_e32 v34, v33, v34
	v_xor_b32_e32 v33, v41, v27
	v_and_b32_e32 v39, v35, v27
	v_xor_b32_e32 v33, v33, v39
	v_xor_b32_e32 v39, v46, v36
	v_and_b32_e32 v40, v46, v28
	;;#ASMSTART
	v_alignbit_b32 v51, v73, v74, v23
v_alignbit_b32 v52, v74, v73, v23
	;;#ASMEND
	v_xor_b32_e32 v40, v39, v40
	v_and_b32_e32 v43, v45, v27
	v_xor_b32_e32 v39, v45, v35
	v_xor_b32_e32 v39, v39, v43
	v_not_b32_e32 v43, v51
	v_and_b32_e32 v43, v41, v43
	v_not_b32_e32 v41, v41
	v_and_b32_e32 v35, v35, v41
	v_not_b32_e32 v41, v42
	v_not_b32_e32 v44, v52
	v_and_b32_e32 v36, v36, v41
	v_and_b32_e32 v44, v42, v44
	v_xor_b32_e32 v48, v52, v36
	v_xor_b32_e32 v47, v51, v35
	v_xor_b32_e32 v27, v51, v27
	v_and_b32_e32 v35, v45, v51
	v_xor_b32_e32 v28, v52, v28
	v_and_b32_e32 v36, v46, v52
	v_xor_b32_e32 v44, v46, v44
	v_xor_b32_e32 v43, v45, v43
	;;#ASMSTART
	v_alignbit_b32 v45, v75, v76, v18
v_alignbit_b32 v46, v76, v75, v18
	;;#ASMEND
	v_xor_b32_e32 v36, v28, v36
	v_xor_b32_e32 v35, v27, v35
	;;#ASMSTART
	v_alignbit_b32 v27, v50, v49, v13
v_alignbit_b32 v28, v49, v50, v13
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v49, v82, v81, v22
v_alignbit_b32 v50, v81, v82, v22
	;;#ASMEND
	v_xor_b32_e32 v41, v50, v28
	v_and_b32_e32 v42, v50, v46
	v_xor_b32_e32 v58, v41, v42
	;;#ASMSTART
	v_alignbit_b32 v51, v53, v54, v21
v_alignbit_b32 v52, v54, v53, v21
	;;#ASMEND
	v_xor_b32_e32 v41, v49, v27
	v_and_b32_e32 v42, v49, v45
	v_xor_b32_e32 v57, v41, v42
	;;#ASMSTART
	v_alignbit_b32 v71, v38, v37, v11
v_alignbit_b32 v72, v37, v38, v11
	;;#ASMEND
	v_not_b32_e32 v37, v51
	v_not_b32_e32 v38, v52
	v_xor_b32_e32 v41, v52, v46
	v_and_b32_e32 v42, v50, v52
	v_xor_b32_e32 v42, v41, v42
	v_and_b32_e32 v38, v72, v38
	v_xor_b32_e32 v41, v51, v45
	v_and_b32_e32 v53, v49, v51
	v_and_b32_e32 v37, v71, v37
	v_xor_b32_e32 v41, v41, v53
	v_xor_b32_e32 v53, v37, v49
	v_not_b32_e32 v37, v71
	v_xor_b32_e32 v54, v38, v50
	v_not_b32_e32 v38, v72
	v_and_b32_e32 v37, v27, v37
	v_and_b32_e32 v38, v28, v38
	v_xor_b32_e32 v49, v45, v71
	v_and_b32_e32 v27, v45, v27
	v_xor_b32_e32 v45, v46, v72
	v_and_b32_e32 v28, v46, v28
	v_xor_b32_e32 v46, v45, v28
	v_xor_b32_e32 v45, v49, v27
	v_xor_b32_e32 v38, v52, v38
	v_xor_b32_e32 v37, v51, v37
	;;#ASMSTART
	v_alignbit_b32 v51, v69, v70, v7
v_alignbit_b32 v52, v70, v69, v7
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v27, v56, v55, v5
v_alignbit_b32 v28, v55, v56, v5
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v69, v65, v64, v16
v_alignbit_b32 v70, v64, v65, v16
	;;#ASMEND
	v_xor_b32_e32 v49, v52, v28
	v_and_b32_e32 v50, v70, v28
	;;#ASMSTART
	v_alignbit_b32 v73, v62, v63, v15
v_alignbit_b32 v74, v63, v62, v15
	;;#ASMEND
	v_xor_b32_e32 v65, v49, v50
	;;#ASMSTART
	v_alignbit_b32 v71, v88, v87, v12
v_alignbit_b32 v72, v87, v88, v12
	;;#ASMEND
	v_xor_b32_e32 v49, v51, v27
	v_and_b32_e32 v50, v69, v27
	v_xor_b32_e32 v64, v49, v50
	v_xor_b32_e32 v49, v74, v28
	v_and_b32_e32 v50, v74, v72
	v_xor_b32_e32 v50, v49, v50
	v_xor_b32_e32 v49, v73, v27
	v_and_b32_e32 v55, v73, v71
	v_not_b32_e32 v27, v27
	v_not_b32_e32 v28, v28
	v_xor_b32_e32 v49, v49, v55
	v_xor_b32_e32 v55, v72, v52
	v_and_b32_e32 v56, v74, v52
	v_xor_b32_e32 v56, v55, v56
	v_and_b32_e32 v28, v72, v28
	v_xor_b32_e32 v55, v71, v51
	v_and_b32_e32 v62, v73, v51
	v_and_b32_e32 v27, v71, v27
	v_xor_b32_e32 v55, v55, v62
	v_xor_b32_e32 v63, v70, v28
	v_xor_b32_e32 v62, v69, v27
	v_xor_b32_e32 v27, v69, v73
	v_and_b32_e32 v51, v69, v51
	v_xor_b32_e32 v28, v70, v74
	v_and_b32_e32 v52, v70, v52
	v_xor_b32_e32 v28, v28, v52
	v_xor_b32_e32 v27, v27, v51
	;;#ASMSTART
	v_alignbit_b32 v51, v59, v60, v3
v_alignbit_b32 v52, v60, v59, v3
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v59, v79, v78, v8
v_alignbit_b32 v60, v78, v79, v8
	;;#ASMEND
	v_xor_b32_e32 v69, v59, v66
	v_and_b32_e32 v70, v59, v51
	v_xor_b32_e32 v78, v69, v70
	v_xor_b32_e32 v69, v60, v61
	v_and_b32_e32 v70, v60, v52
	v_xor_b32_e32 v79, v69, v70
	v_not_b32_e32 v69, v89
	v_and_b32_e32 v73, v77, v69
	v_not_b32_e32 v69, v90
	;;#ASMSTART
	v_alignbit_b32 v67, v68, v67, v14
v_alignbit_b32 v68, v67, v68, v14
	;;#ASMEND
	s_ashr_i32 s19, s18, 31
	v_and_b32_e32 v74, v83, v69
	v_not_b32_e32 v69, v67
	v_and_b32_e32 v81, v66, v69
	v_not_b32_e32 v69, v68
	s_add_i32 s4, s18, 1
	s_lshl_b64 s[16:17], s[18:19], 3
	s_add_u32 s16, s16, s6
	;;#ASMSTART
	v_alignbit_b32 v75, v84, v80, v20
v_alignbit_b32 v76, v80, v84, v20
	;;#ASMEND
	v_and_b32_e32 v82, v61, v69
	v_not_b32_e32 v61, v61
	s_addc_u32 s17, s17, s7
	v_not_b32_e32 v66, v66
	v_and_b32_e32 v61, v52, v61
	v_xor_b32_e32 v52, v76, v52
	v_and_b32_e32 v69, v76, v60
	v_and_b32_e32 v66, v51, v66
	v_xor_b32_e32 v72, v52, v69
	s_load_dwordx2 s[16:17], s[16:17], 0x0
	v_xor_b32_e32 v51, v75, v51
	v_and_b32_e32 v52, v75, v59
	;;#ASMSTART
	v_alignbit_b32 v77, v86, v85, v24
v_alignbit_b32 v80, v85, v86, v24
	;;#ASMEND
	v_xor_b32_e32 v71, v51, v52
	v_xor_b32_e32 v51, v67, v59
	v_xor_b32_e32 v52, v68, v60
	v_and_b32_e32 v59, v76, v68
	v_xor_b32_e32 v69, v52, v59
	v_and_b32_e32 v52, v75, v67
	v_xor_b32_e32 v68, v68, v61
	v_xor_b32_e32 v60, v80, v92
	v_and_b32_e32 v61, v80, v94
	v_xor_b32_e32 v70, v51, v52
	v_xor_b32_e32 v51, v75, v81
	v_xor_b32_e32 v75, v60, v61
	v_xor_b32_e32 v59, v77, v91
	v_and_b32_e32 v60, v77, v93
	v_xor_b32_e32 v52, v76, v82
	v_xor_b32_e32 v76, v59, v60
	v_xor_b32_e32 v67, v67, v66
	v_xor_b32_e32 v60, v89, v93
	v_and_b32_e32 v61, v77, v89
	v_xor_b32_e32 v59, v90, v94
	v_and_b32_e32 v66, v80, v90
	v_xor_b32_e32 v59, v59, v66
	v_xor_b32_e32 v60, v60, v61
	v_xor_b32_e32 v74, v80, v74
	v_xor_b32_e32 v73, v77, v73
	s_mov_b32 s18, s4
	s_waitcnt lgkmcnt(0)
	v_xor_b32_e32 v61, s17, v79
	v_xor_b32_e32 v66, s16, v78
BB2_3:                                  ; %if.end.i332
                                        ;   in Loop: Header=BB2_1 Depth=1
	s_cmp_lt_i32 s18, 23
	s_cbranch_scc1 BB2_1
; BB#4:                                 ; %SHA3_512.exit333
	s_waitcnt vmcnt(1)
	v_xor_b32_e32 v3, v70, v73
	s_waitcnt vmcnt(0)
	v_xor_b32_e32 v4, v69, v74
	v_xor_b32_e32 v5, v61, v75
	v_xor_b32_e32 v6, v66, v76
	v_xor_b32_e32 v7, v72, v59
	v_xor_b32_e32 v8, v71, v60
	v_xor_b32_e32 v4, v4, v50
	v_xor_b32_e32 v3, v3, v49
	v_xor_b32_e32 v9, v27, v67
	v_xor_b32_e32 v10, v28, v68
	v_xor_b32_e32 v6, v6, v64
	v_xor_b32_e32 v5, v5, v65
	v_xor_b32_e32 v3, v3, v53
	v_xor_b32_e32 v4, v4, v54
	v_xor_b32_e32 v8, v8, v62
	v_xor_b32_e32 v7, v7, v63
	v_xor_b32_e32 v11, v55, v51
	v_xor_b32_e32 v12, v56, v52
	v_xor_b32_e32 v5, v5, v58
	v_xor_b32_e32 v6, v6, v57
	v_xor_b32_e32 v7, v7, v42
	v_xor_b32_e32 v8, v8, v41
	v_xor_b32_e32 v12, v12, v38
	v_xor_b32_e32 v11, v11, v37
	v_xor_b32_e32 v10, v10, v46
	v_xor_b32_e32 v9, v9, v45
	v_mov_b32_e32 v13, 31
	v_xor_b32_e32 v4, v4, v34
	v_xor_b32_e32 v3, v3, v33
	v_xor_b32_e32 v11, v11, v39
	v_xor_b32_e32 v12, v12, v40
	v_xor_b32_e32 v9, v9, v35
	v_xor_b32_e32 v10, v10, v36
	v_xor_b32_e32 v8, v8, v47
	v_xor_b32_e32 v7, v7, v48
	v_xor_b32_e32 v6, v6, v43
	;;#ASMSTART
	v_alignbit_b32 v18, v3, v4, v13
v_alignbit_b32 v19, v4, v3, v13
	;;#ASMEND
	v_xor_b32_e32 v5, v5, v44
	v_xor_b32_e32 v18, v18, v6
	v_xor_b32_e32 v19, v19, v5
	v_xor_b32_e32 v12, v12, v32
	;;#ASMSTART
	v_alignbit_b32 v5, v6, v5, v13
v_alignbit_b32 v6, v5, v6, v13
	;;#ASMEND
	v_xor_b32_e32 v11, v11, v31
	v_xor_b32_e32 v10, v10, v30
	;;#ASMSTART
	v_alignbit_b32 v14, v8, v7, v13
v_alignbit_b32 v15, v7, v8, v13
	;;#ASMEND
	v_xor_b32_e32 v9, v9, v29
	;;#ASMSTART
	v_alignbit_b32 v22, v11, v12, v13
v_alignbit_b32 v23, v12, v11, v13
	;;#ASMEND
	v_xor_b32_e32 v15, v15, v10
	v_xor_b32_e32 v14, v14, v9
	;;#ASMSTART
	v_alignbit_b32 v9, v9, v10, v13
v_alignbit_b32 v10, v10, v9, v13
	;;#ASMEND
	v_xor_b32_e32 v20, v18, v60
	v_xor_b32_e32 v21, v19, v59
	v_mov_b32_e32 v13, 20
	v_xor_b32_e32 v5, v5, v11
	v_xor_b32_e32 v6, v6, v12
	v_xor_b32_e32 v7, v23, v7
	v_xor_b32_e32 v8, v22, v8
	v_xor_b32_e32 v11, v6, v30
	v_xor_b32_e32 v12, v5, v29
	;;#ASMSTART
	v_alignbit_b32 v13, v21, v20, v13
v_alignbit_b32 v20, v20, v21, v13
	;;#ASMEND
	v_mov_b32_e32 v21, 12
	v_xor_b32_e32 v22, v8, v33
	v_xor_b32_e32 v23, v7, v34
	;;#ASMSTART
	v_alignbit_b32 v11, v12, v11, v21
v_alignbit_b32 v12, v11, v12, v21
	;;#ASMEND
	v_xor_b32_e32 v8, v8, v49
	v_xor_b32_e32 v7, v7, v50
	v_mov_b32_e32 v21, 3
	;;#ASMSTART
	v_alignbit_b32 v7, v7, v8, v21
v_alignbit_b32 v8, v8, v7, v21
	;;#ASMEND
	v_mov_b32_e32 v21, 21
	;;#ASMSTART
	v_alignbit_b32 v21, v23, v22, v21
v_alignbit_b32 v22, v22, v23, v21
	;;#ASMEND
	v_xor_b32_e32 v6, v6, v28
	v_xor_b32_e32 v5, v5, v27
	v_mov_b32_e32 v23, 18
	;;#ASMSTART
	v_alignbit_b32 v5, v5, v6, v23
v_alignbit_b32 v6, v6, v5, v23
	;;#ASMEND
	v_xor_b32_e32 v18, v18, v41
	v_xor_b32_e32 v19, v19, v42
	v_mov_b32_e32 v23, 19
	v_xor_b32_e32 v3, v9, v3
	v_xor_b32_e32 v4, v10, v4
	v_xor_b32_e32 v9, v4, v52
	v_xor_b32_e32 v10, v3, v51
	;;#ASMSTART
	v_alignbit_b32 v18, v19, v18, v23
v_alignbit_b32 v19, v18, v19, v23
	;;#ASMEND
	v_mov_b32_e32 v23, 4
	;;#ASMSTART
	v_alignbit_b32 v9, v10, v9, v23
v_alignbit_b32 v10, v9, v10, v23
	;;#ASMEND
	v_xor_b32_e32 v4, v4, v38
	v_xor_b32_e32 v3, v3, v37
	v_mov_b32_e32 v23, 11
	;;#ASMSTART
	v_alignbit_b32 v3, v3, v4, v23
v_alignbit_b32 v4, v4, v3, v23
	;;#ASMEND
	v_xor_b32_e32 v16, v15, v61
	v_xor_b32_e32 v17, v14, v66
	v_xor_b32_e32 v15, v15, v44
	v_xor_b32_e32 v14, v14, v43
	v_mov_b32_e32 v23, 29
	;;#ASMSTART
	v_alignbit_b32 v14, v14, v15, v23
v_alignbit_b32 v15, v15, v14, v23
	;;#ASMEND
	v_and_b32_e32 v23, v22, v20
	v_xor_b32_e32 v24, v16, v22
	v_xor_b32_e32 v23, v24, v23
	v_and_b32_e32 v24, v21, v13
	v_xor_b32_e32 v25, v17, v21
	v_xor_b32_e32 v24, v25, v24
	v_not_b32_e32 v26, v5
	v_not_b32_e32 v25, v19
	v_and_b32_e32 v8, v8, v25
	v_and_b32_e32 v26, v17, v26
	v_not_b32_e32 v17, v17
	v_not_b32_e32 v25, v18
	v_and_b32_e32 v7, v7, v25
	v_not_b32_e32 v25, v6
	v_and_b32_e32 v17, v13, v17
	v_and_b32_e32 v28, v3, v21
	v_xor_b32_e32 v13, v3, v13
	v_and_b32_e32 v25, v16, v25
	v_not_b32_e32 v16, v16
	v_and_b32_e32 v27, v4, v22
	v_xor_b32_e32 v70, v13, v28
	v_xor_b32_e32 v22, v6, v22
	v_and_b32_e32 v13, v4, v6
	v_and_b32_e32 v16, v20, v16
	v_xor_b32_e32 v20, v4, v20
	v_and_b32_e32 v29, v15, v12
	v_xor_b32_e32 v10, v15, v10
	v_xor_b32_e32 v12, v19, v12
	v_and_b32_e32 v30, v14, v11
	v_xor_b32_e32 v9, v14, v9
	v_xor_b32_e32 v11, v18, v11
	v_and_b32_e32 v19, v15, v19
	v_and_b32_e32 v18, v14, v18
	v_xor_b32_e32 v64, v22, v13
	v_xor_b32_e32 v21, v5, v21
	v_and_b32_e32 v13, v3, v5
	v_xor_b32_e32 v54, 0x80000000, v23
	v_xor_b32_e32 v57, 0x80008008, v24
	v_xor_b32_e32 v46, v3, v26
	v_xor_b32_e32 v63, v5, v17
	s_mov_b32 s10, 3
	v_xor_b32_e32 v71, v20, v27
	v_xor_b32_e32 v65, v21, v13
	v_xor_b32_e32 v47, v4, v25
	v_xor_b32_e32 v62, v6, v16
	v_xor_b32_e32 v68, v10, v29
	v_xor_b32_e32 v69, v9, v30
	v_xor_b32_e32 v48, v12, v19
	v_xor_b32_e32 v49, v11, v18
	v_xor_b32_e32 v66, v15, v8
	v_xor_b32_e32 v67, v14, v7
	s_mov_b32 s13, 0x1000193
	v_mov_b32_e32 v3, 16
	v_mov_b32_e32 v5, 0
	buffer_store_dword v54, off, s[0:3], s11 offset:20
	buffer_store_dword v57, off, s[0:3], s11 offset:16
	buffer_store_dword v71, off, s[0:3], s11 offset:28
	buffer_store_dword v70, off, s[0:3], s11 offset:24
	buffer_store_dword v64, off, s[0:3], s11 offset:36
	buffer_store_dword v65, off, s[0:3], s11 offset:32
	buffer_store_dword v47, off, s[0:3], s11 offset:44
	buffer_store_dword v46, off, s[0:3], s11 offset:40
	buffer_store_dword v62, off, s[0:3], s11 offset:52
	buffer_store_dword v63, off, s[0:3], s11 offset:48
	buffer_store_dword v68, off, s[0:3], s11 offset:60
	buffer_store_dword v69, off, s[0:3], s11 offset:56
	buffer_store_dword v48, off, s[0:3], s11 offset:68
	buffer_store_dword v49, off, s[0:3], s11 offset:64
	buffer_store_dword v66, off, s[0:3], s11 offset:76
	buffer_store_dword v67, off, s[0:3], s11 offset:72
BB2_5:                                  ; %for.body
                                        ; =>This Inner Loop Header: Depth=1
	s_add_i32 s4, s10, -3
	s_and_b32 s5, s4, 12
	s_lshl_b32 s5, s5, 2
	v_add_u32_e32 v4, vcc, s5, v3
	buffer_load_dword v4, v4, s[0:3], s11 offen
	v_xor_b32_e32 v6, s4, v0
	v_mul_lo_i32 v6, v6, s13
	s_add_i32 s4, s10, -2
	s_add_i32 s5, s10, -1
	s_and_b32 s16, s10, 15
	v_xor_b32_e32 v13, s4, v0
	s_and_b32 s4, s4, 13
	v_xor_b32_e32 v14, s5, v0
	s_and_b32 s5, s5, 14
	s_lshl_b32 s16, s16, 2
	s_lshl_b32 s4, s4, 2
	s_lshl_b32 s5, s5, 2
	v_mul_lo_i32 v39, v13, s13
	v_mov_b32_e32 v9, s15
	v_mul_lo_i32 v40, v14, s13
	v_mul_lo_i32 v19, v71, s13
	v_mul_lo_i32 v20, v70, s13
	v_mul_lo_i32 v21, v54, s13
	v_mul_lo_i32 v22, v57, s13
	v_mul_lo_i32 v23, v47, s13
	v_mul_lo_i32 v24, v46, s13
	v_mul_lo_i32 v25, v64, s13
	v_mul_lo_i32 v26, v65, s13
	v_mul_lo_i32 v27, v68, s13
	v_mul_lo_i32 v28, v69, s13
	v_mul_lo_i32 v29, v62, s13
	v_mul_lo_i32 v30, v63, s13
	v_mul_lo_i32 v31, v66, s13
	v_mul_lo_i32 v32, v67, s13
	v_mul_lo_i32 v33, v48, s13
	v_mul_lo_i32 v34, v49, s13
	v_mov_b32_e32 v10, v5
	v_mov_b32_e32 v35, s15
	v_mov_b32_e32 v11, v5
	v_mov_b32_e32 v36, s15
	v_xor_b32_e32 v37, s10, v0
	v_mov_b32_e32 v12, v5
	v_mov_b32_e32 v38, s15
	s_add_i32 s17, s10, 4
	s_cmpk_eq_i32 s10, 0xff
	s_mov_b32 s10, s17
	s_waitcnt vmcnt(0)
	v_xor_b32_e32 v4, v4, v6
	v_mul_hi_u32 v6, v2, v4
	v_mul_lo_i32 v6, v6, s12
	v_subrev_u32_e32 v7, vcc, v6, v4
	v_cmp_ge_u32_e32 vcc, v4, v6
	v_cndmask_b32_e64 v4, 0, -1, vcc
	v_cmp_le_u32_e32 vcc, s12, v7
	v_cndmask_b32_e64 v6, 0, -1, vcc
	v_subrev_u32_e32 v8, vcc, s12, v7
	v_add_u32_e32 v41, vcc, s16, v3
	v_add_u32_e32 v42, vcc, s4, v3
	v_add_u32_e32 v43, vcc, s5, v3
	v_add_u32_e32 v13, vcc, s12, v7
	v_cmp_eq_u32_e32 vcc, 0, v4
	v_and_b32_e32 v4, v6, v4
	v_cmp_eq_u32_e64 s[4:5], 0, v4
	v_cndmask_b32_e64 v4, v8, v7, s[4:5]
	v_cndmask_b32_e32 v4, v4, v13, vcc
	v_lshlrev_b64 v[6:7], 6, v[4:5]
	v_add_u32_e32 v13, vcc, s14, v6
	v_addc_u32_e32 v14, vcc, v9, v7, vcc
	flat_load_dwordx4 v[6:9], v[13:14]
	v_add_u32_e32 v15, vcc, 16, v13
	v_addc_u32_e32 v16, vcc, 0, v14, vcc
	v_add_u32_e32 v17, vcc, 32, v13
	v_addc_u32_e32 v18, vcc, 0, v14, vcc
	v_add_u32_e32 v13, vcc, 48, v13
	v_addc_u32_e32 v14, vcc, 0, v14, vcc
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v4, v6, v22
	v_xor_b32_e32 v6, v7, v21
	v_xor_b32_e32 v7, v8, v20
	v_xor_b32_e32 v8, v9, v19
	buffer_store_dword v8, off, s[0:3], s11 offset:28
	buffer_store_dword v7, off, s[0:3], s11 offset:24
	buffer_store_dword v6, off, s[0:3], s11 offset:20
	buffer_store_dword v4, off, s[0:3], s11 offset:16
	v_mul_lo_i32 v19, v8, s13
	v_mul_lo_i32 v20, v7, s13
	v_mul_lo_i32 v21, v6, s13
	s_waitcnt expcnt(1)
	flat_load_dwordx4 v[6:9], v[15:16]
	s_waitcnt expcnt(0)
	v_mul_lo_i32 v4, v4, s13
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v9, v9, v23
	v_xor_b32_e32 v8, v8, v24
	v_xor_b32_e32 v7, v7, v25
	v_xor_b32_e32 v6, v6, v26
	buffer_store_dword v9, off, s[0:3], s11 offset:44
	buffer_store_dword v8, off, s[0:3], s11 offset:40
	buffer_store_dword v7, off, s[0:3], s11 offset:36
	buffer_store_dword v6, off, s[0:3], s11 offset:32
	v_mul_lo_i32 v22, v9, s13
	v_mul_lo_i32 v23, v8, s13
	v_mul_lo_i32 v24, v7, s13
	v_mul_lo_i32 v25, v6, s13
	s_waitcnt expcnt(0)
	flat_load_dwordx4 v[6:9], v[17:18]
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v9, v9, v27
	v_xor_b32_e32 v8, v8, v28
	v_xor_b32_e32 v7, v7, v29
	v_xor_b32_e32 v6, v6, v30
	buffer_store_dword v9, off, s[0:3], s11 offset:60
	buffer_store_dword v8, off, s[0:3], s11 offset:56
	buffer_store_dword v7, off, s[0:3], s11 offset:52
	buffer_store_dword v6, off, s[0:3], s11 offset:48
	v_mul_lo_i32 v26, v9, s13
	v_mul_lo_i32 v27, v8, s13
	v_mul_lo_i32 v28, v7, s13
	v_mul_lo_i32 v29, v6, s13
	s_waitcnt expcnt(0)
	flat_load_dwordx4 v[6:9], v[13:14]
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v9, v9, v31
	v_xor_b32_e32 v8, v8, v32
	v_xor_b32_e32 v7, v7, v33
	v_xor_b32_e32 v6, v6, v34
	buffer_store_dword v9, off, s[0:3], s11 offset:76
	buffer_store_dword v8, off, s[0:3], s11 offset:72
	buffer_store_dword v7, off, s[0:3], s11 offset:68
	buffer_store_dword v6, off, s[0:3], s11 offset:64
	v_mul_lo_i32 v33, v6, s13
	s_waitcnt expcnt(0)
	buffer_load_dword v6, v42, s[0:3], s11 offen
	v_mul_lo_i32 v32, v7, s13
	v_mul_lo_i32 v31, v8, s13
	v_mul_lo_i32 v30, v9, s13
	s_waitcnt vmcnt(0)
	v_xor_b32_e32 v6, v6, v39
	v_mul_hi_u32 v7, v2, v6
	v_mul_lo_i32 v7, v7, s12
	v_cmp_ge_u32_e32 vcc, v6, v7
	v_cndmask_b32_e64 v8, 0, -1, vcc
	v_subrev_u32_e32 v6, vcc, v7, v6
	v_cmp_le_u32_e32 vcc, s12, v6
	v_cndmask_b32_e64 v7, 0, -1, vcc
	v_and_b32_e32 v7, v7, v8
	v_subrev_u32_e32 v9, vcc, s12, v6
	v_add_u32_e32 v13, vcc, s12, v6
	v_cmp_eq_u32_e64 s[4:5], 0, v7
	v_cmp_eq_u32_e32 vcc, 0, v8
	v_cndmask_b32_e64 v6, v9, v6, s[4:5]
	v_cndmask_b32_e32 v9, v6, v13, vcc
	v_lshlrev_b64 v[6:7], 6, v[9:10]
	v_add_u32_e32 v13, vcc, s14, v6
	v_addc_u32_e32 v14, vcc, v35, v7, vcc
	flat_load_dwordx4 v[6:9], v[13:14]
	v_add_u32_e32 v15, vcc, 16, v13
	v_addc_u32_e32 v16, vcc, 0, v14, vcc
	v_add_u32_e32 v17, vcc, 32, v13
	v_addc_u32_e32 v18, vcc, 0, v14, vcc
	v_add_u32_e32 v13, vcc, 48, v13
	v_addc_u32_e32 v14, vcc, 0, v14, vcc
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v4, v6, v4
	v_xor_b32_e32 v6, v7, v21
	v_xor_b32_e32 v7, v8, v20
	v_xor_b32_e32 v8, v9, v19
	buffer_store_dword v8, off, s[0:3], s11 offset:28
	buffer_store_dword v7, off, s[0:3], s11 offset:24
	buffer_store_dword v6, off, s[0:3], s11 offset:20
	buffer_store_dword v4, off, s[0:3], s11 offset:16
	v_mul_lo_i32 v19, v8, s13
	v_mul_lo_i32 v20, v7, s13
	v_mul_lo_i32 v21, v6, s13
	s_waitcnt expcnt(1)
	flat_load_dwordx4 v[6:9], v[15:16]
	s_waitcnt expcnt(0)
	v_mul_lo_i32 v4, v4, s13
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v25, v6, v25
	v_xor_b32_e32 v6, v7, v24
	v_xor_b32_e32 v7, v8, v23
	v_xor_b32_e32 v8, v9, v22
	buffer_store_dword v8, off, s[0:3], s11 offset:44
	buffer_store_dword v7, off, s[0:3], s11 offset:40
	buffer_store_dword v6, off, s[0:3], s11 offset:36
	buffer_store_dword v25, off, s[0:3], s11 offset:32
	v_mul_lo_i32 v22, v8, s13
	v_mul_lo_i32 v23, v7, s13
	v_mul_lo_i32 v24, v6, s13
	s_waitcnt expcnt(1)
	flat_load_dwordx4 v[6:9], v[17:18]
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v17, v6, v29
	v_xor_b32_e32 v6, v7, v28
	v_xor_b32_e32 v7, v8, v27
	v_xor_b32_e32 v8, v9, v26
	buffer_store_dword v8, off, s[0:3], s11 offset:60
	buffer_store_dword v7, off, s[0:3], s11 offset:56
	buffer_store_dword v6, off, s[0:3], s11 offset:52
	buffer_store_dword v17, off, s[0:3], s11 offset:48
	v_mul_lo_i32 v18, v8, s13
	v_mul_lo_i32 v26, v7, s13
	v_mul_lo_i32 v27, v6, s13
	s_waitcnt expcnt(1)
	flat_load_dwordx4 v[6:9], v[13:14]
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v28, v6, v33
	v_xor_b32_e32 v6, v7, v32
	v_xor_b32_e32 v7, v8, v31
	v_xor_b32_e32 v8, v9, v30
	buffer_store_dword v8, off, s[0:3], s11 offset:76
	buffer_store_dword v7, off, s[0:3], s11 offset:72
	buffer_store_dword v6, off, s[0:3], s11 offset:68
	buffer_store_dword v28, off, s[0:3], s11 offset:64
	v_mul_lo_i32 v31, v6, s13
	s_waitcnt expcnt(1)
	buffer_load_dword v6, v43, s[0:3], s11 offen
	v_mul_lo_i32 v30, v7, s13
	v_mul_lo_i32 v29, v8, s13
	s_waitcnt vmcnt(0)
	v_xor_b32_e32 v6, v6, v40
	v_mul_hi_u32 v7, v2, v6
	v_mul_lo_i32 v7, v7, s12
	v_cmp_ge_u32_e32 vcc, v6, v7
	v_cndmask_b32_e64 v8, 0, -1, vcc
	v_subrev_u32_e32 v6, vcc, v7, v6
	v_cmp_le_u32_e32 vcc, s12, v6
	v_cndmask_b32_e64 v7, 0, -1, vcc
	v_and_b32_e32 v7, v7, v8
	v_subrev_u32_e32 v9, vcc, s12, v6
	v_add_u32_e32 v10, vcc, s12, v6
	v_cmp_eq_u32_e64 s[4:5], 0, v7
	v_cmp_eq_u32_e32 vcc, 0, v8
	v_cndmask_b32_e64 v6, v9, v6, s[4:5]
	v_cndmask_b32_e32 v10, v6, v10, vcc
	v_lshlrev_b64 v[6:7], 6, v[10:11]
	v_add_u32_e32 v10, vcc, s14, v6
	v_addc_u32_e32 v11, vcc, v36, v7, vcc
	flat_load_dwordx4 v[6:9], v[10:11]
	v_add_u32_e32 v13, vcc, 16, v10
	v_addc_u32_e32 v14, vcc, 0, v11, vcc
	v_add_u32_e32 v15, vcc, 32, v10
	v_addc_u32_e32 v16, vcc, 0, v11, vcc
	v_add_u32_e32 v10, vcc, 48, v10
	v_addc_u32_e32 v11, vcc, 0, v11, vcc
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v4, v6, v4
	v_xor_b32_e32 v6, v9, v19
	v_xor_b32_e32 v20, v8, v20
	v_xor_b32_e32 v21, v7, v21
	buffer_store_dword v6, off, s[0:3], s11 offset:28
	buffer_store_dword v20, off, s[0:3], s11 offset:24
	buffer_store_dword v21, off, s[0:3], s11 offset:20
	buffer_store_dword v4, off, s[0:3], s11 offset:16
	v_mul_lo_i32 v19, v6, s13
	s_waitcnt expcnt(3)
	flat_load_dwordx4 v[6:9], v[13:14]
	v_mul_lo_i32 v13, v25, s13
	s_waitcnt expcnt(0)
	v_mul_lo_i32 v4, v4, s13
	v_mul_lo_i32 v21, v21, s13
	v_mul_lo_i32 v20, v20, s13
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v13, v6, v13
	v_xor_b32_e32 v14, v7, v24
	v_xor_b32_e32 v23, v8, v23
	v_xor_b32_e32 v22, v9, v22
	buffer_store_dword v22, off, s[0:3], s11 offset:44
	buffer_store_dword v23, off, s[0:3], s11 offset:40
	buffer_store_dword v14, off, s[0:3], s11 offset:36
	buffer_store_dword v13, off, s[0:3], s11 offset:32
	flat_load_dwordx4 v[6:9], v[15:16]
	v_mul_lo_i32 v15, v17, s13
	s_waitcnt expcnt(2)
	v_mul_lo_i32 v23, v23, s13
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v6, v6, v15
	v_xor_b32_e32 v8, v8, v26
	v_xor_b32_e32 v16, v9, v18
	v_xor_b32_e32 v7, v7, v27
	buffer_store_dword v16, off, s[0:3], s11 offset:60
	buffer_store_dword v8, off, s[0:3], s11 offset:56
	buffer_store_dword v7, off, s[0:3], s11 offset:52
	buffer_store_dword v6, off, s[0:3], s11 offset:48
	v_mul_lo_i32 v17, v6, s13
	v_mul_lo_i32 v18, v7, s13
	v_mul_lo_i32 v24, v8, s13
	s_waitcnt expcnt(0)
	flat_load_dwordx4 v[6:9], v[10:11]
	v_mul_lo_i32 v10, v28, s13
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v8, v8, v30
	v_xor_b32_e32 v25, v9, v29
	v_xor_b32_e32 v6, v6, v10
	v_xor_b32_e32 v7, v7, v31
	buffer_store_dword v25, off, s[0:3], s11 offset:76
	buffer_store_dword v8, off, s[0:3], s11 offset:72
	buffer_store_dword v7, off, s[0:3], s11 offset:68
	buffer_store_dword v6, off, s[0:3], s11 offset:64
	v_mul_lo_i32 v26, v6, s13
	s_waitcnt expcnt(0)
	buffer_load_dword v6, v41, s[0:3], s11 offen
	v_mul_lo_i32 v27, v7, s13
	v_mul_lo_i32 v7, v37, s13
	v_mul_lo_i32 v28, v8, s13
	v_mul_lo_i32 v29, v13, s13
	v_mul_lo_i32 v30, v14, s13
	s_waitcnt vmcnt(0)
	v_xor_b32_e32 v6, v6, v7
	v_mul_hi_u32 v7, v2, v6
	v_mul_lo_i32 v7, v7, s12
	v_cmp_ge_u32_e32 vcc, v6, v7
	v_cndmask_b32_e64 v8, 0, -1, vcc
	v_subrev_u32_e32 v6, vcc, v7, v6
	v_cmp_le_u32_e32 vcc, s12, v6
	v_cndmask_b32_e64 v7, 0, -1, vcc
	v_and_b32_e32 v7, v7, v8
	v_subrev_u32_e32 v9, vcc, s12, v6
	v_add_u32_e32 v10, vcc, s12, v6
	v_cmp_eq_u32_e64 s[4:5], 0, v7
	v_cmp_eq_u32_e32 vcc, 0, v8
	v_cndmask_b32_e64 v6, v9, v6, s[4:5]
	v_cndmask_b32_e32 v11, v6, v10, vcc
	v_lshlrev_b64 v[6:7], 6, v[11:12]
	v_add_u32_e32 v10, vcc, s14, v6
	v_addc_u32_e32 v11, vcc, v38, v7, vcc
	flat_load_dwordx4 v[6:9], v[10:11]
	v_add_u32_e32 v12, vcc, 16, v10
	v_addc_u32_e32 v13, vcc, 0, v11, vcc
	v_add_u32_e32 v14, vcc, 32, v10
	v_addc_u32_e32 v15, vcc, 0, v11, vcc
	v_add_u32_e32 v10, vcc, 48, v10
	v_addc_u32_e32 v11, vcc, 0, v11, vcc
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v57, v6, v4
	v_xor_b32_e32 v71, v9, v19
	v_xor_b32_e32 v70, v8, v20
	v_xor_b32_e32 v54, v7, v21
	buffer_store_dword v71, off, s[0:3], s11 offset:28
	buffer_store_dword v70, off, s[0:3], s11 offset:24
	buffer_store_dword v54, off, s[0:3], s11 offset:20
	buffer_store_dword v57, off, s[0:3], s11 offset:16
	flat_load_dwordx4 v[6:9], v[12:13]
	v_mul_lo_i32 v4, v22, s13
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v47, v9, v4
	v_xor_b32_e32 v46, v8, v23
	v_xor_b32_e32 v64, v7, v30
	v_xor_b32_e32 v65, v6, v29
	buffer_store_dword v47, off, s[0:3], s11 offset:44
	buffer_store_dword v46, off, s[0:3], s11 offset:40
	buffer_store_dword v64, off, s[0:3], s11 offset:36
	buffer_store_dword v65, off, s[0:3], s11 offset:32
	flat_load_dwordx4 v[6:9], v[14:15]
	v_mul_lo_i32 v4, v16, s13
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v68, v9, v4
	v_xor_b32_e32 v69, v8, v24
	v_xor_b32_e32 v62, v7, v18
	v_xor_b32_e32 v63, v6, v17
	buffer_store_dword v68, off, s[0:3], s11 offset:60
	buffer_store_dword v69, off, s[0:3], s11 offset:56
	buffer_store_dword v62, off, s[0:3], s11 offset:52
	buffer_store_dword v63, off, s[0:3], s11 offset:48
	flat_load_dwordx4 v[6:9], v[10:11]
	v_mul_lo_i32 v4, v25, s13
	s_waitcnt vmcnt(0) lgkmcnt(0)
	v_xor_b32_e32 v66, v9, v4
	v_xor_b32_e32 v67, v8, v28
	v_xor_b32_e32 v48, v7, v27
	v_xor_b32_e32 v49, v6, v26
	buffer_store_dword v66, off, s[0:3], s11 offset:76
	buffer_store_dword v67, off, s[0:3], s11 offset:72
	buffer_store_dword v48, off, s[0:3], s11 offset:68
	buffer_store_dword v49, off, s[0:3], s11 offset:64
	s_cbranch_scc0 BB2_5
; BB#6:                                 ; %for.cond.cleanup
	s_mov_b32 s12, 0
	s_mov_b32 s13, s12
	v_mov_b32_e32 v41, s13
	v_mov_b32_e32 v40, s12
	s_brev_b32 s5, 1
	s_mov_b32 s4, 1
	v_mov_b32_e32 v2, s4
	v_mov_b32_e32 v53, v41
	v_mov_b32_e32 v45, v41
	v_mov_b32_e32 v4, v40
	v_mov_b32_e32 v61, v41
	v_mov_b32_e32 v36, v40
	v_mov_b32_e32 v30, v40
	v_mov_b32_e32 v75, v41
	v_mov_b32_e32 v38, v40
	v_mov_b32_e32 v59, v41
	v_mov_b32_e32 v43, v41
	v_mov_b32_e32 v56, v41
	v_mov_b32_e32 v32, v40
	v_mov_b32_e32 v73, v41
	v_mov_b32_e32 v51, v41
	v_mov_b32_e32 v34, v40
	v_mov_b32_e32 v6, 20
	v_mov_b32_e32 v7, 12
	v_mov_b32_e32 v8, 25
	v_mov_b32_e32 v9, 14
	v_mov_b32_e32 v10, 2
	v_mov_b32_e32 v11, 21
	v_mov_b32_e32 v12, 7
	v_mov_b32_e32 v13, 24
	v_mov_b32_e32 v14, 8
	v_mov_b32_e32 v15, 23
	v_mov_b32_e32 v16, 5
	v_mov_b32_e32 v17, 18
	v_mov_b32_e32 v18, 30
	v_mov_b32_e32 v19, 9
	v_mov_b32_e32 v20, 19
	v_mov_b32_e32 v21, 28
	v_mov_b32_e32 v22, 4
	v_mov_b32_e32 v23, 11
	v_mov_b32_e32 v24, 17
	v_mov_b32_e32 v25, 22
	v_mov_b32_e32 v26, 26
	v_mov_b32_e32 v27, 29
	v_mov_b32_e32 v28, 31
	v_mov_b32_e32 v29, 3
	v_mov_b32_e32 v3, s5
	v_mov_b32_e32 v52, v40
	v_mov_b32_e32 v44, v40
	v_mov_b32_e32 v5, v41
	v_mov_b32_e32 v60, v40
	v_mov_b32_e32 v37, v41
	v_mov_b32_e32 v31, v41
	v_mov_b32_e32 v74, v40
	v_mov_b32_e32 v39, v41
	v_mov_b32_e32 v58, v40
	v_mov_b32_e32 v42, v40
	v_mov_b32_e32 v55, v40
	v_mov_b32_e32 v33, v41
	v_mov_b32_e32 v72, v40
	v_mov_b32_e32 v50, v40
	v_mov_b32_e32 v35, v41
BB2_7:                                  ; %for.body18.i
                                        ; =>This Inner Loop Header: Depth=1
	v_cmp_ne_u32_e32 vcc, 0, v1
	s_cbranch_vccnz BB2_9
; BB#8:                                 ; %do.body19.i
                                        ;   in Loop: Header=BB2_7 Depth=1
	v_xor_b32_e32 v81, v39, v75
	v_xor_b32_e32 v83, v38, v74
	v_xor_b32_e32 v77, v56, v43
	v_xor_b32_e32 v78, v55, v42
	v_xor_b32_e32 v81, v81, v59
	v_xor_b32_e32 v83, v83, v58
	v_xor_b32_e32 v84, v52, v40
	v_xor_b32_e32 v87, v53, v41
	v_xor_b32_e32 v88, v37, v61
	v_xor_b32_e32 v89, v36, v60
	v_xor_b32_e32 v76, v51, v73
	v_xor_b32_e32 v77, v77, v33
	v_xor_b32_e32 v78, v78, v32
	v_xor_b32_e32 v81, v81, v48
	v_xor_b32_e32 v82, v50, v72
	v_xor_b32_e32 v83, v83, v49
	v_xor_b32_e32 v84, v84, v44
	v_xor_b32_e32 v87, v87, v45
	v_xor_b32_e32 v88, v88, v31
	v_xor_b32_e32 v89, v89, v30
	v_xor_b32_e32 v76, v76, v35
	v_xor_b32_e32 v78, v78, v67
	v_xor_b32_e32 v77, v77, v66
	v_xor_b32_e32 v82, v82, v34
	v_xor_b32_e32 v84, v84, v4
	v_xor_b32_e32 v87, v87, v5
	v_xor_b32_e32 v89, v89, v2
	v_xor_b32_e32 v88, v88, v3
	v_xor_b32_e32 v81, v81, v71
	v_xor_b32_e32 v83, v83, v70
	v_xor_b32_e32 v76, v76, v68
	v_xor_b32_e32 v82, v82, v69
	v_xor_b32_e32 v77, v77, v64
	v_xor_b32_e32 v78, v78, v65
	v_xor_b32_e32 v84, v84, v63
	;;#ASMSTART
	v_alignbit_b32 v85, v83, v81, v28
v_alignbit_b32 v86, v81, v83, v28
	;;#ASMEND
	v_xor_b32_e32 v87, v87, v62
	v_xor_b32_e32 v88, v88, v47
	v_xor_b32_e32 v89, v89, v46
	;;#ASMSTART
	v_alignbit_b32 v90, v89, v88, v28
v_alignbit_b32 v91, v88, v89, v28
	;;#ASMEND
	v_xor_b32_e32 v85, v85, v84
	v_xor_b32_e32 v86, v86, v87
	v_xor_b32_e32 v76, v76, v54
	;;#ASMSTART
	v_alignbit_b32 v79, v78, v77, v28
v_alignbit_b32 v80, v77, v78, v28
	;;#ASMEND
	v_xor_b32_e32 v82, v82, v57
	;;#ASMSTART
	v_alignbit_b32 v84, v84, v87, v28
v_alignbit_b32 v87, v87, v84, v28
	;;#ASMEND
	v_xor_b32_e32 v80, v80, v76
	v_xor_b32_e32 v79, v79, v82
	;;#ASMSTART
	v_alignbit_b32 v76, v82, v76, v28
v_alignbit_b32 v82, v76, v82, v28
	;;#ASMEND
	v_xor_b32_e32 v83, v90, v83
	v_xor_b32_e32 v81, v91, v81
	v_xor_b32_e32 v77, v87, v77
	v_xor_b32_e32 v78, v84, v78
	v_xor_b32_e32 v82, v82, v88
	v_xor_b32_e32 v76, v76, v89
	v_xor_b32_e32 v88, v85, v50
	v_xor_b32_e32 v89, v86, v51
	v_xor_b32_e32 v71, v80, v71
	v_xor_b32_e32 v70, v79, v70
	s_waitcnt expcnt(0)
	v_xor_b32_e32 v49, v79, v49
	v_xor_b32_e32 v48, v80, v48
	v_xor_b32_e32 v47, v77, v47
	v_xor_b32_e32 v46, v78, v46
	v_xor_b32_e32 v50, v80, v59
	v_xor_b32_e32 v51, v79, v58
	v_xor_b32_e32 v38, v79, v38
	v_xor_b32_e32 v74, v79, v74
	v_xor_b32_e32 v79, v83, v32
	v_xor_b32_e32 v32, v83, v42
	v_xor_b32_e32 v39, v80, v39
	v_xor_b32_e32 v75, v80, v75
	v_xor_b32_e32 v80, v81, v33
	v_xor_b32_e32 v33, v81, v43
	v_xor_b32_e32 v84, v86, v35
	v_xor_b32_e32 v87, v85, v34
	v_xor_b32_e32 v57, v85, v57
	v_xor_b32_e32 v54, v86, v54
	v_xor_b32_e32 v69, v85, v69
	v_xor_b32_e32 v68, v86, v68
	v_xor_b32_e32 v65, v83, v65
	v_xor_b32_e32 v64, v81, v64
	v_xor_b32_e32 v66, v81, v66
	v_xor_b32_e32 v67, v83, v67
	v_xor_b32_e32 v56, v81, v56
	v_xor_b32_e32 v55, v83, v55
	v_xor_b32_e32 v81, v77, v37
	v_xor_b32_e32 v83, v78, v36
	v_xor_b32_e32 v36, v78, v60
	v_xor_b32_e32 v37, v77, v61
	v_xor_b32_e32 v60, v76, v44
	v_xor_b32_e32 v61, v82, v45
	v_xor_b32_e32 v62, v82, v62
	v_xor_b32_e32 v34, v86, v73
	v_xor_b32_e32 v35, v85, v72
	;;#ASMSTART
	v_alignbit_b32 v85, v39, v38, v20
v_alignbit_b32 v86, v38, v39, v20
	;;#ASMEND
	v_xor_b32_e32 v42, v78, v2
	;;#ASMSTART
	v_alignbit_b32 v46, v46, v47, v22
v_alignbit_b32 v47, v47, v46, v22
	;;#ASMEND
	v_xor_b32_e32 v43, v77, v3
	v_xor_b32_e32 v31, v77, v31
	v_xor_b32_e32 v30, v78, v30
	v_xor_b32_e32 v5, v82, v5
	v_xor_b32_e32 v4, v76, v4
	v_xor_b32_e32 v44, v82, v53
	v_xor_b32_e32 v45, v76, v52
	v_xor_b32_e32 v77, v82, v41
	;;#ASMSTART
	v_alignbit_b32 v78, v33, v32, v29
v_alignbit_b32 v82, v32, v33, v29
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v90, v4, v5, v7
v_alignbit_b32 v91, v5, v4, v7
	;;#ASMEND
	v_xor_b32_e32 v63, v76, v63
	v_xor_b32_e32 v76, v76, v40
	v_xor_b32_e32 v2, v47, v86
	v_and_b32_e32 v3, v47, v82
	;;#ASMSTART
	v_alignbit_b32 v38, v35, v34, v9
v_alignbit_b32 v39, v34, v35, v9
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v40, v30, v31, v12
v_alignbit_b32 v41, v31, v30, v12
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v44, v45, v44, v13
v_alignbit_b32 v45, v44, v45, v13
	;;#ASMEND
	v_xor_b32_e32 v3, v2, v3
	v_xor_b32_e32 v2, v46, v85
	v_and_b32_e32 v32, v46, v78
	v_xor_b32_e32 v4, v82, v91
	v_and_b32_e32 v5, v47, v91
	v_xor_b32_e32 v30, v41, v39
	v_and_b32_e32 v31, v45, v39
	v_xor_b32_e32 v33, v30, v31
	v_xor_b32_e32 v2, v2, v32
	v_xor_b32_e32 v5, v4, v5
	;;#ASMSTART
	v_alignbit_b32 v52, v70, v71, v28
v_alignbit_b32 v53, v71, v70, v28
	;;#ASMEND
	v_xor_b32_e32 v4, v78, v90
	v_and_b32_e32 v32, v46, v90
	v_xor_b32_e32 v30, v40, v38
	v_and_b32_e32 v31, v44, v38
	v_xor_b32_e32 v4, v4, v32
	v_xor_b32_e32 v32, v30, v31
	v_xor_b32_e32 v30, v53, v45
	v_and_b32_e32 v31, v53, v39
	;;#ASMSTART
	v_alignbit_b32 v66, v67, v66, v26
v_alignbit_b32 v67, v66, v67, v26
	;;#ASMEND
	v_xor_b32_e32 v31, v30, v31
	v_and_b32_e32 v34, v52, v38
	v_xor_b32_e32 v30, v52, v44
	v_not_b32_e32 v35, v67
	v_xor_b32_e32 v30, v30, v34
	v_not_b32_e32 v34, v66
	v_and_b32_e32 v34, v40, v34
	v_and_b32_e32 v35, v41, v35
	v_not_b32_e32 v40, v40
	v_not_b32_e32 v41, v41
	v_and_b32_e32 v40, v44, v40
	v_and_b32_e32 v41, v45, v41
	v_xor_b32_e32 v59, v67, v41
	v_xor_b32_e32 v58, v66, v40
	v_xor_b32_e32 v38, v66, v38
	v_and_b32_e32 v40, v52, v66
	v_xor_b32_e32 v39, v67, v39
	v_and_b32_e32 v41, v53, v67
	v_xor_b32_e32 v35, v53, v35
	v_xor_b32_e32 v34, v52, v34
	;;#ASMSTART
	v_alignbit_b32 v52, v68, v69, v21
v_alignbit_b32 v53, v69, v68, v21
	;;#ASMEND
	v_xor_b32_e32 v45, v39, v41
	v_xor_b32_e32 v44, v38, v40
	;;#ASMSTART
	v_alignbit_b32 v40, v63, v62, v16
v_alignbit_b32 v41, v62, v63, v16
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v62, v51, v50, v25
v_alignbit_b32 v63, v50, v51, v25
	;;#ASMEND
	v_xor_b32_e32 v38, v63, v41
	v_and_b32_e32 v39, v63, v53
	v_xor_b32_e32 v51, v38, v39
	;;#ASMSTART
	v_alignbit_b32 v66, v55, v56, v24
v_alignbit_b32 v67, v56, v55, v24
	;;#ASMEND
	v_xor_b32_e32 v38, v62, v40
	v_and_b32_e32 v39, v62, v52
	v_xor_b32_e32 v50, v38, v39
	;;#ASMSTART
	v_alignbit_b32 v68, v37, v36, v14
v_alignbit_b32 v69, v36, v37, v14
	;;#ASMEND
	v_not_b32_e32 v36, v66
	v_not_b32_e32 v37, v67
	v_xor_b32_e32 v38, v67, v53
	v_and_b32_e32 v39, v63, v67
	v_xor_b32_e32 v39, v38, v39
	v_and_b32_e32 v37, v69, v37
	v_xor_b32_e32 v38, v66, v52
	v_and_b32_e32 v55, v62, v66
	v_and_b32_e32 v36, v68, v36
	v_xor_b32_e32 v38, v38, v55
	v_xor_b32_e32 v55, v36, v62
	v_not_b32_e32 v36, v68
	v_xor_b32_e32 v56, v37, v63
	v_not_b32_e32 v37, v69
	v_and_b32_e32 v36, v40, v36
	v_and_b32_e32 v37, v41, v37
	v_xor_b32_e32 v62, v52, v68
	v_and_b32_e32 v40, v52, v40
	v_xor_b32_e32 v52, v53, v69
	v_and_b32_e32 v41, v53, v41
	v_xor_b32_e32 v53, v52, v41
	v_xor_b32_e32 v52, v62, v40
	;;#ASMSTART
	v_alignbit_b32 v62, v64, v65, v10
v_alignbit_b32 v63, v65, v64, v10
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v40, v61, v60, v8
v_alignbit_b32 v41, v60, v61, v8
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v64, v43, v42, v19
v_alignbit_b32 v65, v42, v43, v19
	;;#ASMEND
	v_xor_b32_e32 v42, v63, v41
	v_and_b32_e32 v43, v65, v41
	v_xor_b32_e32 v73, v42, v43
	;;#ASMSTART
	v_alignbit_b32 v68, v74, v75, v18
v_alignbit_b32 v69, v75, v74, v18
	;;#ASMEND
	v_xor_b32_e32 v37, v67, v37
	v_xor_b32_e32 v36, v66, v36
	;;#ASMSTART
	v_alignbit_b32 v66, v89, v88, v15
v_alignbit_b32 v67, v88, v89, v15
	;;#ASMEND
	v_xor_b32_e32 v42, v62, v40
	v_and_b32_e32 v43, v64, v40
	v_xor_b32_e32 v72, v42, v43
	v_xor_b32_e32 v42, v69, v41
	v_and_b32_e32 v43, v69, v67
	v_not_b32_e32 v41, v41
	v_xor_b32_e32 v43, v42, v43
	v_xor_b32_e32 v42, v68, v40
	v_not_b32_e32 v40, v40
	v_and_b32_e32 v60, v68, v66
	v_xor_b32_e32 v42, v42, v60
	v_and_b32_e32 v40, v66, v40
	v_and_b32_e32 v41, v67, v41
	v_xor_b32_e32 v60, v67, v63
	v_and_b32_e32 v61, v69, v63
	v_xor_b32_e32 v61, v60, v61
	v_xor_b32_e32 v75, v65, v41
	v_xor_b32_e32 v74, v64, v40
	v_xor_b32_e32 v60, v66, v62
	v_and_b32_e32 v70, v68, v62
	v_xor_b32_e32 v40, v64, v68
	v_and_b32_e32 v62, v64, v62
	v_xor_b32_e32 v41, v65, v69
	v_and_b32_e32 v63, v65, v63
	s_ashr_i32 s13, s12, 31
	v_xor_b32_e32 v41, v41, v63
	v_xor_b32_e32 v40, v40, v62
	;;#ASMSTART
	v_alignbit_b32 v62, v80, v79, v11
v_alignbit_b32 v63, v79, v80, v11
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v48, v48, v49, v6
v_alignbit_b32 v49, v49, v48, v6
	;;#ASMEND
	s_add_i32 s10, s12, 1
	s_lshl_b64 s[4:5], s[12:13], 3
	v_xor_b32_e32 v64, v62, v57
	v_and_b32_e32 v65, v62, v48
	s_add_u32 s4, s4, s6
	v_xor_b32_e32 v79, v64, v65
	v_xor_b32_e32 v64, v63, v54
	v_and_b32_e32 v65, v63, v49
	s_addc_u32 s5, s5, s7
	v_xor_b32_e32 v80, v64, v65
	v_not_b32_e32 v64, v85
	;;#ASMSTART
	v_alignbit_b32 v68, v76, v77, v17
v_alignbit_b32 v69, v77, v76, v17
	;;#ASMEND
	v_and_b32_e32 v67, v78, v64
	v_not_b32_e32 v64, v86
	s_load_dwordx2 s[4:5], s[4:5], 0x0
	v_and_b32_e32 v66, v82, v64
	v_not_b32_e32 v64, v68
	v_and_b32_e32 v82, v57, v64
	v_not_b32_e32 v64, v69
	;;#ASMSTART
	v_alignbit_b32 v76, v83, v81, v23
v_alignbit_b32 v77, v81, v83, v23
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v78, v87, v84, v27
v_alignbit_b32 v81, v84, v87, v27
	;;#ASMEND
	v_not_b32_e32 v57, v57
	v_and_b32_e32 v83, v54, v64
	v_not_b32_e32 v54, v54
	v_and_b32_e32 v57, v48, v57
	v_and_b32_e32 v54, v49, v54
	v_and_b32_e32 v64, v76, v62
	v_and_b32_e32 v65, v77, v63
	v_xor_b32_e32 v49, v77, v49
	v_xor_b32_e32 v84, v78, v46
	v_xor_b32_e32 v87, v81, v47
	v_xor_b32_e32 v89, v85, v90
	v_and_b32_e32 v88, v78, v90
	v_and_b32_e32 v90, v81, v91
	v_xor_b32_e32 v91, v86, v91
	v_xor_b32_e32 v48, v76, v48
	v_xor_b32_e32 v62, v68, v62
	v_xor_b32_e32 v63, v69, v63
	v_and_b32_e32 v85, v78, v85
	v_and_b32_e32 v86, v81, v86
	v_and_b32_e32 v46, v76, v68
	v_and_b32_e32 v47, v77, v69
	v_xor_b32_e32 v71, v49, v65
	v_xor_b32_e32 v65, v62, v46
	v_xor_b32_e32 v62, v69, v54
	v_xor_b32_e32 v60, v60, v70
	v_xor_b32_e32 v70, v48, v64
	v_xor_b32_e32 v64, v63, v47
	v_xor_b32_e32 v63, v68, v57
	s_mov_b32 s12, s10
	v_xor_b32_e32 v47, v77, v83
	v_xor_b32_e32 v46, v76, v82
	v_xor_b32_e32 v68, v87, v90
	v_xor_b32_e32 v69, v84, v88
	v_xor_b32_e32 v48, v91, v86
	v_xor_b32_e32 v49, v89, v85
	v_xor_b32_e32 v66, v81, v66
	v_xor_b32_e32 v67, v78, v67
	s_waitcnt lgkmcnt(0)
	v_xor_b32_e32 v54, s5, v80
	v_xor_b32_e32 v57, s4, v79
BB2_9:                                  ; %if.end.i
                                        ;   in Loop: Header=BB2_7 Depth=1
	s_cmp_lt_i32 s12, 23
	s_cbranch_scc1 BB2_7
; BB#10:                                ; %SHA3_512.exit
	v_xor_b32_e32 v9, v71, v48
	v_xor_b32_e32 v10, v70, v49
	v_xor_b32_e32 v11, v40, v63
	v_xor_b32_e32 v12, v41, v62
	v_xor_b32_e32 v10, v10, v74
	v_xor_b32_e32 v9, v9, v75
	v_xor_b32_e32 v1, v65, v67
	v_xor_b32_e32 v6, v64, v66
	v_xor_b32_e32 v9, v9, v39
	v_xor_b32_e32 v10, v10, v38
	v_xor_b32_e32 v12, v12, v53
	v_xor_b32_e32 v11, v11, v52
	v_xor_b32_e32 v6, v6, v43
	v_xor_b32_e32 v1, v1, v42
	v_xor_b32_e32 v12, v12, v45
	v_xor_b32_e32 v11, v11, v44
	v_xor_b32_e32 v10, v10, v58
	v_xor_b32_e32 v9, v9, v59
	v_mov_b32_e32 v13, 31
	v_xor_b32_e32 v7, v54, v68
	v_xor_b32_e32 v8, v57, v69
	v_xor_b32_e32 v1, v1, v55
	v_xor_b32_e32 v6, v6, v56
	v_xor_b32_e32 v12, v12, v5
	;;#ASMSTART
	v_alignbit_b32 v14, v10, v9, v13
v_alignbit_b32 v15, v9, v10, v13
	;;#ASMEND
	v_xor_b32_e32 v11, v11, v4
	v_xor_b32_e32 v8, v8, v72
	v_xor_b32_e32 v7, v7, v73
	v_xor_b32_e32 v15, v15, v12
	v_xor_b32_e32 v14, v14, v11
	v_xor_b32_e32 v6, v6, v33
	v_xor_b32_e32 v1, v1, v32
	;;#ASMSTART
	v_alignbit_b32 v11, v11, v12, v13
v_alignbit_b32 v12, v12, v11, v13
	;;#ASMEND
	v_xor_b32_e32 v8, v8, v50
	v_xor_b32_e32 v7, v7, v51
	;;#ASMSTART
	v_alignbit_b32 v16, v1, v6, v13
v_alignbit_b32 v17, v6, v1, v13
	;;#ASMEND
	v_xor_b32_e32 v11, v11, v1
	v_mov_b32_e32 v1, 0
	v_xor_b32_e32 v8, v8, v34
	v_xor_b32_e32 v7, v7, v35
	v_lshlrev_b64 v[0:1], 6, v[0:1]
	v_xor_b32_e32 v20, v16, v8
	v_xor_b32_e32 v22, v17, v7
	v_xor_b32_e32 v6, v12, v6
	v_xor_b32_e32 v12, v61, v47
	v_add_u32_e32 v16, vcc, s8, v0
	v_mov_b32_e32 v17, s9
	v_xor_b32_e32 v25, v60, v46
	v_xor_b32_e32 v0, v12, v37
	v_addc_u32_e32 v17, vcc, v17, v1, vcc
	v_xor_b32_e32 v1, v25, v36
	v_xor_b32_e32 v1, v1, v30
	v_xor_b32_e32 v0, v0, v31
	v_xor_b32_e32 v0, v0, v3
	v_xor_b32_e32 v1, v1, v2
	;;#ASMSTART
	v_alignbit_b32 v2, v1, v0, v13
v_alignbit_b32 v3, v0, v1, v13
	;;#ASMEND
	v_xor_b32_e32 v2, v2, v10
	v_xor_b32_e32 v3, v3, v9
	v_xor_b32_e32 v18, v15, v54
	v_xor_b32_e32 v12, v15, v35
	v_xor_b32_e32 v21, v20, v49
	v_xor_b32_e32 v15, v20, v38
	v_xor_b32_e32 v23, v22, v48
	v_xor_b32_e32 v20, v22, v39
	v_xor_b32_e32 v24, v6, v47
	v_xor_b32_e32 v9, v2, v32
	v_xor_b32_e32 v22, v2, v42
	v_xor_b32_e32 v2, v6, v37
	;;#ASMSTART
	v_alignbit_b32 v6, v8, v7, v13
v_alignbit_b32 v7, v7, v8, v13
	;;#ASMEND
	v_xor_b32_e32 v1, v6, v1
	v_xor_b32_e32 v13, v1, v4
	v_mov_b32_e32 v4, 20
	;;#ASMSTART
	v_alignbit_b32 v21, v23, v21, v4
v_alignbit_b32 v23, v21, v23, v4
	;;#ASMEND
	v_xor_b32_e32 v10, v3, v33
	v_mov_b32_e32 v4, 21
	;;#ASMSTART
	v_alignbit_b32 v27, v10, v9, v4
v_alignbit_b32 v28, v9, v10, v4
	;;#ASMEND
	v_xor_b32_e32 v25, v3, v43
	v_xor_b32_e32 v3, v11, v36
	v_mov_b32_e32 v4, 11
	v_xor_b32_e32 v0, v7, v0
	;;#ASMSTART
	v_alignbit_b32 v6, v3, v2, v4
v_alignbit_b32 v7, v2, v3, v4
	;;#ASMEND
	v_xor_b32_e32 v3, v7, v23
	v_and_b32_e32 v4, v7, v28
	v_xor_b32_e32 v26, v11, v46
	v_xor_b32_e32 v11, v0, v5
	v_xor_b32_e32 v3, v3, v4
	v_xor_b32_e32 v0, v0, v41
	v_xor_b32_e32 v1, v1, v40
	v_mov_b32_e32 v4, 18
	;;#ASMSTART
	v_alignbit_b32 v0, v1, v0, v4
v_alignbit_b32 v1, v0, v1, v4
	;;#ASMEND
	v_xor_b32_e32 v2, v6, v21
	v_and_b32_e32 v5, v6, v27
	v_xor_b32_e32 v2, v2, v5
	v_xor_b32_e32 v5, v1, v28
	v_and_b32_e32 v8, v7, v1
	v_xor_b32_e32 v19, v14, v57
	v_xor_b32_e32 v5, v5, v8
	v_not_b32_e32 v8, v0
	v_and_b32_e32 v8, v19, v8
	v_and_b32_e32 v9, v6, v0
	v_xor_b32_e32 v6, v6, v8
	v_not_b32_e32 v8, v1
	v_and_b32_e32 v8, v18, v8
	v_xor_b32_e32 v4, v0, v27
	v_xor_b32_e32 v7, v7, v8
	v_not_b32_e32 v8, v18
	v_xor_b32_e32 v4, v4, v9
	v_and_b32_e32 v9, v23, v8
	v_not_b32_e32 v8, v19
	v_and_b32_e32 v8, v21, v8
	v_xor_b32_e32 v8, v0, v8
	v_mov_b32_e32 v0, 12
	v_xor_b32_e32 v9, v1, v9
	;;#ASMSTART
	v_alignbit_b32 v0, v13, v11, v0
v_alignbit_b32 v1, v11, v13, v0
	;;#ASMEND
	v_mov_b32_e32 v10, 4
	v_xor_b32_e32 v14, v14, v34
	v_mov_b32_e32 v13, 29
	;;#ASMSTART
	v_alignbit_b32 v10, v26, v24, v10
v_alignbit_b32 v11, v24, v26, v10
	;;#ASMEND
	;;#ASMSTART
	v_alignbit_b32 v14, v14, v12, v13
v_alignbit_b32 v24, v12, v14, v13
	;;#ASMEND
	v_xor_b32_e32 v10, v14, v10
	v_and_b32_e32 v12, v14, v0
	v_xor_b32_e32 v10, v10, v12
	v_xor_b32_e32 v11, v24, v11
	v_and_b32_e32 v12, v24, v1
	v_xor_b32_e32 v11, v11, v12
	v_mov_b32_e32 v12, 19
	;;#ASMSTART
	v_alignbit_b32 v15, v20, v15, v12
v_alignbit_b32 v20, v15, v20, v12
	;;#ASMEND
	v_xor_b32_e32 v0, v15, v0
	v_and_b32_e32 v12, v14, v15
	v_xor_b32_e32 v12, v0, v12
	v_mov_b32_e32 v0, 3
	v_xor_b32_e32 v1, v20, v1
	v_and_b32_e32 v13, v24, v20
	v_xor_b32_e32 v13, v1, v13
	v_not_b32_e32 v1, v15
	;;#ASMSTART
	v_alignbit_b32 v0, v25, v22, v0
v_alignbit_b32 v22, v22, v25, v0
	;;#ASMEND
	v_and_b32_e32 v0, v0, v1
	v_and_b32_e32 v15, v28, v23
	v_xor_b32_e32 v18, v18, v28
	v_xor_b32_e32 v14, v14, v0
	v_xor_b32_e32 v15, v18, v15
	v_and_b32_e32 v0, v27, v21
	v_xor_b32_e32 v1, v19, v27
	v_add_u32_e32 v18, vcc, 48, v16
	v_xor_b32_e32 v0, v1, v0
	v_xor_b32_e32 v1, 0x80000000, v15
	v_not_b32_e32 v15, v20
	v_addc_u32_e32 v19, vcc, 0, v17, vcc
	v_and_b32_e32 v15, v22, v15
	v_add_u32_e32 v20, vcc, 32, v16
	v_xor_b32_e32 v0, 0x80008008, v0
	v_xor_b32_e32 v15, v24, v15
	v_addc_u32_e32 v21, vcc, 0, v17, vcc
	buffer_store_dword v1, off, s[0:3], s11 offset:20
	buffer_store_dword v0, off, s[0:3], s11 offset:16
	buffer_store_dword v3, off, s[0:3], s11 offset:28
	buffer_store_dword v2, off, s[0:3], s11 offset:24
	buffer_store_dword v5, off, s[0:3], s11 offset:36
	buffer_store_dword v4, off, s[0:3], s11 offset:32
	buffer_store_dword v7, off, s[0:3], s11 offset:44
	buffer_store_dword v6, off, s[0:3], s11 offset:40
	buffer_store_dword v9, off, s[0:3], s11 offset:52
	buffer_store_dword v8, off, s[0:3], s11 offset:48
	buffer_store_dword v11, off, s[0:3], s11 offset:60
	buffer_store_dword v10, off, s[0:3], s11 offset:56
	buffer_store_dword v13, off, s[0:3], s11 offset:68
	buffer_store_dword v12, off, s[0:3], s11 offset:64
	buffer_store_dword v15, off, s[0:3], s11 offset:76
	buffer_store_dword v14, off, s[0:3], s11 offset:72
	flat_store_dwordx4 v[18:19], v[12:15]
	flat_store_dwordx4 v[20:21], v[8:11]
	s_nop 0
	s_waitcnt expcnt(6)
	v_add_u32_e32 v8, vcc, 16, v16
	v_addc_u32_e32 v9, vcc, 0, v17, vcc
	flat_store_dwordx4 v[16:17], v[0:3]
	flat_store_dwordx4 v[8:9], v[4:7]
	s_endpgm
.Lfunc_end2:
	.size	GenerateDAG, .Lfunc_end2-GenerateDAG
                                        ; -- End function
	.section	.AMDGPU.csdata
; Kernel info:
; codeLenInByte = 8416
; NumSgprs: 26
; NumVgprs: 95
; ScratchSize: 80
; FloatMode: 192
; IeeeMode: 1
; LDSByteSize: 0 bytes/workgroup (compile time only)
; SGPRBlocks: 3
; VGPRBlocks: 23
; NumSGPRsForWavesPerEU: 26
; NumVGPRsForWavesPerEU: 95
; ReservedVGPRFirst: 0
; ReservedVGPRCount: 0
; COMPUTE_PGM_RSRC2:USER_SGPR: 10
; COMPUTE_PGM_RSRC2:TRAP_HANDLER: 1
; COMPUTE_PGM_RSRC2:TGID_X_EN: 1
; COMPUTE_PGM_RSRC2:TGID_Y_EN: 0
; COMPUTE_PGM_RSRC2:TGID_Z_EN: 0
; COMPUTE_PGM_RSRC2:TIDIG_COMP_CNT: 0
	.type	Keccak_f1600_RC,@object ; @Keccak_f1600_RC
	.section	.rodata,#alloc
	.p2align	3
Keccak_f1600_RC:
	.long	1                       ; 0x1
	.long	0                       ; 0x0
	.long	32898                   ; 0x8082
	.long	0                       ; 0x0
	.long	32906                   ; 0x808a
	.long	2147483648              ; 0x80000000
	.long	2147516416              ; 0x80008000
	.long	2147483648              ; 0x80000000
	.long	32907                   ; 0x808b
	.long	0                       ; 0x0
	.long	2147483649              ; 0x80000001
	.long	0                       ; 0x0
	.long	2147516545              ; 0x80008081
	.long	2147483648              ; 0x80000000
	.long	32777                   ; 0x8009
	.long	2147483648              ; 0x80000000
	.long	138                     ; 0x8a
	.long	0                       ; 0x0
	.long	136                     ; 0x88
	.long	0                       ; 0x0
	.long	2147516425              ; 0x80008009
	.long	0                       ; 0x0
	.long	2147483658              ; 0x8000000a
	.long	0                       ; 0x0
	.long	2147516555              ; 0x8000808b
	.long	0                       ; 0x0
	.long	139                     ; 0x8b
	.long	2147483648              ; 0x80000000
	.long	32905                   ; 0x8089
	.long	2147483648              ; 0x80000000
	.long	32771                   ; 0x8003
	.long	2147483648              ; 0x80000000
	.long	32770                   ; 0x8002
	.long	2147483648              ; 0x80000000
	.long	128                     ; 0x80
	.long	2147483648              ; 0x80000000
	.long	32778                   ; 0x800a
	.long	0                       ; 0x0
	.long	2147483658              ; 0x8000000a
	.long	2147483648              ; 0x80000000
	.long	2147516545              ; 0x80008081
	.long	2147483648              ; 0x80000000
	.long	32896                   ; 0x8080
	.long	2147483648              ; 0x80000000
	.long	2147483649              ; 0x80000001
	.long	0                       ; 0x0
	.long	2147516424              ; 0x80008008
	.long	2147483648              ; 0x80000000
	.size	Keccak_f1600_RC, 192


	.ident	"clang version 6.0.0 (https://github.com/llvm-mirror/clang 6a217e4c25e66055338c0c58d33d8a7e793f8b28) (https://github.com/llvm-mirror/llvm.git 18d898c462a6128a93f511fa696f517600c1783b)"
	.section	".note.GNU-stack"
	.amd_amdgpu_isa "amdgcn-amd-amdhsa-amdgizcl-gfx803"
	.amd_amdgpu_hsa_metadata
---
Version:         [ 1, 0 ]
Kernels:         
  - Name:            search
    SymbolName:      'search@kd'
    Language:        OpenCL C
    LanguageVersion: [ 2, 0 ]
    Attrs:           
      ReqdWorkGroupSize: [ 256, 1, 1 ]
    Args:            
      - TypeName:        'uint*'
        Size:            8
        Align:           8
        ValueKind:       GlobalBuffer
        ValueType:       U32
        AddrSpaceQual:   Global
        AccQual:         Default
        IsRestrict:      true
        IsVolatile:      true
      - TypeName:        'uint2*'
        Size:            8
        Align:           8
        ValueKind:       GlobalBuffer
        ValueType:       U32
        AddrSpaceQual:   Constant
        AccQual:         Default
        IsConst:         true
      - TypeName:        'ulong8*'
        Size:            8
        Align:           8
        ValueKind:       GlobalBuffer
        ValueType:       U64
        AddrSpaceQual:   Global
        AccQual:         Default
        IsConst:         true
      - TypeName:        uint
        Size:            4
        Align:           4
        ValueKind:       ByValue
        ValueType:       U32
        AccQual:         Default
      - TypeName:        ulong
        Size:            8
        Align:           8
        ValueKind:       ByValue
        ValueType:       U64
        AccQual:         Default
      - TypeName:        ulong
        Size:            8
        Align:           8
        ValueKind:       ByValue
        ValueType:       U64
        AccQual:         Default
      - TypeName:        uint
        Size:            4
        Align:           4
        ValueKind:       ByValue
        ValueType:       U32
        AccQual:         Default
      - Size:            8
        Align:           8
        ValueKind:       HiddenGlobalOffsetX
        ValueType:       I64
      - Size:            8
        Align:           8
        ValueKind:       HiddenGlobalOffsetY
        ValueType:       I64
      - Size:            8
        Align:           8
        ValueKind:       HiddenGlobalOffsetZ
        ValueType:       I64
    CodeProps:       
      KernargSegmentSize: 88
      GroupSegmentFixedSize: 5120
      PrivateSegmentFixedSize: 0
      KernargSegmentAlign: 8
      WavefrontSize:   64
      NumSGPRs:        24
      NumVGPRs:        74
      MaxFlatWorkGroupSize: 256
  - Name:            GenerateDAG
    SymbolName:      'GenerateDAG@kd'
    Language:        OpenCL C
    LanguageVersion: [ 2, 0 ]
    Args:            
      - TypeName:        uint
        Size:            4
        Align:           4
        ValueKind:       ByValue
        ValueType:       U32
        AccQual:         Default
      - TypeName:        'uint16*'
        Size:            8
        Align:           8
        ValueKind:       GlobalBuffer
        ValueType:       U32
        AddrSpaceQual:   Global
        AccQual:         Default
        IsConst:         true
      - TypeName:        'uint16*'
        Size:            8
        Align:           8
        ValueKind:       GlobalBuffer
        ValueType:       U32
        AddrSpaceQual:   Global
        AccQual:         Default
      - TypeName:        uint
        Size:            4
        Align:           4
        ValueKind:       ByValue
        ValueType:       U32
        AccQual:         Default
      - TypeName:        uint
        Size:            4
        Align:           4
        ValueKind:       ByValue
        ValueType:       U32
        AccQual:         Default
      - Size:            8
        Align:           8
        ValueKind:       HiddenGlobalOffsetX
        ValueType:       I64
      - Size:            8
        Align:           8
        ValueKind:       HiddenGlobalOffsetY
        ValueType:       I64
      - Size:            8
        Align:           8
        ValueKind:       HiddenGlobalOffsetZ
        ValueType:       I64
    CodeProps:       
      KernargSegmentSize: 64
      GroupSegmentFixedSize: 0
      PrivateSegmentFixedSize: 80
      KernargSegmentAlign: 8
      WavefrontSize:   64
      NumSGPRs:        26
      NumVGPRs:        95
      MaxFlatWorkGroupSize: 256
...

	.end_amd_amdgpu_hsa_metadata
