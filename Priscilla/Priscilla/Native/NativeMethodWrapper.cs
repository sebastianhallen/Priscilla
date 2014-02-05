namespace Priscilla.Native
{
    using System;
    using System.Text;

    internal class NativeMethodWrapper
        : INativeMethodWrapper
    {
        public IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow)
        {
            return NativeMethods.FindWindowEx(hwndParent, hwndChildAfter, lpszClass, lpszWindow);
        }

        public bool EnumChildWindows(IntPtr hWndParent, Func<IntPtr, bool> lpEnumFunc)
        {
            var enumWindowsProcWrapper = new NativeMethods.EnumWindowsProc((IntPtr wnd, ref IntPtr _) => lpEnumFunc(wnd));
            var lParam = IntPtr.Zero;
            return NativeMethods.EnumChildWindows(hWndParent, enumWindowsProcWrapper, ref lParam);
        }

        public int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount)
        {
            return NativeMethods.GetClassName(hWnd, lpClassName, nMaxCount);
        }

        public int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount)
        {
            return NativeMethods.GetWindowText(hWnd, lpString, nMaxCount);
        }

        public bool SetForegroundWindow(IntPtr hWnd)
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }

        public IntPtr GetForegroundWindow()
        {
            return NativeMethods.GetForegroundWindow();
        }

        public IntPtr SetActiveWindow(IntPtr hWnd)
        {
            return NativeMethods.SetActiveWindow(hWnd);
        }

        public bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags)
        {
            return NativeMethods.SetWindowPos(hWnd, hWndInsertAfter, x, y, cx, cy, uFlags);
        }

        public bool ClientToScreen(IntPtr hWnd, ref CursorCoordinate lpPoint)
        {
            return NativeMethods.ClientToScreen(hWnd, ref lpPoint);
        }

        public IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.SendMessage(hWnd, msg, wParam, lParam);
        }

        public void mouse_event(uint dwFlags, uint dy, uint dx, uint dwData, IntPtr dwExtraInfo)
        {
            NativeMethods.mouse_event(dwFlags, dy, dx, dwData, dwExtraInfo);
        }

        public bool GetCursorPos(out CursorCoordinate point)
        {
            return NativeMethods.GetCursorPos(out point);
        }

        public bool SetCursorPos(int x, int y)
        {
            return NativeMethods.SetCursorPos(x, y);
        }

        public bool GetClientRect(IntPtr hWnd, out System.Drawing.Rectangle clientRect)
        {
            Area area;
            var result = NativeMethods.GetClientRect(hWnd, out area);

            clientRect = area;
            return result;
        }
    }
}