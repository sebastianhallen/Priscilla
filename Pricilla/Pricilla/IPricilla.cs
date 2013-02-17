namespace Pricilla
{
    public interface IPricilla
    {
        void MoveTo(Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Instant, Coordinate offset = null);
        void DragAndDrop(Coordinate source, Coordinate target, Coordinate offset = null);
        void LeftClick();
        void RightClick();
        void MiddleClick();      
    }
}