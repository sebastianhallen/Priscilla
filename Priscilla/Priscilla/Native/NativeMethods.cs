namespace Priscilla.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal static class NativeMethods
    {
        public delegate bool EnumWindowsProc(IntPtr hWnd, ref IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, ref IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, ref IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref CursorCoordinate lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern void mouse_event(UInt32 dwFlags, UInt32 dy, UInt32 dx, UInt32 dwData, IntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out CursorCoordinate point);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Area lpRect);
    }
}