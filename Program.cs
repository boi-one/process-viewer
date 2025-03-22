using System.Diagnostics;

namespace ProcessViewer
{
    internal static class Program
    {
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

            Application.Run(new Form1());

            
        }

        static void Monitor()
        {
            while(true)
            {
                foreach(var app in Trackable.allTrackableApplications)
                {
                    Process[] processes = Process.GetProcessesByName(app.fileName);
                    foreach (Process p in processes)
                    {
                        app.processId = p.Id;
                        app.processName = p.ProcessName;
                        Form1.UpdateListView1();
                    }
                }
                Console.WriteLine(Trackable.allTrackableApplications.Count);


                Thread.Sleep(2000); //goodnait
            }
        }
    }
}