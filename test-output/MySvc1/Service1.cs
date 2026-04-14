using System.ServiceProcess;
using System.Diagnostics;

public class Service1 : ServiceBase
{
    public Service1()
    {
        this.ServiceName = "MySvc1";
    }

    protected override void OnStart(string[] args)
    {
        EventLog.WriteEntry(this.ServiceName + " started.");
    }

    protected override void OnStop()
    {
        EventLog.WriteEntry(this.ServiceName + " stopped.");
    }
}