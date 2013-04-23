namespace Priscilla
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

        public static Coordinate operator -(Coordinate a, Coordinate b)
        {
            var aCoord = a ?? new Coordinate(0, 0);
            var bCoord = b ?? new Coordinate(0, 0);

            return new Coordinate(aCoord.X - bCoord.X, aCoord.Y - bCoord.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash*23 + this.X.GetHashCode();
                hash = hash*23 + this.Y.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("(x:{0}|y:{1})", this.X, this.Y);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Coordinate);
        }

        private bool Equals(Coordinate other)
        {
            if (other == null) return false;
            return this.X.Equals(other.X) && this.Y.Equals(other.Y);
        }
    }
}