using System.Diagnostics;

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

            Thread watcher = new Thread(Monitor);
            watcher.IsBackground = true;
            watcher.Start();

            Application.Run(mainForm);


        }

        static void Monitor()
        {
            while (true)
            {
                foreach (var app in Trackable.allTrackableApplications.ToList())
                {
                    Process[] processes = Process.GetProcessesByName(app.fileName);
                    Process process = null;
                    if (processes.Length > 0)
                    {
                        process = processes[0];
                    }

                    if (process == null)
                    {
                        app.processId = 0;
                        app.processName = "null";
                    }
                    else
                    {
                        app.processId = process.Id;
                        app.processName = process.ProcessName;
                    }

                    mainForm.Invoke(mainForm.UpdateProcessesView); //check if it can be invoked
                    
                    Console.WriteLine("updated");
                }

                Thread.Sleep(2000); //goodnait
            }
        }
    }
}