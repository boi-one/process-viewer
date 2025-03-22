using System;
using static ProcessViewer.Form1;

public class Trackable
{
    public static List<Trackable> allTrackableApplications = new List<Trackable>();

    public string fileName = "";
    public int processId = 0;
    public string processName = "";
    public bool active = false;
    public int startTime = 0;
    public int stopTime = 0;

    public Trackable()
    {

    }

    public void SetTrackable(string fname, int processid, string pname)
    {
        fileName = fname;
        processId = processid;
        processName = pname;
    }
}
