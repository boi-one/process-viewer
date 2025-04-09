using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ProcessViewer
{
    internal static class Program
    {

        static Form1 mainForm = new Form1();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(mainForm);
        }
    }
}