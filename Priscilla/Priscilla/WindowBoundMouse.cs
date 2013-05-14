namespace Priscilla
{
    using System;

    public class WindowBoundMouse
        : IMouse
    {
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
            throw new NotImplementedException();
        }

        public void LeftDown()
        {
            throw new NotImplementedException();
        }

        public void LeftUp()
        {
            throw new NotImplementedException();
        }

        public void RightDown()
        {
            throw new NotImplementedException();
        }

        public void RightUp()
        {
            throw new NotImplementedException();
        }

        public void MiddleDown()
        {
            throw new NotImplementedException();
        }

        public void MiddleUp()
        {
            throw new NotImplementedException();
        }

        private static class MouseInput
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/ff468877(v=vs.85).aspx
            public const uint WM_CAPTURECHANGED  = 0x0215;   //sent to window that lost mouse capture
            public const uint WM_LBUTTONDBLCLK   = 0x0203;   //double click
            public const uint WM_LBUTTONDOWN     = 0x0201;   //left button down
            public const uint WM_LBUTTONUP       = 0x0202;   //left button up
            public const uint WM_MBUTTONDBLCLK   = 0x0209;   //middle button double click
            public const uint WM_MBUTTONDOWN     = 0x0207;   //middle button down
            public const uint WM_MBUTTONUP       = 0x0208;   //middle button up
            public const uint WM_MOUSEACTIVATE   = 0x0021;   //active inactive window with mouse
            public const uint WM_MOUSEHOVER      = 0x02A1;   //hover
            public const uint WM_MOUSEHWHEEL     = 0x0205;   //horizontal wheel scrolled
            public const uint WM_MOUSELEAVE      = 0x02A3;   //mouse left client area
            public const uint WM_MOUSEMOVE       = 0x0200;   //cursor moves
            public const uint WM_MOUSEWHEEL      = 0x020A;   //mouse wheel rotated
            public const uint WM_NCHITTEST       = 0x0084;   //sent to window check which part of a window is under the mouse
            public const uint WM_NCLBUTTONDBLCLK = 0x00A3;   //left mouse button double clicked with non-client area
            public const uint WM_NCLBUTTONDOWN   = 0x00A1;   //left button down with non-client area
            public const uint WM_NCLBUTTONUP     = 0x00A2;   //left button up with non-client area
            public const uint WM_NCMBUTTONDBLCLK = 0x00A9;   //middle button double clicked with non-client area
            public const uint WM_NCMBUTTONDOWN   = 0x00A7;   //middle button down with non-client area
            public const uint WM_NCMBUTTONUP     = 0x00A8;   //middle button up with non-client area
            public const uint WM_NCMOUSEHOVER    = 0x02A0;   //hover with non-client area
            public const uint WM_NCMOUSELEAVE    = 0x02A2;   //cursor leaves non-client area
            public const uint WM_NCMOUSEMOVE     = 0x00A0;   //cursor moved within non-client area
            public const uint WM_NCRBUTTONDBLCLK = 0x00A6;   //right button double clicked within non-client area
            public const uint WM_NCRBUTTONDOWN   = 0x00A4;   //right button down within non-client area
            public const uint WM_NCRBUTTONUP     = 0x00A5;   //right button up within non-client area
            public const uint WM_NCXBUTTONDBLCLK = 0x00AD;   //x button double clicked within non-client area
            public const uint WM_NCXBUTTONDOWN   = 0x00AB;   //x button down within non-client area
            public const uint WM_NCXBUTTONUP     = 0x00AC;   //x button up within non-client area
            public const uint WM_RBUTTONDBLCLK   = 0x0206;   //right button double clicked
            public const uint WM_RBUTTONDOWN     = 0x0204;   //right button down
            public const uint WM_RBUTTONUP       = 0x0205;   //right button up
            public const uint WM_XBUTTONDBLCLK   = 0x020D;   //x button double click
            public const uint WM_XBUTTONDOWN     = 0x020B;   //x button down
            public const uint WM_XBUTTONUP       = 0x020C;   //x button up
        }
    }
}