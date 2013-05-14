namespace Priscilla
{
    using System;
    using System.Runtime.InteropServices;

    public interface IApplicationWindowFinder
    {
        IntPtr FindWindow(string windowClass, string windowTitle = null);
        IntPtr FindWindow(IntPtr hwndParent, string windowClass, string windowTitle = null);
    }

    public class ApplicationWindowFinder
        : IApplicationWindowFinder
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        IntPtr IApplicationWindowFinder.FindWindow(string windowClass, string windowTitle)
        {
            return FindWindowEx(IntPtr.Zero, IntPtr.Zero, windowClass, windowTitle);
        }

        IntPtr IApplicationWindowFinder.FindWindow(IntPtr hwndParent, string windowClass, string windowTitle)
        {
            return FindWindowEx(hwndParent, IntPtr.Zero, windowClass, windowTitle);
        }
    }


    public class WindowBoundMouse
        : IMouse
    {
        private readonly IntPtr hWnd;

        public WindowBoundMouse(IntPtr hWnd)
        {
            this.hWnd = hWnd;
        }

        public void PositionCursor(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }

        public Coordinate FindCursor()
        {
            throw new NotImplementedException();
        }

        public void MoveCursor(int dx, int dy)
        {
            throw new NotImplementedException();
        }

        public void LeftDown()
        {
            throw new NotImplementedException();
        }

        public void LeftUp()
        {
            throw new NotImplementedException();
        }

        public void RightDown()
        {
            throw new NotImplementedException();
        }

        public void RightUp()
        {
            throw new NotImplementedException();
        }

        public void MiddleDown()
        {
            throw new NotImplementedException();
        }

        public void MiddleUp()
        {
            throw new NotImplementedException();
        }
    }
}