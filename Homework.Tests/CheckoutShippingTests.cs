using System.Collections.Generic;
using FluentAssertions;
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
            var excepted = new List<Shipping>() {
                new Shipping()
                {
                    Type = ShippingTypeEnum.BlackCat,
                    Fee = 100
                },
                new Shipping()
                {
                    Type = ShippingTypeEnum.Post,
                    Fee = 50
                }
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
            var actual = target.GetShippingList();

            //// Assert
            actual.ShouldBeEquivalentTo(excepted);
        }

        [Fact]
        public void CheckOut_Over_5_Books_Shipping_Only_BlackCat_Fee_is_100()
        {
            //// Arrange
            var target = new ShoppingCart();
            var excepted = new List<Shipping>() {
                new Shipping()
                {
                    Type = ShippingTypeEnum.BlackCat,
                    Fee = 100
                }
            };

            var bookList = new List<Book>
            {
                new Book
                {
                    price = 100,
                    episode = 1
                },
                new Book
                {
                    price = 100,
                    episode = 1
                },
                new Book
                {
                    price = 100,
                    episode = 1
                },
                 new Book
                {
                    price = 100,
                    episode = 1
                },
                new Book
                {
                    price = 100,
                    episode = 1
                }
            };

            //// Act
            target.CheckOut(bookList);
            var actual = target.GetShippingList();

            //// Assert
            actual.ShouldBeEquivalentTo(excepted);
        }
    }
}