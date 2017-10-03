using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork;
using Xunit;

namespace Homework.Tests
{
    public class ShoppingCarShipping
    {
        [Theory]
        [InlineData(5)]
        public void  GetShippingTypeQty5GetCatTest(int Qty)
        {
            //// Arrange
            var exceptShippingTyps = new List<string>() { "BlackCat" };
            var shippingService = new ShippingService();

            //// Act
            var shippingTypes = shippingService.GetShippingType(Qty);
            
            //// Assert
            Assert.Equal(exceptShippingTyps, shippingTypes);
        }
    }
}
