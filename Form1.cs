using System.Diagnostics;

namespace ProcessViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ListViewItem listViewItem;

        private void Form1_Load(object sender, EventArgs e)
        {
            processesView.View = View.Details;
            processesView.FullRowSelect = true;

            processesView.Columns.Add("Name");
            processesView.Columns.Add("Active");
            processesView.Columns.Add("Start");
            processesView.Columns.Add("Stop");

        }

        public class TrackableApplication
        {
            public string fileName = "";
            public int processId = 0;
            public string processName = "";
            public bool active = false;
            public int startTime = 0;
            public int stopTime = 0;
        }

        public static List<TrackableApplication> allTrackableApplications = new List<TrackableApplication>();

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog FD = new OpenFileDialog();

            if (FD.ShowDialog() == DialogResult.OK)
            {
                FileInfo File = new FileInfo(FD.FileName);
                string fileName = Path.GetFileName(FD.FileName);
                string cleanFileName = Path.GetFileNameWithoutExtension(FD.FileName);
                Process[] processes = Process.GetProcessesByName(cleanFileName);
                var app = new TrackableApplication();

                foreach(var trackable in allTrackableApplications)
                {
                    if(trackable.fileName == cleanFileName)
                    {
                        app = trackable;
                        break;
                    }
                    else
                    {
                        app.fileName = cleanFileName;
                        foreach (var p in processes)
                        {
                            app.processName = p.ProcessName;
                            app.processId = p.Id;
                        }
                    }
                } //TODO: herschrijf dit

                allTrackableApplications.Add(app);
                foreach (Process p in processes)
                {
                    app.processId = p.Id;
                    app.processName = p.ProcessName;
                }
                Console.WriteLine($"{app.fileName}, {app.processName}, {app.processId}");
                ListViewItem row = new ListViewItem(app.fileName);
                app.active = (app.processId > 0 && app.processName != null);
                row.SubItems.Add(app.active ? "true" : "false");
                processesView.Items.Add(row);
            }
        }

        public static void UpdateListView1()
        {
            foreach (var app in allTrackableApplications)
            {
                Process[] processes = Process.GetProcessesByName(app.fileName);
                foreach (var process in processes)
                {
                    if (process.ProcessName == app.processName || process.Id == app.processId)
                    {
                        
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("happens");
        }
    }
}
