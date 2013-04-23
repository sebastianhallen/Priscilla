namespace Priscilla
{
    public interface IMouse
    {
        void PositionCursor(Coordinate coordinate);
        Coordinate FindCursor();
        //void MoveCursor(int dy, int dx);


        void LeftDown();
        void LeftUp();
        
        void RightDown();
        void RightUp();

        void MiddleDown();
        void MiddleUp();
    }
}