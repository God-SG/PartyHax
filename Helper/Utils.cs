using System.Diagnostics;

namespace PartyHax.Helper
{
    public class Utils
    {
        public void KillXboxApps()
        {
            foreach (Process pr in Process.GetProcesses())
            {
                if (pr.ProcessName.Contains("xbox") || pr.ProcessName.Contains("Xbox"))
                {
                    pr.Kill();
                }
                if (pr.ProcessName.Contains("xboxApp") || pr.ProcessName.Contains("XboxApp"))
                {
                    pr.Kill();
                }
                if (pr.ProcessName.Contains("gamebar") || pr.ProcessName.Contains("GameBar"))
                {
                    pr.Kill();
                }
            }
        }     
        public string CheckDevice(string devicetype)
        {
            switch (devicetype)
            {
                case "Durango":
                    return "Xbox One";
                case "Scarlett":
                    return "Series X";
                case "Lockhart":
                    return "Series S";
                case "WindowsOneCore":
                    return "Windows-PC";
                default:
                    return devicetype;
            }
        }
    }
}
