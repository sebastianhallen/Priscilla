namespace Priscilla.Native
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
}