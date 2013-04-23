namespace Priscilla
{
    public interface IMouse
    {
        void PositionCursor(Coordinate coordinate);
        Coordinate FindCursor();
        void MoveCursor(int dx, int dy);


        void LeftDown();
        void LeftUp();
        
        void RightDown();
        void RightUp();

        void MiddleDown();
        void MiddleUp();
    }
}