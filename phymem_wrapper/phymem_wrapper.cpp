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



extern "C" {
#include "phymem.h"
#include "phymem_wrapper.h"

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
            //printf("Pointer to ACPI Root System Description Table (RSDT) found at 0x%08x.\n", (unsigned int)virtual_addr + rsdt_ptr_offset);
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

    static volatile uint32_t *virtualAddrArray[256] = {
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

    __declspec(dllexport)
        BOOL ReadFromAMDGPURegister(int32_t busNum, uint32_t regNo, uint32_t *ptrValue)
    {
        uint32_t configRegistersBase = configRegistersBaseArray[busNum];

        if (!configRegistersBase) {
            if (!ReadPCI(busNum, 0, 0, 0x24, 4, &configRegistersBase))
                return FALSE;
            configRegistersBase &= 0xfffffff0;
            configRegistersBaseArray[busNum] = configRegistersBase;
        }

        uint32_t *virtual_addr = (uint32_t *)MapPhyMem(configRegistersBase, 256 * 1024);
        if (!virtual_addr)
            return FALSE;
        *ptrValue = *(virtual_addr + regNo);
        UnmapPhyMem(virtual_addr, 256 * 1024);

        return TRUE;
    }



    __declspec(dllexport)
        BOOL WriteToGMC81Register(int32_t busNum, uint32_t regNo, uint32_t value, uint32_t mask)
    {
        uint32_t configRegistersBase = configRegistersBaseArray[busNum];

        if (!configRegistersBase) {
            if (!ReadPCI(busNum, 0, 0, 0x24, 4, &configRegistersBase))
                return FALSE;
            configRegistersBase &= 0xfffffff0;
            configRegistersBaseArray[busNum] = configRegistersBase;
        }

        volatile uint32_t *virtualAddress = (volatile uint32_t *)MapPhyMem(configRegistersBase, 256 * 1024);
        if (!virtualAddress)
            return FALSE;

        const uint32_t mmMC_HUB_MISC_STATUS = 0x832;
        const uint32_t MC_HUB_MISC_STATUS__RPB_BUSY_MASK = 0x8000;
        const uint32_t MC_HUB_MISC_STATUS__GFX_BUSY_MASK = 0x80000;

        BOOL result = FALSE;
        if (!(virtualAddress[mmMC_HUB_MISC_STATUS] & (MC_HUB_MISC_STATUS__RPB_BUSY_MASK | MC_HUB_MISC_STATUS__GFX_BUSY_MASK))) {
            *(virtualAddress + regNo) = (*(virtualAddress + regNo) & ~mask) | (value & mask);
            result = TRUE;
        }
        UnmapPhyMem((uint32_t *)virtualAddress, 256 * 1024);

        return result;
    }

#define WAIT std::this_thread::sleep_for(std::chrono::nanoseconds(100))

    __declspec(dllexport)
        BOOL UpdateGMC81Registers(int32_t busNum,
                                  uint32_t value0, uint32_t mask0,
                                  uint32_t value1, uint32_t mask1,
                                  uint32_t value2,
                                  uint32_t value3,
                                  uint32_t value4,
                                  uint32_t value5, uint32_t mask5,
                                  uint32_t value6, uint32_t mask6,
                                  uint32_t value7, uint32_t mask7,
                                  uint32_t value8, uint32_t mask8,
                                  uint32_t value9, uint32_t mask9,
                                  uint32_t value10,
                                  uint32_t value11, uint32_t mask11,
                                  uint32_t value12, uint32_t mask12,
                                  uint32_t value13, uint32_t mask13)
    {
        BOOL ret = true;  
        uint32_t configRegistersBase = configRegistersBaseArray[busNum];

        if (!configRegistersBase) {
            if (!ReadPCI(busNum, 0, 0, 0x24, 4, &configRegistersBase))
                return FALSE;
            configRegistersBase &= 0xfffffff0;
            configRegistersBaseArray[busNum] = configRegistersBase;
        }

        volatile uint32_t *virtual_addr = (volatile uint32_t *)MapPhyMem(configRegistersBase, 256 * 1024);
        if (!virtual_addr)
            return FALSE;

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

        const uint32_t mmGRBM_STATUS = 0x2004;
        const uint32_t GRBM_STATUS__GUI_ACTIVE_MASK = 0x80000000;
        
        if ((*(virtual_addr + mmMC_SEQ_CAS_TIMING) & 0xff000000) == (value6 & 0xff000000)) {
            // *(virtual_addr + mmMC_SEQ_RD_CTL_D0) = 0x00f03278;
            // *(virtual_addr + mmMC_SEQ_RD_CTL_D1) = 0x00f0c178;
            // *(virtual_addr + mmMC_SEQ_WR_CTL_D0) = 0x2035f1ff;
            // *(virtual_addr + mmMC_SEQ_WR_CTL_D1) = 0x2035f1ff;
            // *(virtual_addr + mmMC_SEQ_WR_CTL_2) = 0x00000000;

            *(virtual_addr + mmMC_ARB_DRAM_TIMING) = (*(virtual_addr + mmMC_ARB_DRAM_TIMING) & ~mask0) | (value0 & mask0);
            *(virtual_addr + mmMC_ARB_DRAM_TIMING2) = (*(virtual_addr + mmMC_ARB_DRAM_TIMING2) & ~mask1) | (value1 & mask1);

            *(virtual_addr + mmMC_SEQ_RAS_TIMING) = (*(virtual_addr + mmMC_SEQ_RAS_TIMING) & ~mask5) | (value5 & mask5);
            *(virtual_addr + mmMC_SEQ_CAS_TIMING) = (*(virtual_addr + mmMC_SEQ_CAS_TIMING) & ~mask6) | (value6 & mask6);
            *(virtual_addr + mmMC_SEQ_MISC_TIMING) = (*(virtual_addr + mmMC_SEQ_MISC_TIMING) & ~mask7) | (value7 & mask7);
            *(virtual_addr + mmMC_SEQ_MISC_TIMING2) = (*(virtual_addr + mmMC_SEQ_MISC_TIMING2) & ~mask8) | (value8 & mask8);
            *(virtual_addr + mmMC_SEQ_PMG_TIMING) = (*(virtual_addr + mmMC_SEQ_PMG_TIMING) & ~mask9) | (value9 & mask9);

            *(virtual_addr + mmMC_SEQ_MISC1) = value2;
            *(virtual_addr + mmMC_SEQ_MISC3) = value3;
            *(virtual_addr + mmMC_SEQ_MISC8) = value4;

            *(virtual_addr + mmMC_SEQ_MISC4) = 0xe000cdd8;
            *(virtual_addr + mmMC_SEQ_MISC9) = value10;

            *(virtual_addr + mmMC_PHY_TIMING_D0) = (*(virtual_addr + mmMC_PHY_TIMING_D0) & ~mask11) | (value11 & mask11);
            *(virtual_addr + mmMC_PHY_TIMING_D1) = (*(virtual_addr + mmMC_PHY_TIMING_D1) & ~mask12) | (value12 & mask12);
            *(virtual_addr + mmMC_PHY_TIMING_2) = (*(virtual_addr + mmMC_PHY_TIMING_2) & ~mask13) | (value13 & mask13);
        }

        UnmapPhyMem((uint32_t *)virtual_addr, 256 * 1024);

        return ret;
    }
}