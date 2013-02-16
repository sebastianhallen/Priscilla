namespace Pricilla
{
    using System;

    public interface IPricilla
    {
        void MoveTo(Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Instant);
        void LeftClick();
        void RightClick();
        void MiddleClick();
        void DragAndDrop(Coordinate coordinate, Coordinate coordinate1);
    }

    public class Pricilla
        : IPricilla
    {
        private readonly IMouse mouse;

        public Pricilla(IMouse mouse)
        {
            this.mouse = mouse;
        }

        public void MoveTo(Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Instant)
        {
            if (MovementSpeed.Instant.Equals(movementSpeed))
            {
                this.mouse.PositionCursor(target);
                return;
            }
            
            var startPosition = this.mouse.FindCursor();
            var dX = startPosition.X - target.X;
            var dY = startPosition.Y - target.Y;
            var distance = Math.Sqrt((dX * dX + dY* dY));

            var pixelsPerSecond = 100;
            var sectionMovementDuration = 10;
            var steps = this.CalculateNumberOfMovementSteps(pixelsPerSecond, sectionMovementDuration, distance);
            var incrementX = dX / steps;
            var incrementY = dY / steps;

            if (incrementX == 0) incrementX = dX > 0 ? -1 : 1;
            if (incrementY == 0) incrementY = dY > 0 ? -1 : 1;

            for (var i = 0; i < steps; ++i)
            {
                this.mouse.MoveCursor(incrementX, incrementY);
                System.Threading.Thread.Sleep(sectionMovementDuration);
            }
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

        public void DragAndDrop(Coordinate from, Coordinate to)
        {
            this.mouse.PositionCursor(from);
            this.mouse.LeftDown();
            this.MoveTo(to, MovementSpeed.Medium);
            this.mouse.LeftUp();
        }

        private int CalculateNumberOfMovementSteps(int pixelsPerSecond, int sectionMovementDurationMs, double distance)
        {
            var pps = Convert.ToDouble(pixelsPerSecond);
            var smd = Convert.ToDouble(sectionMovementDurationMs);
            var ticks = 1000d;

            var steps = (ticks / smd) / (pps / distance);
            return Convert.ToInt32(steps);
        }
    }
}
