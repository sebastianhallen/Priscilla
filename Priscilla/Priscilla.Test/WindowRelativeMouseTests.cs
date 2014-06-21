namespace Priscilla.Test
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;
    using Priscilla.Native;
    using Priscilla.Test.Utils.Retries;
    using Priscilla.Utils.Retry;

    internal class WindowRelativeMouseTests
    {
        protected IMouse windowRelativeMouse;
        protected IMouse innerMouse;
        protected INativeMethodWrapper nativeMethodWrapper;
        protected IRetryTimerFactory retrierFactory;
        protected IntPtr hWnd;
	    protected Retrier retrier;

	    [SetUp]
        public void Before()
        {
            this.hWnd = new IntPtr(1337);
            this.innerMouse = A.Fake<IMouse>();
            this.nativeMethodWrapper = A.Fake<INativeMethodWrapper>();
            this.retrierFactory = new NRetryTimerFactory(0);
            this.retrier = new Retrier(this.retrierFactory);

            this.windowRelativeMouse = new WindowRelativeMouse(this.hWnd, this.hWnd, this.innerMouse, this.nativeMethodWrapper, this.retrier);
        }
    }

    
}
