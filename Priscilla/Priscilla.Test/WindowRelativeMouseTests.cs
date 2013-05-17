namespace Priscilla.Test
{
    using System;
    using FakeItEasy;
    using NUnit.Framework;

    [TestFixture]
    public class WindowRelativeMouseTests
    {
        private IMouse windowRelativeMouse;
        private IMouse innerMouse;

        [SetUp]
        public void Before()
        {
            this.innerMouse = A.Fake<IMouse>();
            this.windowRelativeMouse = new WindowRelativeMouse(IntPtr.Zero, this.innerMouse);
        }

        [Test]
        public void LeftDown_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.LeftDown();

            A.CallTo(() => this.innerMouse.LeftDown()).MustHaveHappened();
        }

        [Test]
        public void LeftUp_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.LeftUp();

            A.CallTo(() => this.innerMouse.LeftUp()).MustHaveHappened();
        }

        [Test]
        public void RightDown_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.RightDown();

            A.CallTo(() => this.innerMouse.RightDown()).MustHaveHappened();
        }

        [Test]
        public void RightUp_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.RightUp();

            A.CallTo(() => this.innerMouse.RightUp()).MustHaveHappened();
        }

        [Test]
        public void MiddleDown_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.MiddleDown();

            A.CallTo(() => this.innerMouse.MiddleDown()).MustHaveHappened();
        }

        [Test]
        public void MiddleUp_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.MiddleUp();

            A.CallTo(() => this.innerMouse.MiddleUp()).MustHaveHappened();
        }

        [Test]
        public void MoveCursor_should_proxy_inner_mouse()
        {
            this.windowRelativeMouse.MoveCursor(1, 2);

            A.CallTo(() => this.innerMouse.MoveCursor(1, 2)).MustHaveHappened();
        }   
    }
}
