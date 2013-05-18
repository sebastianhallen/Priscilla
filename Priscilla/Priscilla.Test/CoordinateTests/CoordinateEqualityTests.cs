namespace Priscilla.Test.CoordinateTests
{
    using NUnit.Framework;
    using Coordinate = Priscilla.Coordinate;

    [TestFixture]
    public class CoordinateEqualityTests
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
    }
}
