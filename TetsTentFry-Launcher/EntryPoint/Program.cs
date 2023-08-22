using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace tetstentfrylauncher
{
    public static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool BlockInput(bool fBlock);

    
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
      

        [STAThread]
        public static void Main()
        {
         
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
             

           
        }
    }
}
