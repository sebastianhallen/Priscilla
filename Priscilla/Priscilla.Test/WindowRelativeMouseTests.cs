namespace Priscilla.Test
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;
    using Priscilla.Native;
    using Priscilla.Utils.Retry;

    internal class WindowRelativeMouseTests
    {
        protected IMouse windowRelativeMouse;
        protected IMouse innerMouse;
        protected INativeMethodWrapper nativeMethodWrapper;
        protected IRetryTimerFactory retrierFactory;
        protected IntPtr hWnd;

        [SetUp]
        public void Before()
        {
            this.hWnd = new IntPtr(1337);            
            this.innerMouse = A.Fake<IMouse>();
            this.nativeMethodWrapper = A.Fake<INativeMethodWrapper>();
            this.retrierFactory = A.Fake<IRetryTimerFactory>();            
            var retrier = new Retrier(this.retrierFactory);

            A.CallTo(() => this.retrierFactory.Create(A<TimeSpan>._)).Returns(A.Dummy<IRetryTimer>());
            this.windowRelativeMouse = new WindowRelativeMouse(this.hWnd, this.hWnd, this.innerMouse, this.nativeMethodWrapper, retrier);
        }
    }
}
