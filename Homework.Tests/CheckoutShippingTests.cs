using System.Collections.Generic;
using Xunit;

namespace HomeWork.Tests
{
    public class CheckoutShippingTests
    {
        [Fact()]
        public void CheckOut_Shipping_With_BlackCat_ShippingFee_is_100()
        {
            //// Arrange
            var target = new ShoppingCart();
            var excepted = new Shipping()
            {
                Type = "BlackCat",
                Fee = 100
            };
            var bookList = new List<Book>
            {
                new Book
                {
                    price = 100,
                    episode = 1
                }
            };

            //// Act
            target.CheckOut(bookList);
            var actual = target.Shipping;

            //// Assert
            Assert.Equal(excepted, actual);
        }
    }
}