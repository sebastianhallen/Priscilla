namespace Priscilla.Test
{
	using FakeItEasy;
    using NUnit.Framework;
 
    [TestFixture]
    internal class WindowRelativeMouseProxyTests
        : WindowRelativeMouseTests
    {
        private readonly Coordinate offset = new Coordinate(33, 22);

        [SetUp]
        public void BeforeEach()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).Returns(this.hWnd);
            var _ = new CursorCoordinate();
            A.CallTo(() => this.nativeMethodWrapper.ClientToScreen(this.hWnd, ref _))
             .Returns(true)
             .AssignsOutAndRefParameters(new CursorCoordinate
                 {
                     X = this.offset.X,
                     Y = this.offset.Y
                 });
        }

        [Test]
        public void LeftDown_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.LeftDown();

            A.CallTo(() => this.innerMouse.LeftDown()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void LeftUp_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.LeftUp();

            A.CallTo(() => this.innerMouse.LeftUp()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void LeftDown_at_position_should_proxy_inner_mouse()
        {
            var position = new Coordinate(1024, 768);
            this.windowRelativeMouse.LeftDown(position);

            A.CallTo(() => this.innerMouse.LeftDown(position + this.offset)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void LeftUp_at_position_should_proxy_inner_mouse()
        {
            var position = new Coordinate(1024, 768);
            this.windowRelativeMouse.LeftUp(position);

            A.CallTo(() => this.innerMouse.LeftUp(position + this.offset)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RightDown_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.RightDown();

            A.CallTo(() => this.innerMouse.RightDown()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RightUp_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.RightUp();

            A.CallTo(() => this.innerMouse.RightUp()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RightDown_at_position_should_proxy_inner_mouse()
        {
            var position = new Coordinate(1024, 768);
            this.windowRelativeMouse.RightDown(position);

            A.CallTo(() => this.innerMouse.RightDown(position + this.offset)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RightUp_at_position_should_proxy_inner_mouse()
        {
            var position = new Coordinate(1024, 768);
            this.windowRelativeMouse.RightUp(position);

            A.CallTo(() => this.innerMouse.RightUp(position + this.offset)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void MiddleDown_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.MiddleDown();

            A.CallTo(() => this.innerMouse.MiddleDown()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void MiddleUp_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.MiddleUp();

            A.CallTo(() => this.innerMouse.MiddleUp()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void MiddleDown_at_position_should_proxy_inner_mouse()
        {
            var position = new Coordinate(1024, 768);
            this.windowRelativeMouse.MiddleDown(position);

            A.CallTo(() => this.innerMouse.MiddleDown(position + this.offset)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void MiddleUp_at_position_should_proxy_inner_mouse()
        {
            var position = new Coordinate(1024, 768);
            this.windowRelativeMouse.MiddleUp(position);

            A.CallTo(() => this.innerMouse.MiddleUp(position + this.offset)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void MoveCursor_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.MoveCursor(1, 2);

            A.CallTo(() => this.innerMouse.MoveCursor(1, 2)).MustHaveHappened();
        }

    }
}