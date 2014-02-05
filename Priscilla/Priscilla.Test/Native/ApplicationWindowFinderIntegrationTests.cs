namespace Priscilla.Test.Native
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
            var viewporthWnd = this.windowFinder.FindWindow("Chrome_WidgetWin_1", ApplicationWindowFinder.Wildcard + " - Google Chrome").FindChildWindow("Static");

            Assert.That(viewporthWnd, Is.Not.EqualTo(IntPtr.Zero));
        }

        [Test]
        public void Should_be_able_to_find_window_by_title_with_wildcard()
        {
            var chromehWnd = this.windowFinder.FindWindow("Chrome_WidgetWin_1", ApplicationWindowFinder.Wildcard + " - Google Chrome");

            Assert.That(chromehWnd, Is.Not.EqualTo(IntPtr.Zero));
        }

        [Test]
        public void Should_be_able_to_get_window_client_area_for_a_window()
        {
            var area = this.windowFinder.FindWindow("Chrome_WidgetWin_1", ApplicationWindowFinder.Wildcard + "pinvoke" + ApplicationWindowFinder.Wildcard + " - Google Chrome")
                                        .GetClientArea();

            Assert.That(area, Is.Not.EqualTo(new System.Drawing.Rectangle()));
        }

        [Test]
        public void Should_explode_when_trying_to_get_window_client_area_from_a_non_existing_window()
        {
            var exception = Assert.Throws<Exception>(() => this.windowFinder.FindWindow("ÄADF").GetClientArea());

            Assert.That(exception.Message, Is.EqualTo("Unable to get client area"));
        }
    }
}
