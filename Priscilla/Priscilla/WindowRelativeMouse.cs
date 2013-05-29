namespace Priscilla
{
    using System;
    using Priscilla.Native;
    using Priscilla.Utils;
    using Priscilla.Utils.Retry;

    public class WindowRelativeMouse
        : IMouse
    {
        private readonly IntPtr parenthWnd;
        private readonly IntPtr hWnd;
        private readonly IMouse absoluteMouse;
        private readonly INativeMethodWrapper nativeMethodWrapper;
        private readonly IRetrier retry;

        public WindowRelativeMouse(IntPtr parenthWnd, IntPtr hWnd, IMouse mouse)
            : this(parenthWnd, hWnd, mouse, new NativeMethodWrapper(), new Retrier(new RetryTimerFactory()))
        {
        }

        internal WindowRelativeMouse(IntPtr parenthWnd, IntPtr hWnd, IMouse mouse, INativeMethodWrapper nativeMethodWrapper, IRetrier retry)
        {
            this.parenthWnd = parenthWnd;
            this.hWnd = hWnd;
            this.absoluteMouse = mouse;
            this.nativeMethodWrapper = nativeMethodWrapper;
            this.retry = retry;
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
            this.PerformInWindowAction(this.absoluteMouse.LeftDown);
        }

        public void LeftUp()
        {
            this.PerformInWindowAction(this.absoluteMouse.LeftUp);
        }

        public void RightDown()
        {
            this.PerformInWindowAction(this.absoluteMouse.RightDown);
        }

        public void RightUp()
        {
            this.PerformInWindowAction(this.absoluteMouse.RightUp);
        }

        public void MiddleDown()
        {
            this.PerformInWindowAction(this.absoluteMouse.MiddleDown);
        }

        public void MiddleUp()
        {
            this.PerformInWindowAction(this.absoluteMouse.MiddleUp);
        }

        private void PerformInWindowAction(Action action)
        {
            //this.retry.DontDoUntil(action, this.AggressiveBringToFront);
            this.retry.DontDoUntil(action, this.NiceBringToFront);            
        }

        private bool AggressiveBringToFront()
        {
            const uint uFlags = SetWIndowPosFlags.SWP_NOMOVE | SetWIndowPosFlags.SWP_NOSIZE;
            var windowHandle = this.parenthWnd == IntPtr.Zero ? this.hWnd : this.parenthWnd;
            return this.nativeMethodWrapper.SetWindowPos(windowHandle, new IntPtr(-1), 0, 0, 0, 0, uFlags);
        }

        private bool NiceBringToFront()
        {
            var windowHandle = this.parenthWnd == IntPtr.Zero ? this.hWnd : this.parenthWnd;

            var foregroundhWnd = this.nativeMethodWrapper.GetForegroundWindow();
            if (foregroundhWnd == windowHandle)
            {
                return true;
            }

            this.nativeMethodWrapper.SetActiveWindow(windowHandle);
            return this.nativeMethodWrapper.SetForegroundWindow(windowHandle); 

        }
    }
}