using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using FarsiLibrary.Utils;

namespace FarsiLibrary.WinFormDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Thread.CurrentThread.CurrentUICulture = new PersianCultureInfo();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa-ir");
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWinForm());
        }
    }
}