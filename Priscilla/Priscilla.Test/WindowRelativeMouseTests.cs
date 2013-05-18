namespace Priscilla.Test
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;
    using Priscilla.Native;

    internal class WindowRelativeMouseTests
    {
        protected IMouse windowRelativeMouse;
        protected IMouse innerMouse;
        protected INativeMethodWrapper nativeMethodWrapper;
        protected IntPtr hWnd;

        [SetUp]
        public void Before()
        {
            this.hWnd = new IntPtr(1337);
            this.innerMouse = A.Fake<IMouse>();
            this.nativeMethodWrapper = A.Fake<INativeMethodWrapper>();
            this.windowRelativeMouse = new WindowRelativeMouse(this.hWnd, this.innerMouse, this.nativeMethodWrapper);
        }
    }
}
