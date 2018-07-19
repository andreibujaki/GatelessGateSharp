// Copyright 2017 Yurio Miyazawa (a.k.a zawawa) <me@yurio.net>
//
// This file is part of Gateless Gate Sharp.
//
// Gateless Gate Sharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Gateless Gate Sharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Gateless Gate Sharp.  If not, see <http://www.gnu.org/licenses/>.



#define _CRT_SECURE_NO_WARNINGS

#include <windows.h>
#include <VersionHelpers.h>
#include <winioctl.h>
#include <process.h>  
#include <inttypes.h>
#include <chrono>
#include <thread>
#include <mutex>



#define MAX_BIOS_SIZE (512 * 1024)
#pragma pack(push,1)
#include "atom.h"
#include "atombios.h"
//#pragma pack(pop)
std::mutex ATOMBIOSMutex;



extern "C" {
#include "PhyMem.h"
#include "PhyMemWrapper.h"

    CRITICAL_SECTION phymemMutex;

    BOOL InstallDriver(PCSTR driverPath, PCSTR driverName);
    BOOL RemoveDriver(PCSTR driverName);
    BOOL StartDriver(PCSTR driverName);
    BOOL StopDriver(PCSTR driverName);



    BOOL InstallDriver(PCSTR driverPath, PCSTR driverName)
    {
        SC_HANDLE hSCManager;
        SC_HANDLE hService;

        //Remove any previous instance of the driver
        RemoveDriver(driverName);

        hSCManager = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);

        if (hSCManager) {
            //Install the driver
            hService = CreateServiceA(hSCManager,
                                      driverName,
                                      driverName,
                                      SERVICE_ALL_ACCESS,
                                      SERVICE_KERNEL_DRIVER,
                                      SERVICE_DEMAND_START,
                                      SERVICE_ERROR_NORMAL,
                                      driverPath,
                                      NULL,
                                      NULL,
                                      NULL,
                                      NULL,
                                      NULL);

            CloseServiceHandle(hSCManager);

            if (hService == NULL)
                return FALSE;
        } else
            return FALSE;

        CloseServiceHandle(hService);

        return TRUE;
    }

    BOOL RemoveDriver(PCSTR driverName)
    {
        SC_HANDLE hSCManager;
        SC_HANDLE hService;
        BOOL bResult;

        StopDriver(driverName);

        hSCManager = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);

        if (hSCManager) {
            hService = OpenServiceA(hSCManager, driverName, SERVICE_ALL_ACCESS);

            if (hService) {
                SERVICE_STATUS status;
                ControlService(hService, SERVICE_CONTROL_STOP, &status);
                bResult = DeleteService(hService);

                CloseServiceHandle(hSCManager);
                CloseServiceHandle(hService);
            } else {
                CloseServiceHandle(hSCManager);
                return FALSE;
            }
        } else
            return FALSE;

        return bResult;
    }

    BOOL StartDriver(PCSTR driverName)
    {
        SC_HANDLE hSCManager;
        SC_HANDLE hService;
        BOOL bResult;

        hSCManager = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);

        if (hSCManager) {
            hService = OpenServiceA(hSCManager, driverName, SERVICE_ALL_ACCESS);

            CloseServiceHandle(hSCManager);

            if (hService) {
                bResult = StartService(hService, 0, NULL);
                if (bResult == FALSE) {
                    if (GetLastError() == ERROR_SERVICE_ALREADY_RUNNING)
                        bResult = TRUE;
                }

                CloseServiceHandle(hService);
            } else
                return FALSE;
        } else
            return FALSE;

        return bResult;
    }

    BOOL StopDriver(PCSTR driverName)
    {
        SC_HANDLE hSCManager;
        SC_HANDLE hService;
        SERVICE_STATUS ServiceStatus;
        BOOL bResult;

        hSCManager = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);

        if (hSCManager) {
            hService = OpenServiceA(hSCManager, driverName, SERVICE_ALL_ACCESS);

            CloseServiceHandle(hSCManager);

            if (hService) {
                bResult = ControlService(hService, SERVICE_CONTROL_STOP, &ServiceStatus);

                CloseServiceHandle(hService);
            } else
                return FALSE;
        } else
            return FALSE;

        return bResult;
    }



