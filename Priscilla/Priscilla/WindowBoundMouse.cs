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
    }
}