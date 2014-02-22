﻿namespace Priscilla
{
    using System;
    using Priscilla.Native;

    public class Mouse
        : IMouse
    {
        private readonly INativeMethodWrapper nativeMethodWrapper;

        public Mouse()
            : this(new NativeMethodWrapper())
        {
        }

        internal Mouse(INativeMethodWrapper nativeMethodWrapper)
        {
            this.nativeMethodWrapper = nativeMethodWrapper;
        }

        public void PositionCursor(Coordinate coordinate)
        {
            this.nativeMethodWrapper.SetCursorPos(coordinate.X, coordinate.Y);
        }

        public Coordinate FindCursor()
        {
            CursorCoordinate position;
            if (this.nativeMethodWrapper.GetCursorPos(out position))
            {
                return position;
            }
            throw new Exception("unable to get cursor position");
        }

        public void MoveCursor(int dx, int dy)
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.Move, (uint)dy, (uint)dx, 0, new IntPtr());
        }

        public void LeftDown()
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.LeftDown, 0, 0, 0, new IntPtr());
        }

        public void LeftDown(Coordinate coordinate)
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.LeftDown | MouseInput.Absolute, (uint) coordinate.Y, (uint) coordinate.X, 0, new IntPtr());
        }

        public void LeftUp()
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.LeftUp, 0, 0, 0, new IntPtr());
        }

        public void LeftUp(Coordinate coordinate)
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.LeftUp | MouseInput.Absolute, (uint)coordinate.Y, (uint)coordinate.X, 0, new IntPtr());
        }

        public void RightDown()
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.RightDown, 0, 0, 0, new IntPtr());
        }

        public void RightDown(Coordinate coordinate)
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.RightDown | MouseInput.Absolute, (uint)coordinate.Y, (uint)coordinate.X, 0, new IntPtr());
        }

        public void RightUp()
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.RightUp, 0, 0, 0, new IntPtr());
        }

        public void RightUp(Coordinate coordinate)
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.RightUp | MouseInput.Absolute, (uint)coordinate.Y, (uint)coordinate.X, 0, new IntPtr());
        }

        public void MiddleDown()
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.MiddleDown, 0, 0, 0, new IntPtr());
        }

        public void MiddleDown(Coordinate coordinate)
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.MiddleDown | MouseInput.Absolute, (uint)coordinate.Y, (uint)coordinate.X, 0, new IntPtr());
        }

        public void MiddleUp()
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.MiddleUp, 0, 0, 0, new IntPtr());
        }

        public void MiddleUp(Coordinate coordinate)
        {
            this.nativeMethodWrapper.mouse_event(MouseInput.MiddleUp | MouseInput.Absolute, (uint)coordinate.Y, (uint)coordinate.X, 0, new IntPtr());
        }

        internal static class MouseInput
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