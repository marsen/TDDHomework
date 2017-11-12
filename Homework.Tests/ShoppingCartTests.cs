using Xunit;
using HomeWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace HomeWork.Tests
{
    public class ShoppingCartTests
    {
        [Theory]
        [MemberData(nameof(HaryyPotterData))]
        public void CheckOutTest(List<Book> bookList, decimal excepted)
        {
            //// Arrange
            var target = new ShoppingCart();

            //// Act
            var actual = target.CheckOut(bookList);

            //// Assert
            Assert.Equal(excepted, actual);
        }

        public static IEnumerable<object[]> HaryyPotterData
        {
            get
            {
                return new[] {
                    new object[] { new List<Book> {
                        new Book { price = 100, episode = 1 } } ,
                        100 },
                    new object[] { new List<Book> {
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 } } ,
                        200 * (1 - 0.05) },
                    new object[] { new List<Book> {
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 } } ,
                        300 * (1 - 0.1) },
                    new object[] { new List<Book> {
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 },
                        new Book { price = 100, episode = 4 } } ,
                        400 * (1 - 0.2) },
                    new object[] { new List<Book> {
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 },
                        new Book { price = 100, episode = 4 },
                        new Book { price = 100, episode = 5 }} ,
                        500 * (1 - 0.25) },
                    new object[] { new List<Book> {
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 },
                        new Book { price = 100, episode = 1 }} ,
                        300 * (1 - 0.1) + 100 },
                };
            }
        }

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

        #region

        ////// Scenario:寫交易記錄到檔案
        ////// Given:購物車已有書本
        ////// When:購物車結帳時
        ////// Then:寫交易記錄到檔案

        #endregion

        [Fact]
        public void Test_CheckOut_Log()
        {
            //// Arrange
            var target = new StubShoppingCart();
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
            var actual = target.isLoged;

            //// Assert
            Assert.True(actual);
        }

        private class StubShoppingCart : ShoppingCart
        {
            public bool isLoged;

            protected override void Log(decimal totalPrice)
            {
                this.isLoged = true;
                base.Log(totalPrice);
            }
        }
    }
}