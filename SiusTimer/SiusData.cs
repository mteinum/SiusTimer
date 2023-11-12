using System.Diagnostics;

namespace SiusTimer
{
    public class SiusData
    {
        private IntPtr SetButton { get; set; }
        private IntPtr ResetButton { get; set; }

        private Process SiusDataProcess { get; set; }

        public string MainWindowTitle => SiusDataProcess.MainWindowTitle;

        public bool Initialize()
        {
            SiusDataProcess = Process.GetProcessesByName("SiusData").FirstOrDefault();

            if (SiusDataProcess == null)
            {
                return false;
            }

            // find #32770 (Dialog)
            var dialog = WinAPI.FindWindowEx(
                SiusDataProcess.MainWindowHandle,
                IntPtr.Zero,
                "#32770",
                String.Empty
            );

            SetButton = WinAPI.FindWindowEx(dialog, IntPtr.Zero, "Button", "Start");
            ResetButton = WinAPI.FindWindowEx(dialog, IntPtr.Zero, "Button", "Reset");

            return true;
        }

        internal void Start()
        {
            //
        }
    }
}
