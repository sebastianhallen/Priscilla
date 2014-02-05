namespace Priscilla.Native
{
    using System;
    using System.Drawing;

    public static class FluentApplicationWindowFinder
    {
        public static IntPtr FindChildWindow(this IntPtr hwndParent, string windowClass, string windowTitle = null)
        {
            return new ApplicationWindowFinder().FindChildWindow(hwndParent, windowClass, windowTitle);
        }

        public static Rectangle GetClientArea(this IntPtr hWnd)
        {
            return new ApplicationWindowFinder().GetClientArea(hWnd);
        }
    }
}