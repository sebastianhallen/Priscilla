namespace Pricilla.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class PricillaIntegrationsTests
    {
        private readonly Pricilla pricilla = new Pricilla();

        [TestCase((uint)0, (uint)0)]
        [TestCase((uint)100, (uint)100)]
        [TestCase((uint)256, (uint)256)]
        [TestCase((uint)512, (uint)128)]
        [TestCase((uint)333, (uint)111)]
        public void Mouse_should_be_at_specified_coordinates_after_it_has_been_moved(uint x, uint y)
        {
            this.pricilla.MoveTo(new Coordinate(x, y));

            var position = this.pricilla.FindCursor();

            Assert.That(position.X, Is.EqualTo(x));
            Assert.That(position.Y, Is.EqualTo(y));
        }
    }
}
