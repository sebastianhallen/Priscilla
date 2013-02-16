namespace Pricilla.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class PricillaIntegrationsTests
    {
        private IMouse mouse;
        private IPricilla pricilla;

        [SetUp]
        public void Before()
        {
            this.mouse = new Mouse();
            this.pricilla = new Pricilla(this.mouse);

        }

        [TestCase(0, 0)]
        [TestCase(100, 100)]
        [TestCase(256, 256)]
        [TestCase(512, 128)]
        [TestCase(333, 111)]
        public void Mouse_should_be_at_specified_coordinates_after_it_has_been_moved(int x, int y)
        {
            this.pricilla.MoveTo(new Coordinate(x, y));

            var position = this.mouse.FindCursor();
            Assert.That(position.X, Is.EqualTo(x));
            Assert.That(position.Y, Is.EqualTo(y));
        }

        [TestCase(300, 300, 200, 200)]
        [TestCase(200, 200, 300, 300)]
        [TestCase(200, 200, 400, 400)]
        [Explicit]
        public void Drag(int fromX, int fromY, int toX, int toY)
        {
            this.pricilla.DragAndDrop(new Coordinate(fromX, fromY), new Coordinate(toX, toY));            
        }
    }
}
