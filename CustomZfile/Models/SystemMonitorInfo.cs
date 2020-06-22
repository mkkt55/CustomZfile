using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CustomZfile.Services;

namespace CustomZfile.Models
{
    // Depands on platform, implement by Win32Dll1 dll.
    public class SystemMonitorInfo
    {
        private const string arch = "x64";

        private delegate double DoubleVoidFunc();

        // Avoid getting wrong data at the first time.
        static SystemMonitorInfo()
        {
            double m = GetFreeMemory();
            m = GetTotalMemory();
        }

        //[DllImport("Win32Dll1.dll")]
        //private extern static int GetNumCpu();

        private static double GetTotalMemory()
        {
            IntPtr module = FunctionLoader.LoadLibrary("Win32Dll1.dll");
            if (module == IntPtr.Zero) // error handling
            {
                return 0;
            }

            // get a "pointer" to the method
            IntPtr method = FunctionLoader.GetProcAddress(module, "GetTotalMemory");
            if (method == IntPtr.Zero) // error handling
            {
                FunctionLoader.FreeLibrary(module);  // unload library
                return 0;
            }

            // convert "pointer" to delegate
            DoubleVoidFunc f = (DoubleVoidFunc)Marshal.GetDelegateForFunctionPointer(method, typeof(DoubleVoidFunc));

            // use function    
            return f();
        }

        private static double GetFreeMemory()
        {
            IntPtr module = FunctionLoader.LoadLibrary("Win32Dll1.dll");
            if (module == IntPtr.Zero) // error handling
            {
                return 0;
            }

            // get a "pointer" to the method
            IntPtr method = FunctionLoader.GetProcAddress(module, "GetFreeMemory");
            if (method == IntPtr.Zero) // error handling
            {
                FunctionLoader.FreeLibrary(module);  // unload library
                return 0;
            }

            // convert "pointer" to delegate
            DoubleVoidFunc f = (DoubleVoidFunc)Marshal.GetDelegateForFunctionPointer(method, typeof(DoubleVoidFunc));

            // use function    
            return f();
        }

        [DllImport("Win32Dll1.dll")]
        private extern static double GetTotalSpace();

        [DllImport("Win32Dll1.dll")]
        private extern static double GetAvailableSpace();


        public string osName
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public string osDesciption
        {
            get
            {
                return RuntimeInformation.OSDescription;
            }
        }

        public string machineArch
        {
            get
            {
                var ci = new ATLCustomZfileLib.CpuInfo();
                return string.Format("{0} core, {1}", ci.GetCpuNum().ToString(), arch);
            }
        }

        public string upTime
        {
            get
            {
                return SystemManager.StartTime;
            }
        }

        public double totalMemory
        {
            get
            {
                return GetTotalMemory();
            }
        }

        public double freeMemory
        {
            get
            {
                return GetFreeMemory();
            }
        }

        public double totalSpace
        {
            get
            {
                return GetTotalSpace();
            }
        }

        public double freeSpace
        {
            get
            {
                return GetFreeMemory();
            }
        }

        public string clrVersion
        {
            get
            {
                return RuntimeInformation.FrameworkDescription;
            }
        }
    }
}
