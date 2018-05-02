// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the CLRXWRAPPER_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// CLRXWRAPPER_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef CLRXWRAPPER_EXPORTS
#define CLRXWRAPPER_API __declspec(dllexport)
#else
#define CLRXWRAPPER_API __declspec(dllimport)
#endif

extern "C" CLRXWRAPPER_API int CLRX_BuildBinary(void *_context, int deviceIndex, char *source, bool legacy);
extern "C" CLRXWRAPPER_API void *CLRX_Alloc();
extern "C" CLRXWRAPPER_API void CLRX_Free(void *_context);
extern "C" CLRXWRAPPER_API int CLRX_GetError(void *_context);
extern "C" CLRXWRAPPER_API char *CLRX_GetErrorMessage(void *_context);
