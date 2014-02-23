namespace Priscilla
{
    using System;
    using Priscilla.Native;

    public class Mouse
        : IMouse
    {
        private readonly INativeMethodWrapper nativeMethodWrapper;
        private int? screenWidth;
        private int? screenHeight;

        public Mouse()
            : this(new NativeMethodWrapper())
        {
        }

        internal Mouse(INativeMethodWrapper nativeMethodWrapper)
        {
            this.nativeMethodWrapper = nativeMethodWrapper;
        }

        private int ScreenHeight
        {
            get
            {
                if (!this.screenHeight.HasValue)
                {
                    this.screenHeight = this.nativeMethodWrapper.GetSystemMetrics(SystemMetric.PrimaryScreenHeight);
                    Logger.Debug("Using screen height = " + this.screenHeight + "px");
                }
                return screenHeight.Value;
            }
        }

        private int ScreenWidth
        {
            get
            {
                if (!this.screenWidth.HasValue)
                {
                    this.screenWidth = this.nativeMethodWrapper.GetSystemMetrics(SystemMetric.PrimaryScreenWidth);
                    Logger.Debug("Using screen width = " + this.screenWidth + "px");
                }
                return screenWidth.Value;
            }
        }

        public void PositionCursor(Coordinate coordinate)
        {
            Logger.Debug("Positioning cursor @ " + coordinate);
            this.SendMouseInput(input =>
                {
                    input.Flags = MouseInputFlags.Absolute | MouseInputFlags.Move;
                    input.X = Convert.ToInt32(Math.Ceiling(coordinate.X * 65535.0 / this.ScreenWidth)) + 1;
                    input.Y = Convert.ToInt32(Math.Ceiling(coordinate.Y * 65535.0 / this.ScreenHeight)) + 1;
                    return input;
                });
        }

        public Coordinate FindCursor()
        {
            CursorCoordinate position;
            if (this.nativeMethodWrapper.GetCursorPos(out position))
            {
                Logger.Debug("Found cursor @ " + (new Coordinate(0, 0) + position));
                return position;
            }
            throw new Exception("unable to get cursor position");
        }

        public void MoveCursor(int dx, int dy)
        {
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.Move;
                input.X = dx;
                input.Y = dy;
                return input;
            });
        }

        public void LeftDown()
        {
            Logger.Debug("LeftDown @ current cursor position");
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.LeftDown;
                return input;
            });
        }

        public void LeftDown(Coordinate coordinate)
        {
            Logger.Debug("LeftDown @ " + coordinate);
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.LeftDown;
                input.X = Convert.ToInt32(Math.Ceiling(coordinate.X * 65535.0 / this.ScreenWidth)) + 1;
                input.Y = Convert.ToInt32(Math.Ceiling(coordinate.Y * 65535.0 / this.ScreenHeight)) + 1;
                return input;
            });
        }

        public void LeftUp()
        {
            Logger.Debug("LeftUp @ current cursor position");
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.LeftUp;
                return input;
            });
        }

        public void LeftUp(Coordinate coordinate)
        {
            Logger.Debug("LeftUp @ " + coordinate);
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.LeftUp;
                input.X = Convert.ToInt32(Math.Ceiling(coordinate.X * 65535.0 / this.ScreenWidth)) + 1;
                input.Y = Convert.ToInt32(Math.Ceiling(coordinate.Y * 65535.0 / this.ScreenHeight)) + 1;
                return input;
            });
        }

        public void RightDown()
        {
            Logger.Debug("LeftDown @ current cursor position");
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.RightDown;
                return input;
            });
        }

        public void RightDown(Coordinate coordinate)
        {
            Logger.Debug("RightDown @ " + coordinate);
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.RightDown;
                input.X = Convert.ToInt32(Math.Ceiling(coordinate.X * 65535.0 / this.ScreenWidth)) + 1;
                input.Y = Convert.ToInt32(Math.Ceiling(coordinate.Y * 65535.0 / this.ScreenHeight)) + 1;
                return input;
            });

        }

        public void RightUp()
        {
            Logger.Debug("RightUp @ current cursor position");
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.RightUp;
                return input;
            });
        }

        public void RightUp(Coordinate coordinate)
        {
            Logger.Debug("RightUp @ " + coordinate);
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.RightUp;
                input.X = Convert.ToInt32(Math.Ceiling(coordinate.X * 65535.0 / this.ScreenWidth)) + 1;
                input.Y = Convert.ToInt32(Math.Ceiling(coordinate.Y * 65535.0 / this.ScreenHeight)) + 1;
                return input;
            });

        }

        public void MiddleDown()
        {
            Logger.Debug("MiddleDown @ current cursor position");
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.MiddleDown;
                return input;
            });
        }

        public void MiddleDown(Coordinate coordinate)
        {
            Logger.Debug("MiddleDown @ " + coordinate);
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.MiddleDown;
                input.X = Convert.ToInt32(Math.Ceiling(coordinate.X * 65535.0 / this.ScreenWidth)) + 1;
                input.Y = Convert.ToInt32(Math.Ceiling(coordinate.Y * 65535.0 / this.ScreenHeight)) + 1;
                return input;
            });

        }

        public void MiddleUp()
        {
            Logger.Debug("MiddleUp @ current cursor position");
            this.SendMouseInput(input =>
                {
                    input.Flags = MouseInputFlags.MiddleUp;
                    return input;
                });
        }

        public void MiddleUp(Coordinate coordinate)
        {
            Logger.Debug("MiddleUp @ " + coordinate);
            this.SendMouseInput(input =>
            {
                input.Flags = MouseInputFlags.Absolute | MouseInputFlags.Move | MouseInputFlags.MiddleUp;
                input.X = Convert.ToInt32(Math.Ceiling(coordinate.X * 65535.0 / this.ScreenWidth)) + 1;
                input.Y = Convert.ToInt32(Math.Ceiling(coordinate.Y * 65535.0 / this.ScreenHeight)) + 1;
                return input;
            });

        }

        private void SendMouseInput(Func<MouseInput, MouseInput> configure)
        {
            var input = new Input { Type = InputType.Mouse };
            input.Data.Mouse = configure(input.Data.Mouse);

            this.nativeMethodWrapper.SendInput(new[]
                {
                    input
                });
        }
    }
}