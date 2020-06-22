#include "pch.h"
#include "SystemInfo.h"

#include<windows.h>
#include<stdio.h>   
#include<tchar.h>


int GetNumCpu() {
    checkAndInit();
    return numCpu;
}

// Total bytes of physical memory.
double GetTotalMemory() {
    checkAndInit();
    return (double)memoryInfo.ullTotalPhys / gb;
}

// Free bytes of physical memory
double GetFreeMemory() {
    return (double)memoryInfo.ullAvailPhys / gb;
}

double GetTotalSpace() {
    checkAndInit();
    return totalSpace;
}

double GetAvailableSpace() {
    ULARGE_INTEGER freeSpace;
    GetDiskFreeSpaceExA(NULL, NULL, NULL, &freeSpace);
    return (double)freeSpace.QuadPart / gb;
    /*
    BOOL GetDiskFreeSpaceExA(
        LPCSTR          lpDirectoryName,
        PULARGE_INTEGER lpFreeBytesAvailableToCaller,
        PULARGE_INTEGER lpTotalNumberOfBytes,
        PULARGE_INTEGER lpTotalNumberOfFreeBytes
        );
    */
}

void checkAndInit()
{
    if (hasInit) {
        return;
    }

    memoryInfo.dwLength = sizeof(memoryInfo);
    GlobalMemoryStatusEx(&memoryInfo);

    ULARGE_INTEGER ts;
    GetDiskFreeSpaceExA(NULL, NULL, &ts, NULL);
    totalSpace = (double)ts.QuadPart / gb;

    GetSystemInfo(&sysinfo);
    numCpu = sysinfo.dwNumberOfProcessors;

    hasInit = true;

    /*
    _tprintf(TEXT("There is  %*ld percent of memory in use.\n"), WIDTH, statex.dwMemoryLoad);
    _tprintf(TEXT("There are %*I64d total Mbytes of paging file.\n"), WIDTH, statex.ullTotalPageFile);
    _tprintf(TEXT("There are %*I64d free Mbytes of paging file.\n"), WIDTH, statex.ullAvailPageFile);
    _tprintf(TEXT("There are %*I64d total Mbytes of virtual memory.\n"), WIDTH, statex.ullTotalVirtual);
    _tprintf(TEXT("There are %*I64d free Mbytes of virtual memory.\n"), WIDTH, statex.ullAvailVirtual);
    _tprintf(TEXT("There are %*I64d free Mbytes of extended memory.\n"), WIDTH, statex.ullAvailExtendedVirtual);
    */
}