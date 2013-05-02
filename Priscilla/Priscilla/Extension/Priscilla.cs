namespace Priscilla.Extension
{
    using System;
    using Priscilla;

    public static class ClickExtensions
    {
        public static void LeftClick(this IMouse mouse)
        {
            mouse.LeftDown();
            mouse.LeftUp();
        }

        public static void RightClick(this IMouse mouse)
        {
            mouse.RightDown();
            mouse.RightUp();
        }

        public static void MiddleClick(this IMouse mouse)
        {
            mouse.MiddleDown();
            mouse.MiddleUp();
        }
    }

    public static class MoveExtensions
    {
        private static int TranslateMovementSpeedToPixelsPerSecond(MovementSpeed movement)
        {
            switch (movement)
            {
                case MovementSpeed.Slow:
                    return 200;
                case MovementSpeed.Medium:
                    return 400;
                case MovementSpeed.Fast:
                    return 800;
                default:
                    return 400;
            }
        }

        public static void MoveTo(this IMouse mouse, Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Instant)
        {            
            if (MovementSpeed.Instant.Equals(movementSpeed))
            {
                mouse.PositionCursor(target);
                return;
            }

            //calculate the distance to drag as a double to avoid rounding errors later when converting to int
            var startPosition = mouse.FindCursor();
            var startX = Convert.ToDouble(startPosition.X);
            var startY = Convert.ToDouble(startPosition.Y);
            var dX = Convert.ToDouble(target.X) - startX;
            var dY = Convert.ToDouble(target.Y) - startY;
            var distance = Hypotenuse(dX, dY);

            //calculate number of steps needed to perform the move operation
            var pixelsPerSecond = TranslateMovementSpeedToPixelsPerSecond(movementSpeed);
            var sectionMovementDuration = 10;
            var steps = CalculateNumberOfMovementSteps(pixelsPerSecond, sectionMovementDuration, distance);

            //calculate the movement of each step
            var incrementX = dX / steps;
            var incrementY = dY / steps;

            //we need to compensate for int rounding so save the fractions for the next round
            var xRemainingFraction = 0.0d;
            var yRemainingFraction = 0.0d;
            for (var i = 1; i < steps; ++i)
            {
                //apply the fraction part saved from the last loop
                var targetDx = incrementX + xRemainingFraction;
                var targetDy = incrementY + yRemainingFraction;

                //save the fraction for the next loop
                xRemainingFraction = targetDx - (int)targetDx;
                yRemainingFraction = targetDy - (int)targetDy;

                //position cursor to the int part of the coordinates
                var x = (int)((incrementX) * i + startX);
                var y = (int)((incrementY) * i + startY);
                mouse.PositionCursor(new Coordinate(x, y));
                System.Threading.Thread.Sleep(sectionMovementDuration);
            }

            //verify that we are not too far off from the target position
            var endPosition = mouse.FindCursor();
            var distanceFromWantedPosition = (int)CalculateDistance(target, endPosition);
            System.Diagnostics.Debug.Assert(!(Math.Abs(distanceFromWantedPosition) > 5), string.Format("end distance is {0} pixels off", distanceFromWantedPosition));

            //adjusting for rounding errors
            mouse.PositionCursor(new Coordinate(target.X, target.Y));
        }

        private static double CalculateDistance(Coordinate a, Coordinate b)
        {
            var dX = a.X - b.X;
            var dY = a.Y - b.Y;
            return Hypotenuse(dX, dY);
        }

        private static double Hypotenuse(double a, double b)
        {
            return Math.Sqrt(a * a + b * b);
        }

        private static int CalculateNumberOfMovementSteps(int pixelsPerSecond, int sectionMovementDurationMs, double distance)
        {
            var pps = Convert.ToDouble(pixelsPerSecond);
            var smd = Convert.ToDouble(sectionMovementDurationMs);
            var ticks = 1000d;

            return Convert.ToInt32((ticks / smd) / (pps / distance));
        }        
    }

    public static class DragExtensions
    {
        public static void DragAndDrop(this IMouse mouse, Coordinate origin, Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Medium)
        {
            mouse.PositionCursor(origin);
            mouse.LeftDown();
            mouse.MoveTo(target, movementSpeed);
            mouse.LeftUp();
        }
    }

}
