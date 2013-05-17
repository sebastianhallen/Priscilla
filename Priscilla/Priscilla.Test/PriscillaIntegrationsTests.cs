namespace Priscilla.Test
{
    using FakeItEasy;
    using NUnit.Framework;
    using Priscilla.Extension;
    using Priscilla.Native;

    [TestFixture, Explicit]
    public class WindowBoundPriscillaIntegrationTests
        : PriscillaIntegrationsTests
    {
        protected override IMouse CreateMouse()
        {
            IApplicationWindowFinder windowFinder = new ApplicationWindowFinder();

            var viewport = windowFinder.FindWindow("Chrome_WidgetWin_1")
                                       .FindChildWindow("Chrome_WidgetWin_0")
                                       .FindChildWindow("Chrome_RenderWidgetHostHWND");

            return new WindowBoundMouse(viewport);
        }
    }

    [TestFixture, Explicit]
    public class WindowRelativePriscillaIntegrationTests
        : PriscillaIntegrationsTests
    {
        protected override IMouse CreateMouse()
        {
            IApplicationWindowFinder windowFinder = new ApplicationWindowFinder();
            var viewport = windowFinder.FindWindow("Chrome_WidgetWin_1")
                                       .FindChildWindow("Chrome_WidgetWin_0")
                                       .FindChildWindow("Chrome_RenderWidgetHostHWND");

            return new WindowRelativeMouse(viewport, new Mouse());
        }
    }

    [TestFixture]
    public class DefaultPriscillaIntegrationTests
        : PriscillaIntegrationsTests
    {
        protected override IMouse CreateMouse()
        {
            return new Mouse();
        }
    }
    
    public abstract class PriscillaIntegrationsTests
    {
        private IMouse mouse;

        protected abstract IMouse CreateMouse();

        [SetUp]
        public void Before()
        {
            this.mouse = A.Fake<IMouse>(wrapped => wrapped.Wrapping(this.CreateMouse()));
            A.CallTo(() => this.mouse.LeftDown()).Invokes(_ => { });
        }

        [TestCase(0, 0)]
        [TestCase(100, 100)]
        [TestCase(256, 256)]
        [TestCase(512, 128)]
        [TestCase(333, 111)]
        public void Mouse_should_be_at_specified_coordinates_after_it_has_been_moved(int x, int y)
        {
            this.mouse.MoveTo(new Coordinate(x, y), MovementSpeed.Fast);

            var position = this.mouse.FindCursor();
            Assert.That(position.X, Is.EqualTo(x));
            Assert.That(position.Y, Is.EqualTo(y));
        }

        [TestCase(300, 300, 100, 100)]
        [TestCase(200, 200, 400, 400)]
        [TestCase(400, 100, 100, 400)]
        [TestCase(100, 400, 400, 100)]
        [TestCase(200, 200, 400, 200)]
        [TestCase(400, 200, 200, 200)]
        [TestCase(200, 200, 200, 400)]
        [TestCase(200, 400, 200, 200)]
        [TestCase(0, 0, 0, 0)]
        [TestCase(100, 100, 100, 100)]
        public void Drag_and_drop_should_end_at_target_position(int fromX, int fromY, int toX, int toY)
        {
            this.mouse.DragAndDrop(new Coordinate(fromX, fromY), new Coordinate(toX, toY), movementSpeed: MovementSpeed.Fast);

            var position = this.mouse.FindCursor();
            Assert.That(position.X, Is.EqualTo(toX));
            Assert.That(position.Y, Is.EqualTo(toY));
        }

        //[Test]
        //public void Should_be_possible_to_move_the_cursor_a_relative_distance()
        //{
        //    this.mouse.MoveTo(new Coordinate(100, 100));

        //    this.mouse.MoveCursor(75, 175);

        //    var currentPosition = this.mouse.FindCursor();
        //    Assert.That(currentPosition, Is.EqualTo(new Coordinate(175, 25)));
        //}

        [Test]
        public void Should_find_the_cursor_where_I_left_it()
        {
            var coordinate = new Coordinate(123, 321);
            this.mouse.MoveTo(coordinate);

            var position = this.mouse.FindCursor();

            Assert.That(position, Is.EqualTo(coordinate));
        }
    }
}
