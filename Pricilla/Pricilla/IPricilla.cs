namespace Pricilla
{
    public interface IPricilla
    {
        void MoveTo(Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Instant);
        void LeftClick();
        void RightClick();
        void MiddleClick();
        void DragAndDrop(Coordinate coordinate, Coordinate coordinate1);
    }
}