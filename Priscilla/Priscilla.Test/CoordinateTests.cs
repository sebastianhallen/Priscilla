namespace Priscilla.Test
{
    using System;
    using System.Drawing;
    using NUnit.Framework;

    [TestFixture]
    public class CoordinateTests
    {        
        [Test]
        public void Should_override_ToString()
        {
            var str = new Coordinate(23, 42).ToString();

            Assert.That(str, Is.EqualTo("(x:23|y:42)"));
        }

        [Test]
        public void Should_have_implicit_cast_to_Point()
        {
            Action<Point> f = point =>
                {
                    Assert.That(point.X, Is.EqualTo(10));
                    Assert.That(point.Y, Is.EqualTo(20));
                };

            var coordinate = new Coordinate(10, 20);

            f(coordinate);
        }
    }
}
