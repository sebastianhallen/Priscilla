namespace Priscilla
{
    public static class DragExtensions
    {
        public static void DragAndDrop(this IMouse mouse, Coordinate origin, Coordinate target, MovementSpeed movementSpeed = MovementSpeed.Medium)
        {
            mouse.LeftDown(origin);
            mouse.MoveTo(target, movementSpeed);
            mouse.LeftUp(target);
        }

        public static void DragAndDrop(this IMouse mouse, Coordinate origin, Coordinate target, int pixelsPerSecond)
        {
            mouse.LeftDown(origin);
            mouse.MoveTo(target, pixelsPerSecond);
            mouse.LeftUp(target);
        }
    }

}
