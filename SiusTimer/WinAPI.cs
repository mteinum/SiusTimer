using System.Runtime.InteropServices;

namespace SiusTimer
{
    public static class WinAPI
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(
            IntPtr parentHandle,
            IntPtr childAfter,
            string lclassName,
            string windowTitle
        );

        public const int BM_CLICK = 0x00F5;

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
    }
}
