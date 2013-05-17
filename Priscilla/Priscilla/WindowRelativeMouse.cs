namespace Priscilla
{
    using System;
    using Priscilla.Native;

    public class WindowRelativeMouse
        : IMouse
    {
        private readonly IntPtr hWnd;
        private readonly IMouse absoluteMouse;
        private readonly INativeMethodWrapper nativeMethodWrapper;

        public WindowRelativeMouse(IntPtr hWnd, IMouse mouse)
            : this(hWnd, mouse, new NativeMethodWrapper())
        {
        }

        internal WindowRelativeMouse(IntPtr hWnd, IMouse mouse, INativeMethodWrapper nativeMethodWrapper)
        {
            this.hWnd = hWnd;
            this.absoluteMouse = mouse;
            this.nativeMethodWrapper = nativeMethodWrapper;
        }

        public void PositionCursor(Coordinate coordinate)
        {
            var screenCoordinate = new CursorCoordinate();
            this.nativeMethodWrapper.ClientToScreen(this.hWnd, ref screenCoordinate);

            this.absoluteMouse.PositionCursor(coordinate + screenCoordinate);
        }

        public Coordinate FindCursor()
        {            
            var screenCoordinate = new CursorCoordinate();
            this.nativeMethodWrapper.ClientToScreen(this.hWnd, ref screenCoordinate);

            var position = this.absoluteMouse.FindCursor();
            return position - screenCoordinate;
        }

        public void MoveCursor(int dx, int dy)
        {
            this.absoluteMouse.MoveCursor(dx, dy);
        }

        public void LeftDown()
        {
            this.absoluteMouse.LeftDown();
        }

        public void LeftUp()
        {
            this.absoluteMouse.LeftUp();
        }

        public void RightDown()
        {
            this.absoluteMouse.RightDown();
        }

        public void RightUp()
        {
            this.absoluteMouse.RightUp();
        }

        public void MiddleDown()
        {
            this.absoluteMouse.MiddleDown();
        }

        public void MiddleUp()
        {
            this.absoluteMouse.MiddleUp();
        }

    }
}