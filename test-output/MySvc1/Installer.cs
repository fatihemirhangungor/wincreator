using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
    public ProjectInstaller()
    {
        var processInstaller = new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem };
        var serviceInstaller = new ServiceInstaller { StartType = ServiceStartMode.Automatic, ServiceName = "MySvc1" };
        Installers.Add(processInstaller);
        Installers.Add(serviceInstaller);
    }
}