namespace Priscilla
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Area
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public int Height
        {
            get { return this.Bottom - this.Top; }
        }

        public int Width
        {
            get { return this.Right - this.Left; }
        }

        public static implicit operator System.Drawing.Rectangle(Area r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }
    }
}