namespace Priscilla
{
    using System;
    using Priscilla.Native;
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

        public static bool AssumeFixedWindowPosition { get; set; }

        private Coordinate windowOffsetField;
        private Coordinate WindowOffset
        {
            get
            {
                if (AssumeFixedWindowPosition && this.windowOffsetField != null)
                {
                    return this.windowOffsetField;
                }

                var screenCoordinate = new CursorCoordinate();
                this.nativeMethodWrapper.ClientToScreen(this.hWnd, ref screenCoordinate);

                return this.windowOffsetField = screenCoordinate;
            }
        }

        public void PositionCursor(Coordinate coordinate)
        {
            this.absoluteMouse.PositionCursor(coordinate + this.WindowOffset);
        }

        public Coordinate FindCursor()
        {   
            var position = this.absoluteMouse.FindCursor();
            return position - this.WindowOffset;
        }

        public void MoveCursor(int dx, int dy)
        {
            this.absoluteMouse.MoveCursor(dx, dy);
        }

        public void LeftDown()
        {
            this.PerformInWindowAction(this.absoluteMouse.LeftDown);
        }

        public void LeftDown(Coordinate coordinate)
        {
            this.PerformInWindowAction(() => this.absoluteMouse.LeftDown(coordinate + this.WindowOffset));
        }

        public void LeftUp()
        {
            this.PerformInWindowAction(this.absoluteMouse.LeftUp);
        }

        public void LeftUp(Coordinate coordinate)
        {
            this.PerformInWindowAction(() => this.absoluteMouse.LeftUp(coordinate + this.WindowOffset));
        }

        public void RightDown()
        {
            this.PerformInWindowAction(this.absoluteMouse.RightDown);
        }

        public void RightDown(Coordinate coordinate)
        {
            this.PerformInWindowAction(() => this.absoluteMouse.RightDown(coordinate + this.WindowOffset));
        }

        public void RightUp()
        {
            this.PerformInWindowAction(this.absoluteMouse.RightUp);
        }

        public void RightUp(Coordinate coordinate)
        {
            this.PerformInWindowAction(() => this.absoluteMouse.RightUp(coordinate + this.WindowOffset));
        }

        public void MiddleDown()
        {
            this.PerformInWindowAction(this.absoluteMouse.MiddleDown);
        }

        public void MiddleDown(Coordinate coordinate)
        {
            this.PerformInWindowAction(() => this.absoluteMouse.MiddleDown(coordinate + this.WindowOffset));
        }

        public void MiddleUp()
        {
            this.PerformInWindowAction(this.absoluteMouse.MiddleUp);
        }

        public void MiddleUp(Coordinate coordinate)
        {
            this.PerformInWindowAction(() => this.absoluteMouse.MiddleUp(coordinate + this.WindowOffset));
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