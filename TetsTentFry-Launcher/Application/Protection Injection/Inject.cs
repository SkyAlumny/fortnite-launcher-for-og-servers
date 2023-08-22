using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Protection
{
    public class Inject
    {
        public const int PROCESS_CREATE_THREAD = 2;
        public const int PROCESS_VM_OPERATION = 8;
        public const int PROCESS_VM_WRITE = 32;
        public const int PROCESS_VM_READ = 16;
        public const int PROCESS_QUERY_INFORMATION = 1024;
        public const uint PAGE_READWRITE = 4;
        public const uint MEM_COMMIT = 4096;
        public const uint MEM_RESERVE = 8192;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(
          int dwDesiredAccess,
          bool bInheritHandle,
          int dwThreadId);

        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(IntPtr hThread);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleCtrlHandler(
          Inject.HandlerRoutine HandlerRoutine,
          bool Add);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(
          int dwDesiredAccess,
          bool bInheritHandle,
          int dwProcessId);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(
          IntPtr hProcess,
          IntPtr lpAddress,
          uint dwSize,
          uint flAllocationType,
          uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          byte[] lpBuffer,
          uint nSize,
          out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(
          IntPtr hProcess,
          IntPtr lpThreadAttributes,
          uint dwStackSize,
          IntPtr lpStartAddress,
          IntPtr lpParameter,
          uint dwCreationFlags,
          IntPtr lpThreadId);
        public delegate bool HandlerRoutine(int dwCtrlType);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool BlockInput(bool fBlock);
        public static void InjectProtection(int processId, string path)
        {
           
            IntPtr hProcess = Inject.OpenProcess(1082, false, processId);
            IntPtr procAddress = Inject.GetProcAddress(Inject.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            uint num1 = (uint)((path.Length + 1) * Marshal.SizeOf(typeof(char)));
            IntPtr num2 = Inject.VirtualAllocEx(hProcess, IntPtr.Zero, num1, 12288U, 4U);
            Inject.WriteProcessMemory(hProcess, num2, Encoding.Default.GetBytes(path), num1, out UIntPtr _);
            Inject.CreateRemoteThread(hProcess, IntPtr.Zero, 0U, procAddress, num2, 0U, IntPtr.Zero);
    
        }
    }
}
