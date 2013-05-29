namespace Priscilla.Test
{
    using FakeItEasy;
    using NUnit.Framework;
 
    [TestFixture]
    internal class WindowRelativeMouseProxyTests
        : WindowRelativeMouseTests
    {
        [SetUp]
        public void Before()
        {
            A.CallTo(() => this.nativeMethodWrapper.GetForegroundWindow()).Returns(this.hWnd);
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
        public void MoveCursor_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.MoveCursor(1, 2);

            A.CallTo(() => this.innerMouse.MoveCursor(1, 2)).MustHaveHappened();
        }   
    }
}