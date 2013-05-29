namespace Priscilla.Native
{
    using System;

    internal interface INativeMethodWrapper
    {
        IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        bool SetForegroundWindow(IntPtr hWnd);
        IntPtr GetForegroundWindow();
        IntPtr SetActiveWindow(IntPtr hWnd);
        bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        bool ClientToScreen(IntPtr hWnd, ref CursorCoordinate lpPoint);

        IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        void mouse_event(UInt32 dwFlags, UInt32 dy, UInt32 dx, UInt32 dwData, IntPtr dwExtraInfo);

        bool GetCursorPos(out CursorCoordinate point);
        bool SetCursorPos(int x, int y);
    }
}
