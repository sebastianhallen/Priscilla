namespace Priscilla
{
    public static class ClickExtensions
    {
        public static void LeftClick(this IMouse mouse)
        {
            mouse.LeftDown();
            mouse.LeftUp();
        }

        public static void RightClick(this IMouse mouse)
        {
            mouse.RightDown();
            mouse.RightUp();
        }

        public static void MiddleClick(this IMouse mouse)
        {
            mouse.MiddleDown();
            mouse.MiddleUp();
        }
    }
}