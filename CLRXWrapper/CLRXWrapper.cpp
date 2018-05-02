// CLRXWrapper.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#define CLRXWRAPPER_EXPORTS
#include "CLRXWrapper.h"

#include <algorithm>
#include <iostream>
#include <fstream>
#include <vector>
#include <cstring>
#include <memory>
#include <CL/cl.h>
#include <Objbase.h>
#undef VOID
#undef min
#include <CLRX/amdasm/Assembler.h>
#include <CLRX/clhelper/CLHelper.h>

using namespace CLRX;



CLError::CLError() : error(0)
{ }

CLError::CLError(const char* _description) : Exception(_description), error(0)
{ }

CLError::CLError(cl_int _error, const char* _description) : error(_error)
{
    // construct message
    char buf[20];
    ::snprintf(buf, 20, "%d", _error);
    message = "CLError code: ";
    message += buf;
    message += ", Desc: ";
    message += _description;
}

CLError::~CLError() noexcept
{ }

const char* CLError::what() const noexcept
{
    // print no error if no error
    return (!message.empty()) ? message.c_str() : "No error!";
}

// strip CString (remove spaces from first and last characters
static char* stripCString(char* str)
{
    while (*str == ' ') str++;
    char* last = str + ::strlen(str);
    while (last != str && (*last == 0 || *last == ' '))
        last--;
    if (*last != 0) last[1] = 0;
    return str;
}



struct CLRXWrapperContext {
    int deviceIndex;
    cl_platform_id platform;
    cl_device_id deviceID;
    Array<cxbyte> binary;
    CLAsmSetup setup;
    cl_int error;
    const char *errorMessage;
};

extern "C" {

    CLRXWrapperContext *__context = nullptr;

    CLRXWRAPPER_API void *CLRX_Alloc()
    {
        return (__context = new CLRXWrapperContext());
    }

    CLRXWRAPPER_API void CLRX_Free(void *_context)
    {
        delete (CLRXWrapperContext *)_context;
    }

    CLRXWRAPPER_API int CLRX_BuildBinary(void *_context, int deviceIndex, char *source, bool legacy)
    {
        CLRXWrapperContext *context = (CLRXWrapperContext *)__context;

        cl_int error = CL_SUCCESS;
        cl_uint platformsNum;
        std::unique_ptr<cl_platform_id[]> platforms;
        error = clGetPlatformIDs(0, nullptr, &platformsNum);
        if (error != CL_SUCCESS) {
            context->error = error;
            context->errorMessage = "clGetPlatformIDs";
            return context->error;
        }
        platforms.reset(new cl_platform_id[platformsNum]);
        error = clGetPlatformIDs(platformsNum, platforms.get(), nullptr);
        if (error != CL_SUCCESS) {
            context->error = error;
            context->errorMessage = "clGetPlatformIDs";
            return context->error;
        }

        cxuint devPlatformStart = 0;
        cl_uint i;
        for (i = 0; i < platformsNum; i++) {
            size_t platformNameSize;
            std::unique_ptr<char[]> platformName;
            error = clGetPlatformInfo(platforms[i], CL_PLATFORM_NAME, 0, nullptr,
                                      &platformNameSize);
            if (error != CL_SUCCESS) {
                context->error = error;
                context->errorMessage = "clGetPlatformInfo";
                return context->error;
            }
            platformName.reset(new char[platformNameSize]);
            error = clGetPlatformInfo(platforms[i], CL_PLATFORM_NAME, platformNameSize,
                                      platformName.get(), nullptr);
            if (error != CL_SUCCESS) {
                context->error = error;
                context->errorMessage = "clGetPlatformInfo";
                return context->error;
            }

            cl_uint devicesNum;
            std::unique_ptr<cl_device_id[]> devices;
            error = clGetDeviceIDs(platforms[i], CL_DEVICE_TYPE_GPU, 0, nullptr, &devicesNum);
            if (error != CL_SUCCESS) {
                context->error = devicesNum; // error;
                context->errorMessage = "clGetDeviceIDs";
                return context->error;
            }

            devices.reset(new cl_device_id[devicesNum]);
            error = clGetDeviceIDs(platforms[i], CL_DEVICE_TYPE_GPU, devicesNum, devices.get(), nullptr);
            if (error != CL_SUCCESS) {
                context->error = error;
                context->errorMessage = "clGetDeviceIDs";
                return context->error;
            }

            if (deviceIndex - devPlatformStart < devicesNum) {
                context->platform = platforms[i];
                context->deviceID = devices[deviceIndex - devPlatformStart];
                break;
            }
            devPlatformStart += devicesNum;
        }
        if (i >= platformsNum) {
            context->error = CL_DEVICE_NOT_FOUND;
            context->errorMessage = "PlatformNotFound";
            return context->error;
        }

        try {
            const size_t sourceLen = strlen(source);
            ArrayIStream astream(sourceLen, source);
            Assembler assembler("", astream, 0U, (legacy ? BinaryFormat::AMD : BinaryFormat::AMDCL2), GPUDeviceType::ELLESMERE, std::cerr, std::cout);
            assembler.set64Bit(1);
            ///assembler.setLLVMVersion(asmSetup.llvmVersion);
            //assembler.setDriverVersion(asmSetup.driverVersion);
            //assembler.setNewROCmBinFormat(asmSetup.newROCmBinFormat);
            //for (const auto& symbol : defSymbols)
            //    assembler.addInitialDefSym(symbol.first, symbol.second);

            assembler.assemble();
            // write binary
            assembler.writeBinary("d:\\hoge.bin");
            Array<cxbyte> binary;
            //assembler.writeBinary(binary);

        } catch (std::exception& ex) {
            context->error = CL_BUILD_PROGRAM_FAILURE;
            context->errorMessage = ex.what();
            return context->error;
        }

        context->error = CL_SUCCESS;
        context->errorMessage = "Success!";
        return context->error;
    }

    CLRXWRAPPER_API int CLRX_GetError(void *context)
    {
        return ((CLRXWrapperContext *)context)->error;
    }


    CLRXWRAPPER_API char *CLRX_GetErrorMessage(void *context)
    {
        const char *s = ((CLRXWrapperContext *)context)->errorMessage;
        char *ss = (char*)::CoTaskMemAlloc(strlen(s) + 1);
        strcpy(ss, s);
        return ss;
    }
}