using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace PaddleOCRSharpDemo
{
    static class Program
    {
        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
