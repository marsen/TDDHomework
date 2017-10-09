using Xunit;
using HomeWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Tests
{
    public class CheckoutShippingTests
    {
        [Fact()]
        public void CheckOut_Shipping_With_BlackCat_ShippingFee_is_100()
        {

            //// Arrange 
            var target = new ShoppingCart();
            var excepted = "BlackCat";
            var exceptedFee = 100;
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
            var actual = target.ShippingType;
            var actualFee = target.ShippingFee;

            //// Assert
            Assert.Equal(excepted, actual);
            Assert.Equal(exceptedFee, actualFee);
        }

    }
}