// CpuInfo.cpp: CCpuInfo 的实现

#include "pch.h"
#include "CpuInfo.h"


// CCpuInfo



STDMETHODIMP CCpuInfo::GetCpuNum(LONG* num)
{
	// TODO: 在此处添加实现代码
	SYSTEM_INFO sysinfo;
	GetSystemInfo(&sysinfo);
	*num = sysinfo.dwNumberOfProcessors;
	return S_OK;
}
