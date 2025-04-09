using System;
using System.Diagnostics;
using System.Media;

namespace ProcessViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Thread watcher = new Thread(Monitor);
            watcher.IsBackground = true;
            watcher.Start();
        }

        void Monitor()
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

                    if (InvokeRequired) Invoke(() => UpdateProcessesView); //check if it can be invoked


                    Console.WriteLine("updated");
                }

                Thread.Sleep(2000); //goodnait
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            processesView.View = View.Details;
            processesView.FullRowSelect = true;

            processesView.Columns.Add("Name");
            processesView.Columns.Add("Active");
            processesView.Columns.Add("Start");
            processesView.Columns.Add("Stop");

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void AddItem(Trackable app)
        {
            ListViewItem row = new ListViewItem(app.fileName);
            app.active = (app.processId > 0 && app.processName != null);
            row.SubItems.Add(app.active ? "true" : "false");
            processesView.Items.Add(row);

            Console.WriteLine($"{app.fileName}, {app.processName}, {app.processId}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog FD = new OpenFileDialog();

            if (FD.ShowDialog() == DialogResult.OK)
            {
                FileInfo File = new FileInfo(FD.FileName);
                string fileName = Path.GetFileName(FD.FileName);
                string cleanFileName = Path.GetFileNameWithoutExtension(FD.FileName);
                Process[] processes = Process.GetProcessesByName(cleanFileName);

                /*
                 string cleanFileName = Path.GetFileNameWithoutExtension(FD.FileName);
Process[] allProcesses = Process.GetProcesses();
foreach (Process process in allProcesses)
{
    string processName = Path.GetFileNameWithoutExtension(process.MainModule.FileName);
    if (processName.Equals(cleanFileName, StringComparison.OrdinalIgnoreCase))
    {
        // Process found
    }
} // kijk hier naar
                 
                 */

                Process process = null;
                int processId = 0;
                string processName = "null";

                if (processes.Length > 0) process = processes[0];
                else if (process != null)
                {
                    processId = process.Id;
                    processName = process.ProcessName;
                }

                Trackable app = new Trackable();

                if (Trackable.allTrackableApplications.Count < 1)
                {
                    app.SetTrackable(cleanFileName, processId, processName);
                    Trackable.allTrackableApplications.Add(app);
                    AddItem(app);
                }
                else
                {
                    foreach (Trackable trackable in Trackable.allTrackableApplications)
                    {
                        if (trackable.fileName == cleanFileName)
                        {
                            app = trackable;
                            SystemSounds.Exclamation.Play();
                            MessageBox.Show("You only need to add an application once.", "Already tracking this application.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            break;
                        }
                        else
                        {
                            app.SetTrackable(cleanFileName, processId, processName);
                            Trackable.allTrackableApplications.Add(app);
                            AddItem(app);
                        }
                    }
                }
            }
        }

        public void UpdateProcessesView()
        {
            foreach (Trackable app in Trackable.allTrackableApplications.ToList())
            {
                Process[] processes = Process.GetProcessesByName(app.fileName);
                foreach (ListViewItem i in processesView.Items)
                {
                    if (i.Text != app.fileName) continue;
                    app.active = processes.Length > 0 ? true : false;
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("happens");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void processesView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
