namespace Priscilla.Native
{
    using System;

    public class ApplicationWindowFinder
        : IApplicationWindowFinder
    {
        private readonly INativeMethodWrapper nativeMethodWrapper;

        public ApplicationWindowFinder()
            : this(new NativeMethodWrapper())
        {
        }

        internal ApplicationWindowFinder(INativeMethodWrapper nativeMethodWrapper)
        {
            this.nativeMethodWrapper = nativeMethodWrapper;
        }

        public IntPtr FindWindow(string windowClass, string windowTitle)
        {
            return this.nativeMethodWrapper.FindWindowEx(IntPtr.Zero, IntPtr.Zero, windowClass, windowTitle);
        }

        public IntPtr FindChildWindow(IntPtr hwndParent, string windowClass, string windowTitle)
        {
            return this.nativeMethodWrapper.FindWindowEx(hwndParent, IntPtr.Zero, windowClass, windowTitle);
        }
    }
}