namespace Priscilla.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class CoordinateMergingTests
    {
        [Test]
        public void Should_be_possible_to_add_coordinates()
        {
            var a = new Coordinate(100, 200);
            var b = new Coordinate(50, 75);

            var c = a + b;

            Assert.That(c.Equals(new Coordinate(150, 275)));
        }

        [Test]
        public void Should_get_same_coordinates_when_adding_with_null()
        {
            Coordinate other = null;
            var a = new Coordinate(100, 100);

            var b = other + a;

            Assert.That(b.Equals(new Coordinate(100, 100)));
        }

        [Test]
        public void Should_be_possible_to_subtract_coordinates()
        {
            var a = new Coordinate(100, 200);
            var b = new Coordinate(50, 75);

            var c = a - b;

            Assert.That(c.Equals(new Coordinate(50, 125)));
        }
    }
}
