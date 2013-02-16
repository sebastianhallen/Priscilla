namespace Pricilla.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class PricillaIntegrationsTests
    {
        private readonly IPricilla pricilla = new Pricilla();

        [Test]
        public void Set_cursor_position_and_left_click()
        {            
            this.pricilla.MoveTo(new Coordinate(100, 100));
            this.pricilla.LeftClick();
        }

    }
}
