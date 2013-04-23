namespace Priscilla.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class CoordinateTests
    {
        [Test]
        public void Should_be_equal_when_x_and_y_coordinates_match()
        {
            var a = new Coordinate(10, 10);
            var b = new Coordinate(10, 10);

            Assert.That(a.Equals(b));
        }

        [TestCase(100, 10)]
        [TestCase(100, 100)]
        [TestCase(10, 100)]
        public void Should_not_be_equal_when_coordinates_differs(int otherX, int otherY)
        {
            var a = new Coordinate(10, 10);
            var b = new Coordinate(otherX, otherY);

            Assert.That(a.Equals(b), Is.False);
        }

        [Test]
        public void Should_not_be_equal_when_other_coordinate_is_null()
        {
            Coordinate other = null;

            Assert.That(new Coordinate(0, 0).Equals(other), Is.False);
        }

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

        [Test]
        public void Should_get_the_same_hash_code_with_the_same_coordinates()
        {
            var a = new Coordinate(10, 10);
            var b = new Coordinate(10, 10);

            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void Should_not_get_the_same_hash_with_inverted_coordinates()
        {
            var a = new Coordinate(10, 100);
            var b = new Coordinate(100, 10);

            Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
        }

        [TestCase(0, 23)]
        [TestCase(42, 0)]
        public void Should_not_get_zero_hash_when_one_coordinate_is_zero(int x, int y)
        {
            var c = new Coordinate(x, y);

            Assert.That(c.GetHashCode(), Is.Not.EqualTo(0));
        }

        [Test]
        public void Should_override_ToString()
        {
            var str = new Coordinate(23, 42).ToString();

            Assert.That(str, Is.EqualTo("(x:23|y:42)"));
        }
    }
}
