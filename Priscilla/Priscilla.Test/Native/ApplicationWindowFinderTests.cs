namespace Priscilla.Test.Native
{
    using System;
    using System.Text;
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

        [TearDown]
        public void After()
        {
            ApplicationWindowFinder.UseCaseInsensitiveSearch = false;
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

        [Test]
        public void Should_return_exact_match_when_finding_window_with_title_containing_wildcard()
        {
            var hWnd = new IntPtr(1337);
            A.CallTo(() => this.nativeMethodWrapper.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "class", "some title " + ApplicationWindowFinder.Wildcard)).Returns(hWnd);

            var result = this.windowFinder.FindWindow("class", "some title " + ApplicationWindowFinder.Wildcard);

            Assert.That(result, Is.EqualTo(hWnd));
        }

        [Test]
        public void Should_be_able_to_search_for_window_with_wildcard_title()
        {
            this.ExpectWindow("some class", "some title with a suffix");

            var result = this.windowFinder.FindWindow("some class", ApplicationWindowFinder.Wildcard + "ome title" + ApplicationWindowFinder.Wildcard);

            Assert.That(result.ToInt32(), Is.EqualTo(1337));
        }

        [Test]
        public void Should_be_able_to_search_for_window_with_wildcard_class()
        {
            this.ExpectWindow("some class with suffix", "some title");

            var result = this.windowFinder.FindWindow(ApplicationWindowFinder.Wildcard + "class" + ApplicationWindowFinder.Wildcard, "some title");

            Assert.That(result.ToInt32(), Is.EqualTo(1337));
        }

        [Test]
        public void Should_find_window_with_matching_case_insensitive_class_when_case_insensitive_search_is_enabled()
        {
            this.ExpectWindow("some class with suffix", "some title");
            ApplicationWindowFinder.UseCaseInsensitiveSearch = true;

            var result = this.windowFinder.FindWindow(ApplicationWindowFinder.Wildcard + "CLASS" + ApplicationWindowFinder.Wildcard, "some title");

            Assert.That(result.ToInt32(), Is.EqualTo(1337));
        }

        [Test]
        public void Should_find_window_with_matching_case_insensitive_title_when_case_insensitive_search_is_enabled()
        {
            this.ExpectWindow("some class with suffix", "some title");
            ApplicationWindowFinder.UseCaseInsensitiveSearch = true;

            var result = this.windowFinder.FindWindow("some class with suffix", "soME TITle");

            Assert.That(result.ToInt32(), Is.EqualTo(1337));
        }

        [Test]
        public void Should_not_find_window_with_matching_case_insensitive_class_when_case_insensitive_search_is_disabled()
        {
            this.ExpectWindow("some class with suffix", "some title");
            ApplicationWindowFinder.UseCaseInsensitiveSearch = false;

            var result = this.windowFinder.FindWindow(ApplicationWindowFinder.Wildcard + "CLASS" + ApplicationWindowFinder.Wildcard, "some title");

            Assert.That(result.ToInt32(), Is.EqualTo(0));
        }

        [Test]
        public void Should_not_find_window_with_matching_case_insensitive_title_when_case_insensitive_search_is_disabled()
        {
            this.ExpectWindow("some class with suffix", "some title");
            ApplicationWindowFinder.UseCaseInsensitiveSearch = false;

            var result = this.windowFinder.FindWindow("some class with suffix", "soME TITle");

            Assert.That(result.ToInt32(), Is.EqualTo(0));
        }

        private void ExpectWindow(string windowClass, string windowCaption)
        {
            var _ = IntPtr.Zero;
            A.CallTo(() => this.nativeMethodWrapper.GetClassName(A.Dummy<IntPtr>(), A.Dummy<StringBuilder>(), A.Dummy<int>()))
             .WithAnyArguments()
             .Invokes(fake => fake.Arguments.Get<StringBuilder>("lpClassName").Append(windowClass))
             .Returns(0);
            A.CallTo(() => this.nativeMethodWrapper.GetWindowText(A.Dummy<IntPtr>(), A.Dummy<StringBuilder>(), A.Dummy<int>()))
             .WithAnyArguments()
             .Invokes(fake => fake.Arguments.Get<StringBuilder>("lpString").Append(windowCaption))
             .Returns(0);
            A.CallTo(() => this.nativeMethodWrapper.EnumChildWindows(IntPtr.Zero, A<Func<IntPtr, bool>>._))
             .WithAnyArguments()
             .Invokes(fake =>
             {
                 var callback = fake.Arguments.Get<Func<IntPtr, bool>>(1);
                 callback(new IntPtr(1337));
             })
             .Returns(true);
        }
    }
}
