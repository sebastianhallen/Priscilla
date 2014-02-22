namespace Priscilla.Test
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;
    using Priscilla.Native;

    [TestFixture]
    public class MouseTest
    {
        private Mouse mouse;
        private INativeMethodWrapper nativeMethodWrapper;

        [SetUp]
        public void Before()
        {
            this.nativeMethodWrapper = A.Fake<INativeMethodWrapper>();
            this.mouse = new Mouse(this.nativeMethodWrapper);
        }

        [Test]
        public void Should_set_cursor_position_with_native_wrapper()
        {
            this.mouse.PositionCursor(new Coordinate(10, 20));

            A.CallTo(() => this.nativeMethodWrapper.SetCursorPos(10, 20)).MustHaveHappened();
        }

        [Test]
        public void Should_get_cursor_position_from_native_wrapper()
        {
            CursorCoordinate _;
            A.CallTo(() => this.nativeMethodWrapper.GetCursorPos(out _)).Returns(true);

            this.mouse.FindCursor();
        
            A.CallTo(() => this.nativeMethodWrapper.GetCursorPos(out _)).MustHaveHappened();
        }

        [Test]
        public void Should_move_cursor_with_native_wrapper()
        {
            var x = 10;
            var y = 20;

            this.mouse.MoveCursor(x, y);

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.Move, (uint) y, (uint) x, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_left_down_with_native_wrapper()
        {
            this.mouse.LeftDown();

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.LeftDown, 0, 0, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_left_up_with_native_wrapper()
        {
            this.mouse.LeftUp();

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.LeftUp, 0, 0, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_pres_left_down_at_specific_position_with_native_wrapper()
        {
            this.mouse.LeftDown(new Coordinate(1024, 768));

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.LeftDown | Mouse.MouseInput.Absolute, 768, 1024, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_left_up_at_specific_position_with_native_wrapper()
        {
            this.mouse.LeftUp(new Coordinate(1024, 768));

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.LeftUp | Mouse.MouseInput.Absolute, 768, 1024, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_right_down_with_native_wrapper()
        {
            this.mouse.RightDown();

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.RightDown, 0, 0, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_righ_up_with_native_wrapper()
        {
            this.mouse.RightUp();

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.RightUp, 0, 0, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_right_down_at_specific_position_with_native_wrapper()
        {
            this.mouse.RightDown(new Coordinate(1024, 768));

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.RightDown | Mouse.MouseInput.Absolute, 768, 1024, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_right_up_at_specific_position_with_native_wrapper()
        {
            this.mouse.RightUp(new Coordinate(1024, 768));

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.RightUp | Mouse.MouseInput.Absolute, 768, 1024, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_middle_down_with_native_wrapper()
        {
            this.mouse.MiddleDown();

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.MiddleDown, 0, 0, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_middle_up_with_native_wrapper()
        {
            this.mouse.MiddleUp();

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.MiddleUp, 0, 0, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_middle_down_at_specific_position_with_native_wrapper()
        {
            this.mouse.MiddleDown(new Coordinate(1024, 768));

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.MiddleDown | Mouse.MouseInput.Absolute, 768, 1024, 0, A<IntPtr>._)).MustHaveHappened();
        }

        [Test]
        public void Should_press_middle_up_at_specific_position_with_native_wrapper()
        {
            this.mouse.MiddleUp(new Coordinate(1024, 768));

            A.CallTo(() => this.nativeMethodWrapper.mouse_event(Mouse.MouseInput.MiddleUp | Mouse.MouseInput.Absolute, 768, 1024, 0, A<IntPtr>._)).MustHaveHappened();
        }
    }
}
