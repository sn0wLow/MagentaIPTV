using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace MagentaIPTV
{
    public class WindowMaximizer
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public int dwFlags;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

        private static readonly int MONITOR_DEFAULTTONEAREST = 2;

        private enum WindowRestoringState
        {
            Default,    // Restored
            Restoring,
            RestoreMove,
        }

        public static void FixMaximizedWindowSize(Window win)
        {
            var windowHelper = new WindowInteropHelper(win);
            IntPtr hwnd = HwndSource.FromHwnd(windowHelper.Handle).Handle;


            win.WindowStyle = WindowStyle.None;

            var hMonitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            var monitorInfo = new MONITORINFO
            {
                cbSize = Marshal.SizeOf(typeof(MONITORINFO))
            };

            GetMonitorInfo(hMonitor, ref monitorInfo);
            var workingRectangle = monitorInfo.rcWork;

            var x = workingRectangle.left;
            var y = workingRectangle.top - workingRectangle.top;
            var width = Math.Abs(workingRectangle.right - workingRectangle.left);
            var height = Math.Abs(workingRectangle.bottom);
            //var height = Math.Abs(workingRectangle.bottom - workingRectangle.top);
            MoveWindow(hwnd, x, y, width, height, true);
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
    }
}
