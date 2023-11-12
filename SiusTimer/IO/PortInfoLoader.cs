using System.IO.Ports;
using System.Management;
using System.Windows.Forms.Design;

namespace SiusTimer.IO
{
    public static class PortInfoExt
    {
        public static PortInfo GetArduinoPort(this List<PortInfo> ports)
        {
            return ports.FirstOrDefault(p => p.Description?.Contains("Arduino") == true);
        }
    }

    class PortInfoLoader
    {
        public static List<PortInfo> GetPortInfo()
        {
            var ports = new List<PortInfo>();

            foreach (var port in SerialPort.GetPortNames())
            {
                ports.Add(new PortInfo { Name = port });
            }

            string query = "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0";

            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (var obj in searcher.Get().Cast<ManagementObject>())
                {
                    if (obj["Caption"] is string caption)
                    {
                        foreach (var info in ports)
                        {
                            if (caption.Contains(info.Name))
                            {
                                info.Description = caption;
                            }
                        }
                    }
                }
            }

            return ports;
        }
    }
}
