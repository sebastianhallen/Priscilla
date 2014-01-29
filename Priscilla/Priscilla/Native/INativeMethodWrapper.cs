namespace Priscilla.Native
{
    using System;
    using System.Text;

    internal interface INativeMethodWrapper
    {
        IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        /// <summary>
        /// Enumerate over window handles until the desired window handle is found.
        /// Note that this signature does not match the corresponding WinAPI call with the same name.
        /// </summary>
        /// <param name="hWndParent">Parent window - use IntPtr.Zero for top level windows</param>
        /// <param name="lpEnumFunc">Function that checks if the current window handle (IntPtr argument) is the desired one. Should return false when desired window is found, should return true to keep on enumerating over window handles.</param>
        /// <returns></returns>
        bool EnumChildWindows(IntPtr hWndParent, Func<IntPtr, bool> lpEnumFunc);
        
        int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

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
