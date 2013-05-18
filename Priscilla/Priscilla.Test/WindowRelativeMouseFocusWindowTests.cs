namespace Priscilla.Test
{
    using FakeItEasy;
    using NUnit.Framework;

    [TestFixture]
    internal class WindowRelativeMouseFocusWindowTests
        : WindowRelativeMouseTests
    {
        [Test]
        public void Should_focus_window_before_performing_left_down()
        {
            this.windowRelativeMouse.LeftDown();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened();
        }

        [Test]
        public void Should_focus_window_before_performing_left_up()
        {
            this.windowRelativeMouse.LeftUp();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened();
        }

        [Test]
        public void Should_focus_window_before_performing_middle_down()
        {
            this.windowRelativeMouse.MiddleDown();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened();
        }

        [Test]
        public void Should_focus_window_before_performing_middle_up()
        {
            this.windowRelativeMouse.MiddleUp();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened();
        }

        [Test]
        public void Should_focus_window_before_performing_right_down()
        {
            this.windowRelativeMouse.RightDown();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened();
        }

        [Test]
        public void Should_focus_window_before_performing_right_up()
        {
            this.windowRelativeMouse.RightUp();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened();
        }
    }
}