#define RSDT_PTR_MAGIC_STRING "RSD PTR "
#define RSDT_PTR_MAGIC_STRING_RANGE_BASE 1024
#define RSDT_PTR_MAGIC_STRING_RANGE_SIZE (0x100000 - RSDT_PTR_MAGIC_STRING_RANGE_BASE)
#define RSDT_MAGIC_STRING "MCFG"
#define MAX_NUM_PCIE_BUSES 64 // TODO
#define PCIE_MEMORY_MAP_SIZE (MAX_NUM_PCIE_BUSES * 1024 * 1024)

    HANDLE hDriver = INVALID_HANDLE_VALUE;
    struct ACPISDTHeader {
        char Signature[4];
        uint32_t Length;
        uint8_t Revision;
        uint8_t Checksum;
        char OEMID[6];
        char OEMTableID[8];
        uint32_t OEMRevision;
        uint32_t CreatorID;
        uint32_t CreatorRevision;
    };
    struct RSDT {
        struct ACPISDTHeader h;
        uint32_t PointerToOtherSDT[];
    };

    BOOL GetFirmwarePrivilege()
    {
        BOOL result = FALSE;
        HANDLE processToken = NULL;
        TOKEN_PRIVILEGES privileges = { 0 };
        HANDLE process = GetCurrentProcess();
        LUID luid = { 0 };

        if (OpenProcessToken(process, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &processToken)) {
            if (LookupPrivilegeValue(NULL, SE_SYSTEM_ENVIRONMENT_NAME, &luid)) {
                privileges.PrivilegeCount = 1;
                privileges.Privileges[0].Luid = luid;
                privileges.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

                if (AdjustTokenPrivileges(processToken, FALSE, &privileges, sizeof(TOKEN_PRIVILEGES), NULL, 0)) {
                    result = TRUE;
                }
            }

            CloseHandle(processToken);
        }

        CloseHandle(process);

        return result;
    }


    static uint32_t GetPCIeMappedMemoryAddress()
    {
        uint32_t RSDTTableBase = 0;
        char *virtualAddress = (char*)MapPhyMem(RSDT_PTR_MAGIC_STRING_RANGE_BASE, RSDT_PTR_MAGIC_STRING_RANGE_SIZE);
        unsigned int RSDTPtrOffset;
        for (RSDTPtrOffset = 0; RSDTPtrOffset < RSDT_PTR_MAGIC_STRING_RANGE_SIZE - strlen(RSDT_PTR_MAGIC_STRING); RSDTPtrOffset += 16) {
            if (strncmp(virtualAddress + RSDTPtrOffset, RSDT_PTR_MAGIC_STRING, strlen(RSDT_PTR_MAGIC_STRING)) == 0)
                break;
        }
        if (RSDTPtrOffset < RSDT_PTR_MAGIC_STRING_RANGE_SIZE - strlen(RSDT_PTR_MAGIC_STRING)) {
            //printf("Pointer to ACPI Root System Description Table (RSDT) found at 0x%08x.\n", (unsigned int)virtualAddress + rsdt_ptr_offset);
            RSDTTableBase = *(uint32_t *)(virtualAddress + RSDTPtrOffset + 0x10);
            UnmapPhyMem(virtualAddress, RSDT_PTR_MAGIC_STRING_RANGE_SIZE);

            struct RSDT *RSDTPtr = (struct RSDT *)MapPhyMem(RSDTTableBase, sizeof(RSDTPtr->h));
            int entries = (RSDTPtr->h.Length - sizeof(RSDTPtr->h)) / 4;
            uint32_t RSDTTableSize = RSDTPtr->h.Length;
            UnmapPhyMem(RSDTPtr, sizeof(RSDTPtr->h));
            RSDTPtr = (struct RSDT *)MapPhyMem(RSDTTableBase, RSDTTableSize);
            //printf("entries: %d\n", entries);

            struct ACPISDTHeader *h = NULL;
            for (int i = 0; i < entries; i++) {
                h = (struct ACPISDTHeader *)MapPhyMem(RSDTPtr->PointerToOtherSDT[i], 0x30);
                //printf("signature: %c%c%c%c\n", h->Signature[0], h->Signature[1], h->Signature[2], h->Signature[3]);
                if (!strncmp(h->Signature, RSDT_MAGIC_STRING, 4))
                    break;
                UnmapPhyMem(h, 0x30);
                h = NULL;
            }

            if (h) {
                //printf("ACPI Root System Description Table (RSDT) found at 0x%08x.\n", (unsigned int)h);
            } else {
                //printf("ACPI Root System Description Table (RSDT) not found.\n");
                UnmapPhyMem(RSDTPtr, RSDTTableSize);
                UnmapPhyMem(h, 0x30);
                return 0;
            }
            uint32_t pcie_mapped_memory_addr = *(uint32_t *)((uint8_t *)h + 0x2c);
            //printf("PCIe Memory Map found at 0x%08x.\n", pcie_mapped_memory_addr);
            UnmapPhyMem(RSDTPtr, RSDTTableSize);
            UnmapPhyMem(h, 0x30);

            return pcie_mapped_memory_addr;
        } else {
            UnmapPhyMem(virtualAddress, RSDT_PTR_MAGIC_STRING_RANGE_SIZE);
            uint8_t mcfg[1024];
            uint32_t mcfg_size;
            if (GetFirmwarePrivilege() && ((mcfg_size = GetSystemFirmwareTable('ACPI', 'GFCM', mcfg, sizeof(mcfg))) > 0)) {
                //printf("MCFG was retrieved through GetSystemFirmwareTable().\n");
                return *(uint32_t *)((uint8_t *)mcfg + 0x2c);
            } else {
                //printf("Pointer to ACPI Root System Description Table (RSDT) not found (%u)\n", GetLastError());
                return 0;
            }
        }

    }

    static BOOL HasElevatedPrivileges() {
        HANDLE token = NULL;
        BOOL result = FALSE;
        TOKEN_ELEVATION elevation;
        DWORD size = sizeof(TOKEN_ELEVATION);

        OpenProcessToken(GetCurrentProcess(), TOKEN_QUERY, &token);
        if (!token)
            return FALSE;

        if (GetTokenInformation(token, TokenElevation, &elevation, sizeof(elevation), &size))
            result = elevation.TokenIsElevated;
        CloseHandle(token);
        return result;
    }

    //get driver(phymem.sys) full path
    static BOOL GetDriverPath(PSTR driverPath)
    {
        PSTR slash;

        GetModuleFileNameA(GetModuleHandle(NULL), driverPath, MAX_PATH);
        if (GetLastError())
            return FALSE;

        slash = strrchr(driverPath, '\\');

        if (slash)
            slash[1] = '\0';
        else
            return FALSE;

        return TRUE;
    }


    //install and start driver
    __declspec(dllexport)
        BOOL LoadPhyMemDriver()
    {
        EnterCriticalSection(&phymemMutex);;

        BOOL result = FALSE;
        CHAR szDriverPath[MAX_PATH];

        if (!HasElevatedPrivileges())
            goto end;

        hDriver = CreateFileA("\\\\.\\PhyMem",
                              GENERIC_READ | GENERIC_WRITE,
                              0,
                              NULL,
                              OPEN_EXISTING,
                              FILE_ATTRIBUTE_NORMAL,
                              NULL);

        //If the driver is not running, install it
        if (hDriver == INVALID_HANDLE_VALUE) {
            GetDriverPath(szDriverPath);
            strcat(szDriverPath, "phymem.sys");
            if (!InstallDriver(szDriverPath, "PHYMEM"))
                goto end;
            if (!StartDriver("PHYMEM"))
                goto end;

            hDriver = CreateFileA("\\\\.\\PhyMem",
                                  GENERIC_READ | GENERIC_WRITE,
                                  0,
                                  NULL,
                                  OPEN_EXISTING,
                                  FILE_ATTRIBUTE_NORMAL,
                                  NULL);

            if (hDriver == INVALID_HANDLE_VALUE)
                goto end;
        }

        result = TRUE;

end:
        LeaveCriticalSection(&phymemMutex);;
        return result;
    }

    //stop and remove driver
    VOID UnloadPhyMemDriver()
    {
        EnterCriticalSection(&phymemMutex);;
        if (hDriver != INVALID_HANDLE_VALUE) {
            CloseHandle(hDriver);
            hDriver = INVALID_HANDLE_VALUE;
        }

        RemoveDriver("PHYMEM");
        LeaveCriticalSection(&phymemMutex);;
    }

    //map physical memory to user space
    __declspec(dllexport) PVOID MapPhyMem(DWORD64 physicalAddress, DWORD memSize)
    {
        EnterCriticalSection(&phymemMutex);;

        PVOID virtualAddress = NULL;	//mapped virtual addr
        PHYMEM_MEM pm;
        DWORD bytes = 0;
        BOOL ret = FALSE;

        pm.pvAddr = (PVOID)physicalAddress;	//physical address
        pm.dwSize = memSize;	//memory size

        if (hDriver != INVALID_HANDLE_VALUE) {
            ret = DeviceIoControl(hDriver, IOCTL_PHYMEM_MAP, &pm,
                                  sizeof(PHYMEM_MEM), &virtualAddress, sizeof(PVOID), &bytes, NULL);
        }

        LeaveCriticalSection(&phymemMutex);;
        return (ret && bytes == sizeof(PVOID)) ? virtualAddress : NULL;
    }

    //unmap memory
    __declspec(dllexport) VOID UnmapPhyMem(PVOID virtualAddress, DWORD memSize)
    {
        EnterCriticalSection(&phymemMutex);;
        PHYMEM_MEM pm;
        DWORD bytes = 0;

        pm.pvAddr = virtualAddress;
        pm.dwSize = memSize;

        if (hDriver != INVALID_HANDLE_VALUE)
            DeviceIoControl(hDriver, IOCTL_PHYMEM_UNMAP, &pm, sizeof(PHYMEM_MEM), NULL, 0, &bytes, NULL);
        LeaveCriticalSection(&phymemMutex);;
    }

    //read 1 byte from port
    BYTE ReadPortByte(WORD portAddr)
    {
        EnterCriticalSection(&phymemMutex);;
        PHYMEM_PORT pp;
        DWORD pv = 0;	//returned port value
        DWORD dwBytes;

        pp.dwPort = portAddr;
        pp.dwSize = 1;	//1 byte

        if (hDriver != INVALID_HANDLE_VALUE) {
            DeviceIoControl(hDriver, IOCTL_PHYMEM_GETPORT, &pp,
                            sizeof(PHYMEM_PORT), &pv, sizeof(DWORD), &dwBytes, NULL);
        }

        LeaveCriticalSection(&phymemMutex);;
        return (BYTE)pv;
    }

    //read 2 bytes from port
    WORD ReadPortWord(WORD portAddr)
    {
        EnterCriticalSection(&phymemMutex);;
        PHYMEM_PORT pp;
        DWORD pv = 0;	//returned port value
        DWORD dwBytes;

        pp.dwPort = portAddr;
        pp.dwSize = 2;	//2 bytes

        if (hDriver != INVALID_HANDLE_VALUE) {
            DeviceIoControl(hDriver, IOCTL_PHYMEM_GETPORT, &pp,
                            sizeof(PHYMEM_PORT), &pv, sizeof(DWORD), &dwBytes, NULL);
        }

        LeaveCriticalSection(&phymemMutex);;
        return (WORD)pv;
    }

    //read 4 bytes from port
    DWORD ReadPortLong(WORD portAddr)
    {
        EnterCriticalSection(&phymemMutex);;
        PHYMEM_PORT pp;
        DWORD pv = 0;	//returned port value
        DWORD dwBytes;

        pp.dwPort = portAddr;
        pp.dwSize = 4;	//4 bytes

        if (hDriver != INVALID_HANDLE_VALUE) {
            DeviceIoControl(hDriver, IOCTL_PHYMEM_GETPORT, &pp,
                            sizeof(PHYMEM_PORT), &pv, sizeof(DWORD), &dwBytes, NULL);
        }

        LeaveCriticalSection(&phymemMutex);;
        return pv;
    }

    //write 1 byte to port
    VOID WritePortByte(WORD portAddr, BYTE portValue)
    {
        EnterCriticalSection(&phymemMutex);;
        PHYMEM_PORT pp;
        DWORD dwBytes;

        pp.dwPort = portAddr;
        pp.dwValue = portValue;
        pp.dwSize = 1;	//1 byte

        if (hDriver != INVALID_HANDLE_VALUE) {
            DeviceIoControl(hDriver, IOCTL_PHYMEM_SETPORT, &pp,
                            sizeof(PHYMEM_PORT), NULL, 0, &dwBytes, NULL);
        }
        LeaveCriticalSection(&phymemMutex);;
    }

    //write 2 bytes to port
    VOID WritePortWord(WORD portAddr, WORD portValue)
    {
        EnterCriticalSection(&phymemMutex);;
        PHYMEM_PORT pp;
        DWORD dwBytes;

        pp.dwPort = portAddr;
        pp.dwValue = portValue;
        pp.dwSize = 2;	//2 bytes

        if (hDriver != INVALID_HANDLE_VALUE) {
            DeviceIoControl(hDriver, IOCTL_PHYMEM_SETPORT, &pp,
                            sizeof(PHYMEM_PORT), NULL, 0, &dwBytes, NULL);
        }
        LeaveCriticalSection(&phymemMutex);;
    }

    //write 4 bytes to port
    VOID WritePortLong(WORD portAddr, DWORD portValue)
    {
        EnterCriticalSection(&phymemMutex);;
        PHYMEM_PORT pp;
        DWORD dwBytes;

        pp.dwPort = portAddr;
        pp.dwValue = portValue;
        pp.dwSize = 4;	//4 bytes

        if (hDriver != INVALID_HANDLE_VALUE) {
            DeviceIoControl(hDriver, IOCTL_PHYMEM_SETPORT, &pp,
                            sizeof(PHYMEM_PORT), NULL, 0, &dwBytes, NULL);
        }
        LeaveCriticalSection(&phymemMutex);;
    }

    //read pci configuration
    __declspec(dllexport)
        BOOL ReadPCI(DWORD busNum, DWORD devNum, DWORD funcNum,
                     DWORD regOff, DWORD bytes, PVOID pValue)
    {
        uint32_t PCIeMappedMemoryAddress = GetPCIeMappedMemoryAddress();
        if (!PCIeMappedMemoryAddress)
            return FALSE;

        char *virtualAddress = (char*)MapPhyMem(PCIeMappedMemoryAddress + 4096 * (funcNum + 8 * (devNum + 32 * busNum)), 4096);
        if (!virtualAddress)
            return FALSE;

        memcpy(pValue, virtualAddress + regOff, bytes);
        UnmapPhyMem(virtualAddress, 4096);

        return TRUE;
    }

    //write pci configuration
    __declspec(dllexport) BOOL WritePCI(DWORD busNum, DWORD devNum, DWORD funcNum,
                                        DWORD regOff, DWORD bytes, PVOID pValue)
    {
        uint32_t PCIeMappedMemoryAddress = GetPCIeMappedMemoryAddress();
        if (!PCIeMappedMemoryAddress)
            return FALSE;

        char *virtualAddress = (char*)MapPhyMem(PCIeMappedMemoryAddress + 4096 * (funcNum + 8 * (devNum + 32 * busNum)), 4096);
        if (!virtualAddress)
            return FALSE;

        memcpy(virtualAddress + regOff, pValue, bytes);
        UnmapPhyMem(virtualAddress, 4096);

        return TRUE;
    }

    static uint32_t configRegistersBaseArray[256] = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
    };

    static volatile uint32_t *virtualAddressArray[256] = {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
    };

    volatile uint32_t *GetVirtualAddress(uint32_t busNumber)
    {
        volatile uint32_t *virtualAddress = virtualAddressArray[busNumber];

        if (virtualAddress == NULL) {
            uint32_t configRegistersBase = configRegistersBaseArray[busNumber];

            if (!configRegistersBase) {
                if (!ReadPCI(busNumber, 0, 0, 0x24, 4, &configRegistersBase))
                    return FALSE;
                configRegistersBase &= 0xfffffff0;
                configRegistersBaseArray[busNumber] = configRegistersBase;
            }

            virtualAddress = (uint32_t *)MapPhyMem(configRegistersBase, MAX_BIOS_SIZE);
            if (!virtualAddress)
                return FALSE;

            virtualAddressArray[busNumber] = virtualAddress;
        }
        return virtualAddress;
    }

    __declspec(dllexport)
        BOOL ReadFromAMDGPURegister(int32_t busNum, uint32_t regNo, uint32_t *ptrValue)
    {
        volatile uint32_t *virtualAddress = GetVirtualAddress(busNum);

        *ptrValue = *(virtualAddress + regNo);

        return TRUE;
    }

    __declspec(dllexport)
        BOOL WriteToAMDGPURegister(int32_t busNum, uint32_t regNo, uint32_t value)
    {
        volatile uint32_t *virtualAddress = GetVirtualAddress(busNum);

        *(virtualAddress + regNo) = value;
        
        return TRUE;
    }

    __declspec(dllexport)
        BOOL SMU7_ReadDWORD(uint32_t busNum, uint32_t SMCAddress, uint32_t *value)
    {
        const uint32_t mmSMC_IND_ACCESS_CNTL = 0x90;
        const uint32_t mmSMC_IND_INDEX_8 = 0x8e;
        const uint32_t mmSMC_IND_DATA_8 = 0x8f;
        const uint32_t SMC_IND_ACCESS_CNTL__AUTO_INCREMENT_IND_8_MASK = 0x100;

        volatile uint32_t *virtualAddress = GetVirtualAddress(busNum);

        *(virtualAddress + mmSMC_IND_INDEX_8) = SMCAddress;
        *(virtualAddress + mmSMC_IND_ACCESS_CNTL) &= ~SMC_IND_ACCESS_CNTL__AUTO_INCREMENT_IND_8_MASK;
        *value = *(virtualAddress + mmSMC_IND_DATA_8);

        UnmapPhyMem((PVOID)virtualAddress, 256 * 1024);

        return TRUE;
    }

    __declspec(dllexport)
        BOOL SMU7_WriteDWORD(uint32_t busNum, uint32_t SMCAddress, uint32_t value)
    {
        const uint32_t mmSMC_IND_ACCESS_CNTL = 0x90;
        const uint32_t mmSMC_IND_INDEX_8 = 0x8e;
        const uint32_t mmSMC_IND_DATA_8 = 0x8f;
        const uint32_t SMC_IND_ACCESS_CNTL__AUTO_INCREMENT_IND_8_MASK = 0x100;

        volatile uint32_t *virtualAddress = GetVirtualAddress(busNum);

        *(virtualAddress + mmSMC_IND_INDEX_8) = SMCAddress;
        *(virtualAddress + mmSMC_IND_ACCESS_CNTL) &= ~SMC_IND_ACCESS_CNTL__AUTO_INCREMENT_IND_8_MASK;
        *(virtualAddress + mmSMC_IND_DATA_8) = value;

        UnmapPhyMem((PVOID)virtualAddress, 256 * 1024);

        return TRUE;
    }

    __declspec(dllexport)
        BOOL UpdateGMC81Registers(uint32_t busNum,
                                  uint32_t arbDramTiming,
                                  uint32_t arbDramTiming2,
                                  uint32_t seqRasTiming,
                                  uint32_t seqCasTiming,
                                  uint32_t seqMiscTiming,
                                  uint32_t seqMiscTiming2,
                                  uint32_t seqMisc1,
                                  uint32_t seqMisc3,
                                  uint32_t seqMisc8,
                                  uint32_t seqWrCtlD0,
                                  uint32_t seqWrCtlD1,
                                  uint32_t seqWrCtl2,
                                  uint32_t arbDramTiming_1,
                                  uint32_t arbDramTiming2_1,
                                  uint32_t arbRttCntl0)
    {
        BOOL ret = true;  
        volatile uint32_t *virtualAddress = GetVirtualAddress(busNum);

        const uint32_t mmMC_SEQ_MISC1 = 0xa81;
        const uint32_t mmMC_SEQ_MISC3 = 0xa8b;
        const uint32_t mmMC_SEQ_MISC4 = 0xa8c;
        const uint32_t mmMC_SEQ_MISC8 = 0xa5f;
        const uint32_t mmMC_SEQ_MISC9 = 0xae7;
        const uint32_t mmMC_ARB_DRAM_TIMING = 0x9dd;
        const uint32_t mmMC_ARB_DRAM_TIMING2 = 0x9de;
        const uint32_t mmMC_SEQ_RAS_TIMING = 0xa28;
        const uint32_t mmMC_SEQ_CAS_TIMING = 0xa29;
        const uint32_t mmMC_SEQ_MISC_TIMING = 0xa2a;
        const uint32_t mmMC_SEQ_MISC_TIMING2 = 0xa2b;
        const uint32_t mmMC_SEQ_PMG_TIMING = 0xa2c;

        const uint32_t mmMC_PHY_TIMING_D0 = 0xacc;
        const uint32_t mmMC_PHY_TIMING_D1 = 0xacd;
        const uint32_t mmMC_PHY_TIMING_2 = 0xace;

        const uint32_t mmMC_SEQ_RD_CTL_D0 = 0xa2d;
        const uint32_t mmMC_SEQ_RD_CTL_D1 = 0xa2e;
        const uint32_t mmMC_SEQ_WR_CTL_D0 = 0xa2f;
        const uint32_t mmMC_SEQ_WR_CTL_D1 = 0xa30;
        const uint32_t mmMC_SEQ_WR_CTL_2 = 0xad5;

        const uint32_t mmMC_SEQ_RAS_TIMING_LP = 0xa9b;
        const uint32_t mmMC_SEQ_CAS_TIMING_LP = 0xa9c;
        const uint32_t mmMC_SEQ_MISC_TIMING_LP = 0xa9d;
        const uint32_t mmMC_SEQ_MISC_TIMING2_LP = 0xa9e;
        const uint32_t mmMC_SEQ_PMG_TIMING_LP = 0xad3;
        const uint32_t mmMC_ARB_DRAM_TIMING_1 = 0x9fc;
        const uint32_t mmMC_ARB_DRAM_TIMING2_1 = 0x9ff;
        const uint32_t mmMC_ARB_BURST_TIME = 0xa02;
        const uint32_t mmMC_SEQ_SUP_CNTL = 0xa32;
        const uint32_t mmMC_SEQ_DRAM_2 = 0xa27;
        const uint32_t mmMC_CONFIG = 0x800;
        const uint32_t mmPMI_STATUS_CNTL = 0x15;
        const uint32_t mmMC_SEQ_STATUS_M = 0xa7d;
        const uint32_t mmMC_SEQ_RD_CTL_D0_LP = 0xac7;
        const uint32_t mmMC_SEQ_RD_CTL_D1_LP = 0xac8;
        const uint32_t mmMC_SEQ_WR_CTL_2_LP = 0xad6;
        const uint32_t mmMC_SEQ_WR_CTL_D0_LP = 0xa9f;
        const uint32_t mmMC_SEQ_WR_CTL_D1_LP = 0xaa0;
        const uint32_t mmMC_ARB_RTT_CNTL0 = 0x9d0;

#define mmSRBM_STATUS 0x394
#define SRBM_STATUS__VMC_BUSY_MASK 0x100
#define SRBM_STATUS__MCB_BUSY_MASK 0x200
#define SRBM_STATUS__MCB_NON_DISPLAY_BUSY_MASK 0x400
#define SRBM_STATUS__MCC_BUSY_MASK 0x800
#define SRBM_STATUS__MCD_BUSY_MASK 0x1000
#define SRBM_STATUS__VMC1_BUSY_MASK 0x2000

        while (*(virtualAddress + mmSRBM_STATUS)
               & (SRBM_STATUS__MCB_BUSY_MASK |
                  SRBM_STATUS__MCB_NON_DISPLAY_BUSY_MASK |
                  SRBM_STATUS__MCC_BUSY_MASK |
                  SRBM_STATUS__MCD_BUSY_MASK |
                  SRBM_STATUS__VMC_BUSY_MASK |
                  SRBM_STATUS__VMC1_BUSY_MASK))
            ;

#define mmMC_SHARED_BLACKOUT_CNTL 0x82b
#define mmBIF_FB_EN  0x1524
#define BIF_FB_EN__FB_READ_EN_MASK 0x1
#define BIF_FB_EN__FB_WRITE_EN_MASK 0x2

        uint32_t blackout = *(virtualAddress + mmMC_SHARED_BLACKOUT_CNTL);
        if ((blackout & 0x7) != 1) {
            *(virtualAddress + mmBIF_FB_EN) = 0;
            blackout = (blackout & 0xfffffff8) | 1;
            *(virtualAddress + mmMC_SHARED_BLACKOUT_CNTL) = blackout;
        }

        //std::this_thread::sleep_for(std::chrono::microseconds(200));

        //*(virtualAddress + mmMC_SEQ_SUP_CNTL) = 0x0;

        std::this_thread::sleep_for(std::chrono::microseconds(200));

        if (   *(virtualAddress + mmMC_ARB_DRAM_TIMING) != arbDramTiming
            || *(virtualAddress + mmMC_ARB_DRAM_TIMING2) != arbDramTiming2
            || *(virtualAddress + mmMC_ARB_DRAM_TIMING_1) != arbDramTiming_1
            || *(virtualAddress + mmMC_ARB_DRAM_TIMING2_1) != arbDramTiming2_1
            || *(virtualAddress + mmMC_ARB_RTT_CNTL0) != arbRttCntl0
            || *(virtualAddress + mmMC_SEQ_RAS_TIMING) != seqRasTiming
            || *(virtualAddress + mmMC_SEQ_MISC_TIMING) != seqMiscTiming
            || *(virtualAddress + mmMC_SEQ_MISC1) != seqMisc1
            || *(virtualAddress + mmMC_SEQ_MISC3) != seqMisc3
            || *(virtualAddress + mmMC_SEQ_MISC8) != seqMisc8) {

            *(virtualAddress + mmMC_ARB_DRAM_TIMING) = arbDramTiming;
            *(virtualAddress + mmMC_ARB_DRAM_TIMING2) = arbDramTiming2;
            *(virtualAddress + mmMC_ARB_DRAM_TIMING_1) = arbDramTiming_1;
            *(virtualAddress + mmMC_ARB_DRAM_TIMING2_1) = arbDramTiming2_1;
            *(virtualAddress + mmMC_ARB_RTT_CNTL0) = arbRttCntl0;

            *(virtualAddress + mmMC_SEQ_RAS_TIMING) = seqRasTiming;
            *(virtualAddress + mmMC_SEQ_MISC_TIMING) = seqMiscTiming;
            *(virtualAddress + mmMC_SEQ_MISC1) = seqMisc1;
            *(virtualAddress + mmMC_SEQ_MISC3) = seqMisc3;
            *(virtualAddress + mmMC_SEQ_MISC8) = seqMisc8;
        } else {
            seqCasTiming = (*(virtualAddress + mmMC_SEQ_CAS_TIMING) & 0xff000000) | (seqCasTiming & ~0xff000000);
            seqMiscTiming2 = (*(virtualAddress + mmMC_SEQ_MISC_TIMING2) & 0x001fe000) | (seqMiscTiming2 & ~0x001fe000);

            if (   *(virtualAddress + mmMC_SEQ_CAS_TIMING) != seqCasTiming
                || *(virtualAddress + mmMC_SEQ_MISC_TIMING2) != seqMiscTiming2) {

                *(virtualAddress + mmMC_SEQ_CAS_TIMING) = (*(virtualAddress + mmMC_SEQ_CAS_TIMING) & 0xff000000) | (seqCasTiming & ~0xff000000);
                *(virtualAddress + mmMC_SEQ_MISC_TIMING2) = (*(virtualAddress + mmMC_SEQ_MISC_TIMING2) & 0x001fe000) | (seqMiscTiming2 & ~0x001fe000);
            } else {
                //*(virtualAddress + mmMC_SEQ_WR_CTL_D0) = seqWrCtlD0;
                //*(virtualAddress + mmMC_SEQ_WR_CTL_D1) = seqWrCtlD1;
                //*(virtualAddress + mmMC_SEQ_WR_CTL_2)  = seqWrCtl2;
            }
        }

        *(virtualAddress + mmMC_SEQ_SUP_CNTL) = 0x8;
        *(virtualAddress + mmMC_SEQ_SUP_CNTL) = 0x4;
        *(virtualAddress + mmMC_SEQ_SUP_CNTL) = 0x1;

        *(virtualAddress + mmMC_SHARED_BLACKOUT_CNTL) = (*(virtualAddress + mmMC_SHARED_BLACKOUT_CNTL) & 0xfffffff8);
        *(virtualAddress + mmBIF_FB_EN) |= 0x3;

#if FALSE
        char filename[256];
        sprintf(filename, "timing%04d.log", busNum);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "updated: %d\n", updated);
        fclose(file);
