namespace Priscilla.Test
{
    using FakeItEasy;
    using NUnit.Framework;
    using Priscilla.Extension;

    [TestFixture]
    public class PriscillaIntegrationsTests
    {
        private IMouse mouse;

        [SetUp]
        public void Before()
        {
            this.mouse = A.Fake<IMouse>(wrapped => wrapped.Wrapping(new Mouse()));
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

        [TestCase(50, 75)]
        [TestCase(-50, -75)]
        public void Should_be_possible_to_specify_a_position_offset_for_move_operations(int xOffset, int yOffset)
        {
            this.mouse.MoveTo(new Coordinate(100, 100), offset: new Coordinate(xOffset, yOffset));

            var position = this.mouse.FindCursor();
            Assert.That(position.X, Is.EqualTo(100 + xOffset));
            Assert.That(position.Y, Is.EqualTo(100 + yOffset));
        }

        [Test]
        public void Should_be_possible_to_specify_a_position_offset_for_drag_operations()
        {
            this.mouse.DragAndDrop(new Coordinate(100, 100), new Coordinate(200, 200), offset: new Coordinate(50, 50));

            var position = this.mouse.FindCursor();
            A.CallTo(() => this.mouse.PositionCursor(A<Coordinate>.That.Matches(coordinate => coordinate.X == 150 && coordinate.Y == 150))).MustHaveHappened();
            A.CallTo(() => this.mouse.PositionCursor(A<Coordinate>.That.Matches(coordinate => coordinate.X < 150 || coordinate.Y < 150))).MustNotHaveHappened();
            Assert.That(position.X, Is.EqualTo(250));
            Assert.That(position.Y, Is.EqualTo(250));
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
