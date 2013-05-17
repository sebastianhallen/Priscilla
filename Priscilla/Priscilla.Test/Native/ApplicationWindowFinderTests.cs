namespace Priscilla.Test.Native
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;
    using Priscilla.Native;

    [TestFixture]
    public class ApplicationWindowFinderTests
    {
        private ApplicationWindowFinder windowFinder;
        private INativeMethodWrapper nativeMethodWrapper;

        [SetUp]
        public void Before()
        {
            this.nativeMethodWrapper = A.Fake<INativeMethodWrapper>();
            this.windowFinder = new ApplicationWindowFinder(this.nativeMethodWrapper);
        }

        [Test]
        public void Should_delegate_find_window_call_to_native_method_wrapper_when_finding_root_window()
        {
            this.windowFinder.FindWindow("class", "title");

            A.CallTo(() => this.nativeMethodWrapper.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "class", "title")).MustHaveHappened();
        }

        [Test]
        public void Should_delegate_find_window_call_to_native_wrapper_when_finding_child_window()
        {
            var parent = new IntPtr(1337);

            this.windowFinder.FindChildWindow(parent, "class", "title");

            A.CallTo(() => this.nativeMethodWrapper.FindWindowEx(parent, IntPtr.Zero, "class", "title")).MustHaveHappened();
        }
    }
}
