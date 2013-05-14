namespace Priscilla
{
    using System;
    using System.Runtime.InteropServices;

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
}