namespace Priscilla
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorCoordinate
    {
        public int X;
        public int Y;

        public static implicit operator Coordinate(CursorCoordinate coordinate)
        {
            return new Coordinate(coordinate.X, coordinate.Y);
        }
    }
}