#endif

        return ret;
    }


    static struct {
        struct atom_context *context;
        uint8_t *BIOS;
        int    BIOSSize;
        struct card_info card_info;
    } ATOMBIOSContextArray[256] =
    {
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
        { NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },{ NULL, NULL, 0 },
    };

#define AMD_VBIOS_SIGNATURE " 761295520"
#define AMD_VBIOS_SIGNATURE_OFFSET 0x30
#define AMD_VBIOS_SIGNATURE_SIZE sizeof(AMD_VBIOS_SIGNATURE)
#define AMD_VBIOS_SIGNATURE_END (AMD_VBIOS_SIGNATURE_OFFSET + AMD_VBIOS_SIGNATURE_SIZE)
#define AMD_IS_VALID_VBIOS(p) ((p)[0] == 0x55 && (p)[1] == 0xAA)
//#define AMD_VBIOS_LENGTH(p) ((p)[2] << 9)

    void reg_write(struct card_info *info, uint32_t addr, uint32_t data)
    {
#if FALSE
        char filename[256];
        sprintf(filename, "bios%04d.log", info->bus_number);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "reg_write, addr: 0x%08x, data: 0x%08x\n", addr, data);
        fclose(file);
#endif

        WriteToAMDGPURegister(info->bus_number, addr, data);
    }

    uint32_t reg_read(struct card_info *info, uint32_t addr)
    {
        uint32_t data;
        ReadFromAMDGPURegister(info->bus_number, addr, &data);
      
#if FALSE
        char filename[256];
        sprintf(filename, "bios%04d.log", info->bus_number);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "reg_read, addr: 0x%08x, data: 0x%08x\n", addr, data);
        fclose(file);
