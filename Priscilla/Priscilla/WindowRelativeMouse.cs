﻿namespace Priscilla
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

	    private static bool? _allowTopLeftPositionField;
	    public static bool AllowTopLeftPosition
	    {
		    get
		    {
			    return !_allowTopLeftPositionField.HasValue || _allowTopLeftPositionField.Value;
		    }
			set { _allowTopLeftPositionField = value; }
	    }

	    private static int? _assumeFixedWindowPositionThresholdField;
		public static int AssumeFixedWindowPositionThreshold
		{
			get
			{
				if (_assumeFixedWindowPositionThresholdField.HasValue)
				{
					return _assumeFixedWindowPositionThresholdField.Value;
				}

				return 1;
			}
			set { _assumeFixedWindowPositionThresholdField = value; }
		}
		public static bool AssumeFixedWindowPosition { get; set; }
		private static int AssumeFixedWindowPositionThresholdTries { get; set; }
	    
        private Coordinate windowOffsetField;
        private Coordinate WindowOffset
        {
            get
            {
				if (AssumeFixedWindowPosition && ++AssumeFixedWindowPositionThresholdTries > AssumeFixedWindowPositionThreshold && this.windowOffsetField != null)
                {
                    return this.windowOffsetField;
                }

                var screenCoordinate = new CursorCoordinate();
				this.nativeMethodWrapper.ClientToScreen(this.hWnd, ref screenCoordinate);
				this.retry.DoUntil(
					() => this.nativeMethodWrapper.ClientToScreen(this.hWnd, ref screenCoordinate),
					() =>
						{
							var isTopLeftPosition = (screenCoordinate.X == 0 && screenCoordinate.Y == 0);

							if (!AllowTopLeftPosition && isTopLeftPosition)
							{
								Logger.Debug("Window is in top left corner when not allowing top left position with relative mouse");
								return false;
							}

							return true;
						});
                Logger.Debug("Using window offset: " + (new Coordinate(0, 0) + screenCoordinate));
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