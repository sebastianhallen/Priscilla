namespace Pricilla
{
    public class Coordinate
    {
        public int Y;
        public int X;

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            var aCoord = a ?? new Coordinate(0, 0);
            var bCoord = b ?? new Coordinate(0, 0);

            return new Coordinate(aCoord.X + bCoord.X, aCoord.Y + bCoord.Y);
        }
    }
}