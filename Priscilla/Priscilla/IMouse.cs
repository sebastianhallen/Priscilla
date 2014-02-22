namespace Priscilla
{
    public interface IMouse
    {
        void PositionCursor(Coordinate coordinate);
        Coordinate FindCursor();
        void MoveCursor(int dx, int dy);

        void LeftDown();
        void LeftDown(Coordinate coordinate);
        void LeftUp();
        void LeftUp(Coordinate coordinate);


        void RightDown();
        void RightDown(Coordinate coordinate);
        void RightUp();
        void RightUp(Coordinate coordinate);

        void MiddleDown();
        void MiddleDown(Coordinate coordinate);
        void MiddleUp();
        void MiddleUp(Coordinate coordinate);
    }
}