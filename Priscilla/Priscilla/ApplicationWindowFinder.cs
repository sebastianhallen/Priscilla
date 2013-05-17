namespace Priscilla
{
    using System;

    public class ApplicationWindowFinder
        : IApplicationWindowFinder
    {        
        public IntPtr FindWindow(string windowClass, string windowTitle)
        {
            return NativeMethods.FindWindowEx(IntPtr.Zero, IntPtr.Zero, windowClass, windowTitle);
        }

        public IntPtr FindChildWindow(IntPtr hwndParent, string windowClass, string windowTitle)
        {
            return NativeMethods.FindWindowEx(hwndParent, IntPtr.Zero, windowClass, windowTitle);
        }
    }

    public static class FluentApplicationWindowFinder
    {
        public static IntPtr FindChildWindow(this IntPtr hwndParent, string windowClass, string windowTitle = null)
        {
            return new ApplicationWindowFinder().FindChildWindow(hwndParent, windowClass, windowTitle);
        }
    }
}