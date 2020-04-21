using System;
using System.Threading;
using System.Runtime.InteropServices;
using StructRobotBase;
using StructSendMoves;

namespace RoboCaptchaTeste
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_SHOWMINIMIZED = 2;

        static void Main(string[] args)
        {
            IntPtr winHandle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(winHandle, SW_SHOWMINIMIZED);

            string url = string.Empty;
            var exec = new RobotBase();
            exec.StarterBrowser();

            var test = new SendMoves();

            Thread.Sleep(8000);
            test.Start();

        }
    }

}