#endif

        return data;
    }

    void ioreg_write(struct card_info *info, uint32_t addr, uint32_t data)
    {
        char filename[256];
        sprintf(filename, "bios%04d.log", info->bus_number);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "ioreg_write, addr: 0x%08x, data: 0x%08x\n", addr, data);
        fclose(file);
    }

    uint32_t ioreg_read(struct card_info *info, uint32_t addr)
    {
        uint32_t data = 0x0;

        char filename[256];
        sprintf(filename, "bios%04d.log", info->bus_number);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "ioreg_read, addr: 0x%08x, data: 0x%08x\n", addr, data);
        fclose(file);

        return data;
    }

    void mc_write(struct card_info *info, uint32_t addr, uint32_t data)
    {
        char filename[256];
        sprintf(filename, "bios%04d.log", info->bus_number);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "mc_write, addr: 0x%08x, data: 0x%08x\n", addr, data);
        fclose(file);
    }

    uint32_t mc_read(struct card_info *info, uint32_t addr)
    {
        uint32_t data = 0x0;

        char filename[256];
        sprintf(filename, "bios%04d.log", info->bus_number);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "mc_read, addr: 0x%08x, data: 0x%08x\n", addr, data);
        fclose(file);

        return data;
    }

    void pll_write(struct card_info *info, uint32_t addr, uint32_t data)
    {
        char filename[256];
        sprintf(filename, "bios%04d.log", info->bus_number);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "pll_write, addr: 0x%08x, data: 0x%08x\n", addr, data);
        fclose(file);
    }

    uint32_t pll_read(struct card_info *info, uint32_t addr)
    {
        uint32_t data = 0x0;

        char filename[256];
        sprintf(filename, "bios%04d.log", info->bus_number);
        FILE *file = fopen(filename, "a+");
        fprintf(file, "pll_read, addr: 0x%08x, data: 0x%08x\n", addr, data);
        fclose(file);

        return data;
    }

    __declspec(dllexport)
        BOOL ATOMBIOS_Load(uint32_t bus_number)
    {
        std::lock_guard<std::mutex> guard(ATOMBIOSMutex);

        uint32_t  bios_addr;
        uint16_t  command;

        ReadPCI(bus_number, 0, 0, 0x4, sizeof(command), &command);
        command |= 2;
        WritePCI(bus_number, 0, 0, 0x4, sizeof(command), &command);

        ReadPCI(bus_number, 0, 0, 0x24, sizeof(bios_addr), &bios_addr);
        bios_addr = ((bios_addr ^ 0x80000) & 0xffffe000);

        bios_addr = (bios_addr | 0x1);
        WritePCI(bus_number, 0, 0, 0x30, sizeof(bios_addr), &bios_addr);
        bios_addr = (bios_addr & 0xfffffff0);

        uint8_t *mapped_bios = (uint8_t *)MapPhyMem(bios_addr, MAX_BIOS_SIZE);
        if (mapped_bios == NULL)
            return FALSE;

        if (!AMD_IS_VALID_VBIOS(mapped_bios)) {
            UnmapPhyMem(mapped_bios, MAX_BIOS_SIZE);
            return FALSE;
        }

        uint32_t bios_length = MAX_BIOS_SIZE; // AMD_VBIOS_LENGTH(mapped_bios);
        uint8_t *bios = (uint8_t *)malloc(bios_length);
        if (bios == NULL) {
            UnmapPhyMem(mapped_bios, MAX_BIOS_SIZE);
            return FALSE;
        }
        memcpy(bios, mapped_bios, bios_length);

        bios_addr = 0;
        WritePCI(bus_number, 0, 0, 0x30, sizeof(bios_addr), &bios_addr);
        UnmapPhyMem(mapped_bios, MAX_BIOS_SIZE);

#if FALSE
        char filename[256];
        sprintf(filename, "bios%04d.rom", bus_number);
        FILE *file = fopen(filename, "wb");
        fwrite(bios, sizeof(uint8_t), bios_length, file);
        fclose(file);
#endif
        ATOMBIOSContextArray[bus_number].card_info.bus_number = bus_number;
        ATOMBIOSContextArray[bus_number].card_info.reg_read = reg_read;
        ATOMBIOSContextArray[bus_number].card_info.reg_write = reg_write;
        ATOMBIOSContextArray[bus_number].card_info.ioreg_read = ioreg_read;
        ATOMBIOSContextArray[bus_number].card_info.ioreg_write = ioreg_write;
        ATOMBIOSContextArray[bus_number].card_info.mc_read = mc_read;
        ATOMBIOSContextArray[bus_number].card_info.mc_write = mc_write;
        ATOMBIOSContextArray[bus_number].card_info.pll_read = pll_read;
        ATOMBIOSContextArray[bus_number].card_info.pll_write = pll_write;
        ATOMBIOSContextArray[bus_number].context = amdgpu_atom_parse(&ATOMBIOSContextArray[bus_number].card_info, bios);

        uint16_t base = *(uint16_t *)((char *)bios + ATOM_ROM_TABLE_PTR);
        uint16_t str_pos = *(uint16_t *)((char *)bios + base + ATOM_ROM_MSG_PTR);
        char *str = ((char *)bios + str_pos);
        while (*str && ((*str == '\n') || (*str == '\r')))
            str++;
        char bios_name[1024];
        strncpy(bios_name, str, 1023);
        bios_name[1023] = '\0';
        for (str = bios_name; *str; ++str)
            if ((*str == '\n') || (*str == '\r'))
                *str = '\0';
        for (str = bios_name + strlen(bios_name) - 1; (str != bios_name) && ((*str == '\n') || (*str == '\r') || (*str == ' ') || (*str == '\t')); --str)
            *str = '\0';

#if FALSE
        sprintf(filename, "bios%04d.log", bus_number);
        file = fopen(filename, "a+");
        fprintf(file, "bios_name: %s\n", bios_name);
        fclose(file);
#endif
        
        return TRUE;
    }

    __declspec(dllexport)
        BOOL ATOMBIOS_SetOverclockingSettings(uint32_t busNum, int32_t engineClock, int32_t VDDC, int32_t memoryClock, int32_t VDDCI)
    {
        std::lock_guard<std::mutex> guard(ATOMBIOSMutex);

        if (ATOMBIOSContextArray[busNum].context == NULL)
            return FALSE;

        uint8_t frev, crev;
        int priority = GetThreadPriority(GetCurrentThread());
        SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_TIME_CRITICAL);

        if (false) {
            int index = GetIndexIntoMasterTable(COMMAND, SetEngineClock);
            amdgpu_atom_parse_cmd_header(ATOMBIOSContextArray[busNum].context, index, &frev, &crev);

            DYNAMICE_ENGINE_SETTINGS_PARAMETER  args;
            args.ulClock.ulClockFreq = (uint64_t)engineClock * 100;
            args.ulClock.ulComputeClockFlag = COMPUTE_ENGINE_PLL_PARAM;
            args.ulMemoryClock = (uint64_t)memoryClock * 100;
            amdgpu_atom_execute_table(ATOMBIOSContextArray[busNum].context, index, (uint32_t *)&args);
        }

        if (false) {
            int index = GetIndexIntoMasterTable(COMMAND, SetMemoryClock);
            amdgpu_atom_parse_cmd_header(ATOMBIOSContextArray[busNum].context, index, &frev, &crev);

            DYNAMICE_ENGINE_SETTINGS_PARAMETER  args;
            args.ulClock.ulClockFreq = (uint64_t)memoryClock * 100;
            args.ulClock.ulComputeClockFlag = COMPUTE_MEMORY_PLL_PARAM;
            amdgpu_atom_execute_table(ATOMBIOSContextArray[busNum].context, index, (uint32_t *)&args);
        }

        if (true) {
            int index = GetIndexIntoMasterTable(COMMAND, SetVoltage);
            amdgpu_atom_parse_cmd_header(ATOMBIOSContextArray[busNum].context, index, &frev, &crev);

            union {
                SET_VOLTAGE_PARAMETERS_V1_3 in;
            } args;
            args.in.ucVoltageType = VOLTAGE_TYPE_VDDC;
            args.in.ucVoltageMode = ATOM_SET_VOLTAGE;
            args.in.usVoltageLevel = VDDC;
            amdgpu_atom_execute_table(ATOMBIOSContextArray[busNum].context, index, (uint32_t *)&args);
        }

        if (true) {
            int index = GetIndexIntoMasterTable(COMMAND, SetVoltage);
            //amdgpu_atom_parse_cmd_header(ATOMBIOSContextArray[bus_number].context, index, &frev, &crev);

            union {
                SET_VOLTAGE_PARAMETERS_V1_3 in;
            } args;
            args.in.ucVoltageType = VOLTAGE_TYPE_VDDCI;
            args.in.ucVoltageMode = ATOM_SET_VOLTAGE;
            args.in.usVoltageLevel = VDDCI;
            amdgpu_atom_execute_table(ATOMBIOSContextArray[busNum].context, index, (uint32_t *)&args);
        }

        SetThreadPriority(GetCurrentThread(), priority);

        return TRUE;
    }

    __declspec(dllexport)
        BOOL ATOMBIOS_SetMemoryTimings(uint32_t bus_number, int32_t coreClock, int32_t memoryClock)
    {
        std::lock_guard<std::mutex> guard(ATOMBIOSMutex);

        if (ATOMBIOSContextArray[bus_number].context == NULL)
            return FALSE;

        int index = GetIndexIntoMasterTable(COMMAND, DynamicMemorySettings);
        uint8_t frev, crev;
        amdgpu_atom_parse_cmd_header(ATOMBIOSContextArray[bus_number].context, index, &frev, &crev);
#if FALSE
        sprintf(filename, "Logs\\bios%04d.log", bus_number);
        file = fopen(filename, "a+");
        fprintf(file, "frev: %d\n", (int)(frev));
        fprintf(file, "crev: %d\n", (int)(crev));
        fclose(file);
#endif

        SET_ENGINE_CLOCK_PS_ALLOCATION engine_clock_parameters;
        engine_clock_parameters.ulTargetEngineClock = (((unsigned long)coreClock * 100) & SET_CLOCK_FREQ_MASK) | (COMPUTE_ENGINE_PLL_PARAM << 24);
        engine_clock_parameters.sReserved.ulClock = (((unsigned long)memoryClock * 100) & SET_CLOCK_FREQ_MASK);
        amdgpu_atom_execute_table(ATOMBIOSContextArray[bus_number].context, index, (uint32_t *)&engine_clock_parameters);
        
        UnmapPhyMem((PVOID)virtualAddressArray[bus_number], MAX_BIOS_SIZE);
        virtualAddressArray[bus_number] = NULL;

        return TRUE;
    }
}

