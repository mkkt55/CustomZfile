SYSTEM_INFO sysinfo;

MEMORYSTATUSEX memoryInfo;

double totalSpace = 0;

int numCpu = 0;

bool hasInit = false;

const double kb = 1024;
const double mb = kb * 1024;
const double gb = mb * 1024;


void checkAndInit();

extern "C" __declspec(dllexport) int GetNumCpu();
extern "C" __declspec(dllexport) double GetTotalMemory();
extern "C" __declspec(dllexport) double GetFreeMemory();
extern "C" __declspec(dllexport) double GetTotalSpace();
extern "C" __declspec(dllexport) double GetAvailableSpace();


