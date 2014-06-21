namespace Priscilla
{
    using System;
    using Priscilla.Native;
    using Priscilla.Utils.Retry;

    public class WindowRelativeMouse
        : IMouse
    {
	    public class Settings
	    {
		    public Settings()
		    {
			    this.AllowTopLeftPosition = true;
			    this.AssumeFixedWindowPosition = false;
			    this.AssumeFixedWindowPositionThreshold = 1;
		    }

			public bool AllowTopLeftPosition { get; set; }
			public int AssumeFixedWindowPositionThreshold { get; set; }
			public bool AssumeFixedWindowPosition { get; set; }
	    }

	    private readonly IntPtr parenthWnd;
        private readonly IntPtr hWnd;
        private readonly IMouse absoluteMouse;
        private readonly INativeMethodWrapper nativeMethodWrapper;
        private readonly IRetrier retry;
	    private readonly Settings settings;
		private int assumeFixedWindowPositionThresholdTries;
	    
	    public WindowRelativeMouse(IntPtr parenthWnd, IntPtr hWnd, IMouse mouse, Settings settings = null)
            : this(parenthWnd, hWnd, mouse, new NativeMethodWrapper(), new Retrier(new RetryTimerFactory()), settings)
        {
        }

		internal WindowRelativeMouse(IntPtr parenthWnd, IntPtr hWnd, IMouse mouse, INativeMethodWrapper nativeMethodWrapper, IRetrier retry, Settings settings = null)
        {
            this.parenthWnd = parenthWnd;
            this.hWnd = hWnd;
            this.absoluteMouse = mouse;
            this.nativeMethodWrapper = nativeMethodWrapper;
            this.retry = retry;
			this.settings = settings ?? new Settings();
        }

        private Coordinate windowOffsetField;
        private Coordinate WindowOffset
        {
            get
            {
				if (this.settings.AssumeFixedWindowPosition)
                {
                    if (assumeFixedWindowPositionThresholdTries >= this.settings.AssumeFixedWindowPositionThreshold && this.windowOffsetField != null)
                    {
                        return this.windowOffsetField;
                    }
                    ++assumeFixedWindowPositionThresholdTries;
                }
            
                var screenCoordinate = new CursorCoordinate();
                var hasAllowedPosition = false;

                this.nativeMethodWrapper.ClientToScreen(this.hWnd, ref screenCoordinate);
				this.retry.DoUntil(
					() => this.nativeMethodWrapper.ClientToScreen(this.hWnd, ref screenCoordinate),
					() =>
						{
						    var isTopLeftPosition = (screenCoordinate.X == 0 && screenCoordinate.Y == 0);

							if (!this.settings.AllowTopLeftPosition && isTopLeftPosition)
							{
								return false;
							}
						    hasAllowedPosition = true;
							return true;
						});
                if (!hasAllowedPosition)
                {
                    Logger.Error("Window for handle '" + this.hWnd + "' was either: positioned in top left corner, not displayed, or didn't exist");
                }
                else
                {
                    this.windowOffsetField = screenCoordinate;
                }

                Logger.Info("Using window offset: " + (new Coordinate(0, 0) + screenCoordinate));
                return screenCoordinate;
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
            this.retry.DontDoUntil(() => { }, this.NiceBringToFront);
            action();
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