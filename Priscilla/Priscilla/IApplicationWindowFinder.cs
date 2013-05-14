namespace Priscilla
{
    using System;

    public interface IApplicationWindowFinder
    {
        IntPtr FindWindow(string windowClass, string windowTitle = null);
        IntPtr FindWindow(IntPtr hwndParent, string windowClass, string windowTitle = null);
    }
}