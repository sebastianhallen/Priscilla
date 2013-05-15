namespace Priscilla
{
    using System;
    using System.Runtime.InteropServices;


    /// <summary>
    /// use absolute coordinates within a window
    /// </summary>
    public class WindowBoundMouse
        : IMouse
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private readonly IntPtr hWnd;

        public WindowBoundMouse(IntPtr hWnd)
        {
            this.hWnd = hWnd;
        }

        public void PositionCursor(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }

        public Coordinate FindCursor()
        {
            throw new NotImplementedException();
        }

        public void MoveCursor(int dx, int dy)
        {
            SendMessage(this.hWnd, MouseInput.Move, IntPtr.Zero, new IntPtr(dy << 16 | dx));
        }

        public void LeftDown()
        {
            SendMessage(this.hWnd, MouseInput.LeftDown, MouseButton.Left, IntPtr.Zero);
        }

        public void LeftUp()
        {
            SendMessage(this.hWnd, MouseInput.LeftUp, MouseButton.Left, IntPtr.Zero);
        }

        public void RightDown()
        {
            SendMessage(this.hWnd, MouseInput.RightDown, MouseButton.Right, IntPtr.Zero);
        }

        public void RightUp()
        {
            SendMessage(this.hWnd, MouseInput.RightUp, MouseButton.Right, IntPtr.Zero);
        }

        public void MiddleDown()
        {
            SendMessage(this.hWnd, MouseInput.MiddleDown, MouseButton.Middle, IntPtr.Zero);
        }

        public void MiddleUp()
        {
            SendMessage(this.hWnd, MouseInput.MiddleUp, MouseButton.Middle, IntPtr.Zero);
        }

        private static class MouseButton
        {
            public static readonly IntPtr Left = new IntPtr(MK_LBUTTON);
            public static readonly IntPtr Middle = new IntPtr(MK_MBUTTON);
            public static readonly IntPtr Right = new IntPtr(MK_RBUTTON);

            private const int MK_LBUTTON = 0x0001;
            private const int MK_MBUTTON = 0x0010;
            private const int MK_RBUTTON = 0x0002;
        }

        private static class MouseInput
        {
            public const int LeftDown = WM_LBUTTONDOWN;
            public const int LeftUp = WM_LBUTTONUP;
            public const int MiddleDown = WM_MBUTTONDOWN;
            public const int MiddleUp = WM_MBUTTONUP;
            public const int Move = WM_MOUSEMOVE;
            public const int RightDown = WM_RBUTTONDOWN;
            public const int RightUp = WM_RBUTTONUP;

            //http://msdn.microsoft.com/en-us/library/windows/desktop/ff468877(v=vs.85).aspx
            private const int WM_CAPTURECHANGED  = 0x0215;   //sent to window that lost mouse capture
            private const int WM_LBUTTONDBLCLK   = 0x0203;   //double click
            private const int WM_LBUTTONDOWN     = 0x0201;   //left button down
            private const int WM_LBUTTONUP       = 0x0202;   //left button up
            private const int WM_MBUTTONDBLCLK   = 0x0209;   //middle button double click
            private const int WM_MBUTTONDOWN     = 0x0207;   //middle button down
            private const int WM_MBUTTONUP       = 0x0208;   //middle button up
            private const int WM_MOUSEACTIVATE   = 0x0021;   //active inactive window with mouse
            private const int WM_MOUSEHOVER      = 0x02A1;   //hover
            private const int WM_MOUSEHWHEEL     = 0x0205;   //horizontal wheel scrolled
            private const int WM_MOUSELEAVE      = 0x02A3;   //mouse left client area
            private const int WM_MOUSEMOVE       = 0x0200;   //cursor moves
            private const int WM_MOUSEWHEEL      = 0x020A;   //mouse wheel rotated
            private const int WM_NCHITTEST       = 0x0084;   //sent to window check which part of a window is under the mouse
            private const int WM_NCLBUTTONDBLCLK = 0x00A3;   //left mouse button double clicked with non-client area
            private const int WM_NCLBUTTONDOWN   = 0x00A1;   //left button down with non-client area
            private const int WM_NCLBUTTONUP     = 0x00A2;   //left button up with non-client area
            private const int WM_NCMBUTTONDBLCLK = 0x00A9;   //middle button double clicked with non-client area
            private const int WM_NCMBUTTONDOWN   = 0x00A7;   //middle button down with non-client area
            private const int WM_NCMBUTTONUP     = 0x00A8;   //middle button up with non-client area
            private const int WM_NCMOUSEHOVER    = 0x02A0;   //hover with non-client area
            private const int WM_NCMOUSELEAVE    = 0x02A2;   //cursor leaves non-client area
            private const int WM_NCMOUSEMOVE     = 0x00A0;   //cursor moved within non-client area
            private const int WM_NCRBUTTONDBLCLK = 0x00A6;   //right button double clicked within non-client area
            private const int WM_NCRBUTTONDOWN   = 0x00A4;   //right button down within non-client area
            private const int WM_NCRBUTTONUP     = 0x00A5;   //right button up within non-client area
            private const int WM_NCXBUTTONDBLCLK = 0x00AD;   //x button double clicked within non-client area
            private const int WM_NCXBUTTONDOWN   = 0x00AB;   //x button down within non-client area
            private const int WM_NCXBUTTONUP     = 0x00AC;   //x button up within non-client area
            private const int WM_RBUTTONDBLCLK   = 0x0206;   //right button double clicked
            private const int WM_RBUTTONDOWN     = 0x0204;   //right button down
            private const int WM_RBUTTONUP       = 0x0205;   //right button up
            private const int WM_XBUTTONDBLCLK   = 0x020D;   //x button double click
            private const int WM_XBUTTONDOWN     = 0x020B;   //x button down
            private const int WM_XBUTTONUP       = 0x020C;   //x button up
        }
    }
}