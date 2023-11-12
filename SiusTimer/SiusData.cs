using System.Diagnostics;

namespace SiusTimer
{
    public class SiusData
    {
        private IntPtr StartButton { get; set; }
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

            SetButton = WinAPI.FindWindowEx(dialog, IntPtr.Zero, "Button", "Set");
            StartButton = WinAPI.FindWindowEx(dialog, IntPtr.Zero, "Button", "Start");
            ResetButton = WinAPI.FindWindowEx(dialog, IntPtr.Zero, "Button", "Reset");

            return true;
        }

        internal void Set(TimeSpan duration)
        {
            WinAPI.SendMessage(SetButton, WinAPI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        internal void Reset()
        {
            WinAPI.SendMessage(ResetButton, WinAPI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        internal void Start()
        {
            WinAPI.SendMessage(StartButton, WinAPI.BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
