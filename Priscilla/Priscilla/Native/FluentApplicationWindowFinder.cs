namespace Priscilla.Native
{
    using System;

    public static class FluentApplicationWindowFinder
    {
        public static IntPtr FindChildWindow(this IntPtr hwndParent, string windowClass, string windowTitle = null)
        {
            return new ApplicationWindowFinder().FindChildWindow(hwndParent, windowClass, windowTitle);
        }
    }
}