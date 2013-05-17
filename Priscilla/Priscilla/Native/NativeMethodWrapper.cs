namespace Priscilla.Native
{
    using System;

    internal class NativeMethodWrapper
        : INativeMethodWrapper
    {
        public IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow)
        {
            return NativeMethods.FindWindowEx(hwndParent, hwndChildAfter, lpszClass, lpszWindow);
        }

        public bool SetForegroundWindow(IntPtr hWnd)
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }

        public IntPtr GetForegroundWindow()
        {
            return NativeMethods.GetForegroundWindow();
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
    }
}