namespace Pricilla
{
    using System;

    public class Pricilla
        : IPricilla
    {
        private readonly IMouse mouse;

        public Pricilla(IMouse mouse)
        {
            this.mouse = mouse;
        }

        public void LeftClick()
        {
            this.mouse.LeftDown();
            this.mouse.LeftUp();
        }

        public void RightClick()
        {
            this.mouse.RightDown();
            this.mouse.RightUp();
        }

        public void MiddleClick()
        {
            this.mouse.MiddleDown();
            this.mouse.MiddleUp();
        }

        public void DragAndDrop(Coordinate origin, Coordinate target, Coordinate offset = null)
        {
            this.mouse.PositionCursor(origin + offset);
            this.mouse.LeftDown();
            this.MoveTo(target, MovementSpeed.Medium, offset: offset);
            this.mouse.LeftUp();
        }

        public void MoveTo(Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Instant, Coordinate offset = null)
        {
            var targetWithOffset = target + offset;

            if (MovementSpeed.Instant.Equals(movementSpeed))
            {
                this.mouse.PositionCursor(targetWithOffset);
                return;
            }

            //calculate the distance to drag as a double to avoid rounding errors later when converting to int
            var startPosition = this.mouse.FindCursor();
            var startX = Convert.ToDouble(startPosition.X);
            var startY = Convert.ToDouble(startPosition.Y);
            var dX = Convert.ToDouble(targetWithOffset.X) - startX;
            var dY = Convert.ToDouble(targetWithOffset.Y) - startY;
            var distance = this.Hypotenuse(dX, dY);

            //calculate number of steps needed to perform the move operation
            var pixelsPerSecond = 100;
            var sectionMovementDuration = 10;
            var steps = this.CalculateNumberOfMovementSteps(pixelsPerSecond, sectionMovementDuration, distance);

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
                this.mouse.PositionCursor(new Coordinate(x, y));
                System.Threading.Thread.Sleep(sectionMovementDuration);
            }

            //verify that we are not too far off from the target position
            var endPosition = this.mouse.FindCursor();
            var distanceFromWantedPosition = (int)this.CalculateDistance(targetWithOffset, endPosition);
            System.Diagnostics.Debug.Assert(!(Math.Abs(distanceFromWantedPosition) > 5), string.Format("end distance is {0} pixels off", distanceFromWantedPosition));

            //adjusting for rounding errors
            this.mouse.PositionCursor(new Coordinate(targetWithOffset.X, targetWithOffset.Y));
        }

        private double CalculateDistance(Coordinate a, Coordinate b)
        {
            var dX = a.X - b.X;
            var dY = a.Y - b.Y;
            return this.Hypotenuse(dX, dY);
        }

        private double Hypotenuse(double a, double b)
        {
            return Math.Sqrt(a * a + b * b);
        }

        private int CalculateNumberOfMovementSteps(int pixelsPerSecond, int sectionMovementDurationMs, double distance)
        {
            var pps = Convert.ToDouble(pixelsPerSecond);
            var smd = Convert.ToDouble(sectionMovementDurationMs);
            var ticks = 1000d;

            return Convert.ToInt32((ticks / smd) / (pps / distance));
        }        
    }
}
