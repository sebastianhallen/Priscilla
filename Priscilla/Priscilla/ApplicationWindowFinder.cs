namespace Priscilla
{
    using System;
    using System.Runtime.InteropServices;

    public class ApplicationWindowFinder
        : IApplicationWindowFinder
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        public IntPtr FindWindow(string windowClass, string windowTitle)
        {
            return FindWindowEx(IntPtr.Zero, IntPtr.Zero, windowClass, windowTitle);
        }

        public IntPtr FindChildWindow(IntPtr hwndParent, string windowClass, string windowTitle)
        {
            return FindWindowEx(hwndParent, IntPtr.Zero, windowClass, windowTitle);
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