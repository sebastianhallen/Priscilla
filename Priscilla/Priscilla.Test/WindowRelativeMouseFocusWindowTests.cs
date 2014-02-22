namespace Priscilla.Test
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;

    [TestFixture]
    internal class WindowRelativeMouseFocusWindowTests
        : WindowRelativeMouseTests
    {        
        [Test]
        public void Should_focus_window_before_performing_left_down()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.LeftDown();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_left_up()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.LeftUp();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_left_down_at_position()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.LeftDown(new Coordinate(1, 1));

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_left_up_at_position()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.LeftUp(new Coordinate(1, 1));

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_middle_down()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.MiddleDown();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_middle_up()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.MiddleUp();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_middle_down_at_position()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.MiddleDown(new Coordinate(1, 1));

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_middle_up_at_position()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.MiddleUp(new Coordinate(1, 1));

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_right_down()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.RightDown();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_right_up()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.RightUp();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_right_down_at_position()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.RightDown(new Coordinate(1, 1));

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_focus_window_before_performing_right_up_at_position()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.RightUp(new Coordinate(1, 1));

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_check_if_window_has_focus_before_focusing()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).ReturnsNextFromSequence(IntPtr.Zero, this.hWnd);

            this.windowRelativeMouse.LeftDown();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(this.hWnd)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => this.innerMouse.LeftDown()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_not_set_foregroundwindow_when_window_already_is_foreground_window()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).Returns(this.hWnd);

            this.windowRelativeMouse.LeftDown();

            A.CallTo(() => this.nativeMethodWrapper.SetForegroundWindow(A<IntPtr>._)).MustNotHaveHappened();
            A.CallTo(() => this.innerMouse.LeftDown()).MustHaveHappened(Repeated.Exactly.Once);
        }

    }
}