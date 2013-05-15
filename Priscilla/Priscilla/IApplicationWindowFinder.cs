namespace Priscilla
{
    using System;

    public interface IApplicationWindowFinder
    {
        IntPtr FindWindow(string windowClass, string windowTitle = null);
        IntPtr FindChildWindow(IntPtr hwndParent, string windowClass, string windowTitle = null);
    }
}