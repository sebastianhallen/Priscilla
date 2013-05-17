namespace Priscilla.Test
{
    using System;
    using NUnit.Framework;
    using Priscilla.Native;

    // these tests require google chrome to be running
    [TestFixture, Explicit]
    public class ApplicationWindowFinderIntegrationTests
    {
        private IApplicationWindowFinder windowFinder;

        [SetUp]
        public void Before()
        {
            windowFinder = new ApplicationWindowFinder();
        }

        [Test]
        public void Should_return_zero_pointer_when_trying_to_find_a_non_existing_window()
        {
            var hWnd = this.windowFinder.FindWindow("meh");

            Assert.That(hWnd, Is.EqualTo(IntPtr.Zero));
        }

        [Test]
        public void Should_be_able_to_find_a_google_chrome_browser_window()
        {
            var chromehWnd = this.windowFinder.FindWindow("Chrome_WidgetWin_1");

            Assert.That(chromehWnd, Is.Not.EqualTo(IntPtr.Zero));
        }

        [Test]
        public void Should_return_zero_pointer_when_trying_to_find_non_existing_child_window()
        {
            var hWnd = this.windowFinder.FindWindow("Chrome_WidgetWin_1").FindChildWindow("meh");

            Assert.That(hWnd, Is.EqualTo(IntPtr.Zero));
        }

        [Test]
        public void Should_be_able_to_find_viewport_in_a_google_chrome_browser_window()
        {
            var viewporthWnd = this.windowFinder.FindWindow("Chrome_WidgetWin_1").FindChildWindow("Chrome_WidgetWin_0").FindChildWindow("Chrome_RenderWidgetHostHWND");

            Assert.That(viewporthWnd, Is.Not.EqualTo(IntPtr.Zero));
        }
    }
}
