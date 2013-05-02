namespace Priscilla.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class CoordinateGetHashTests
    {
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
    }
}
