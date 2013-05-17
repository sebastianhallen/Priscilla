﻿namespace Priscilla
{
    using System;
    using System.Runtime.InteropServices;

    public class WindowRelativeMouse
        : IMouse
    {
        private readonly IntPtr hWnd;
        private readonly Mouse absoluteMouse;

        public WindowRelativeMouse(IntPtr hWnd)
        {
            this.hWnd = hWnd;
            this.absoluteMouse = new Mouse();
        }


        public void PositionCursor(Coordinate coordinate)
        {
            var screenCoordinate = new CursorCoordinate();
            NativeMethods.ClientToScreen(this.hWnd, ref screenCoordinate);

            this.absoluteMouse.PositionCursor(coordinate + screenCoordinate);
        }

        public Coordinate FindCursor()
        {            
            var screenCoordinate = new CursorCoordinate();
            NativeMethods.ClientToScreen(this.hWnd, ref screenCoordinate);

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