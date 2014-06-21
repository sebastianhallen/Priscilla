namespace Priscilla
{
    using System;

    public static class MoveExtensions
    {
	    public static int SectionMovementDuration = 10;

        public static void MoveTo(this IMouse mouse, Coordinate target, int pixelsPerSecond)
        {
            //calculate the distance to drag as a double to avoid rounding errors later when converting to int
            var startPosition = mouse.FindCursor();
            var startX = Convert.ToDouble(startPosition.X);
            var startY = Convert.ToDouble(startPosition.Y);
            var dX = Convert.ToDouble(target.X) - startX;
            var dY = Convert.ToDouble(target.Y) - startY;
            var distance = Hypotenuse(dX, dY);

            //calculate number of steps needed to perform the move operation
			var steps = CalculateNumberOfMovementSteps(pixelsPerSecond, SectionMovementDuration, distance);

            //calculate the movement of each step
            var incrementX = dX / steps;
            var incrementY = dY / steps;

            //we need to compensate for int rounding so save the fractions for the next round
            var xRemainingFraction = 0.0d;
            var yRemainingFraction = 0.0d;

            //note that this is one iteration too short but due to rounding errors we will make
            //the final "snap to end position" call to PositionCursor outside of the loop
            for (var i = 1; i < steps; ++i)
            {
                //calculate the target location for the round
                var incrementTargetX = startX + (incrementX * i) + xRemainingFraction;
                var incrementTargetY = startY + (incrementY * i) + yRemainingFraction;
                var roundedIncrementTargetX = Math.Ceiling(incrementTargetX);
                var roundedIncrementTargetY = Math.Ceiling(incrementTargetY);

                //calculate the int rounding error that we need to compensate for in the next iteration
                xRemainingFraction = incrementTargetX - roundedIncrementTargetX;
                yRemainingFraction = incrementTargetY - roundedIncrementTargetY;

                //do the actual positioning
                mouse.PositionCursor(new Coordinate((int)roundedIncrementTargetX, (int)roundedIncrementTargetY));

                System.Threading.Thread.Sleep(SectionMovementDuration);
            }

            //snap to end position
            mouse.PositionCursor(new Coordinate(target.X, target.Y));
        }

        public static void MoveTo(this IMouse mouse, Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Instant)
        {
            if (MovementSpeed.Instant.Equals(movementSpeed))
            {
                mouse.PositionCursor(target);
                return;
            }

            var pixelsPerSecond = TranslateMovementSpeedToPixelsPerSecond(movementSpeed);
            MoveTo(mouse, target, pixelsPerSecond);
        }

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
}