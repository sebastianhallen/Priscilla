namespace Priscilla
{
    using System;
    using System.Runtime.InteropServices;

    public class Mouse
        : IMouse
    {
        public void PositionCursor(Coordinate coordinate)
        {
            SetCursorPos(coordinate.X, coordinate.Y);
        }

        public Coordinate FindCursor()
        {
            CursorCoordinate position;
            if (GetCursorPos(out position))
            {
                return position;
            }
            throw new Exception("unable to get cursor position");
        }

        public void MoveCursor(int dx, int dy)
        {
            mouse_event(MouseInput.Move, (uint)dy, (uint)dx, 0, new IntPtr());
        }

        public void LeftDown()
        {
            mouse_event(MouseInput.LeftDown, 0, 0, 0, new IntPtr());
        }

        public void LeftUp()
        {
            mouse_event(MouseInput.LeftUp, 0, 0, 0, new IntPtr());
        }

        public void RightDown()
        {
            mouse_event(MouseInput.RightDown, 0, 0, 0, new IntPtr());
        }

        public void RightUp()
        {
            mouse_event(MouseInput.RightUp, 0, 0, 0, new IntPtr());
        }

        public void MiddleDown()
        {
            mouse_event(MouseInput.MiddleDown, 0, 0, 0, new IntPtr());
        }

        public void MiddleUp()
        {
            mouse_event(MouseInput.MiddleUp, 0, 0, 0, new IntPtr());
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dy, UInt32 dx, UInt32 dwData, IntPtr dwExtraInfo);
       
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out CursorCoordinate point);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        private static class MouseInput
        {
            public const uint Absolute = 0x8000;        //The dx and dy parameters contain normalized absolute coordinates. If not set, those parameters contain relative data
            public const uint VirtualDesktop = 0x4000;  //Maps coordinates to the entire desktop. Must be used with MouseInput.Absolute. 
            public const uint LeftDown = 0x0002;        //The left button is down.
            public const uint LeftUp = 0x0004;          //The left button is up.
            public const uint MiddleDown = 0x0020;      //The middle button is down.
            public const uint MiddleUp = 0x0040;        //The middle button is up.
            public const uint Move = 0x0001;            //Movement occurred.
            public const uint RightDown = 0x0008;       //The right button is down.
            public const uint RightUp = 0x0010;         //The right button is up.
            public const uint Wheel = 0x0800;           //The wheel has been moved, if the mouse has a wheel. The amount of movement is specified in dwData
            public const uint XDown = 0x0080;           //An X button was pressed.
            public const uint XUp = 0x0100;             //An X button was released.
            public const uint HWheel = 0x1000;          //The wheel button is tilted.
        }
    }
}