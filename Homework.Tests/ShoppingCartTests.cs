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

        #region
        ////// Scenario:檢查總共不能超過 30 本
        ////// Given:購物車已有書本 0 本
        ////// When:購物車結帳時
        ////// Then:檢查通過

        ////// Scenario:檢查總共不能超過 30 本
        ////// Given:購物車已有書本 1 本
        ////// When:購物車結帳時
        ////// Then:檢查通過

        ////// Scenario:檢查總共不能超過 30 本
        ////// Given:購物車已有書本 30 本
        ////// When:購物車結帳時
        ////// Then:檢查通過

        ////// Scenario:檢查總共不能超過 30 本
        ////// Given:購物車已有書本 1 本
        ////// When:購物車結帳時
        ////// Then:檢查不通過
        #endregion

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(30, true)]
        [InlineData(31, false)]
        public void BookList_limit_is_30_books_Test(int bookCount, bool expected)
        {
            //// Arrange
            var target = new StubShoppingCart();
            var bookList = new List<Book>();
            for (int i = 0; i < bookCount; i++)
            {
                bookList.Add(new Book
                {
                    price = 100,
                    episode = 1
                });
            }

            //// Act
            target.CheckOut(bookList);
            var actual = target.isChecked;

            //// Assert
            actual.Should().Be(expected);
        }

        #region
        ////// Scenario:檢查總共不能超過 30 本
        ////// Given:購物車已有書本 31 本
        ////// When:購物車結帳時
        ////// Then:拋出例外
        #endregion

        [Fact]
        public void BookList_Over_30_Should_Throw_Exception_Test()
        {
            //// Arrange
            var target = new ShoppingCart();
            var bookList = new List<Book>();
            for (int i = 0; i < 31; i++)
            {
                bookList.Add(new Book
                {
                    price = 100,
                    episode = 1
                });
            }

            //// Act
            Action act = () => target.CheckOut(bookList);

            //// Assert
            act.ShouldThrow<ApplicationException>("書本超過上限");
        }

        #region
        ////// Scenario: 每一集最多買 5 本
        ////// Given: 購物車已有書本 1,2,3,4,5 集各1本
        ////// When: 購物車結帳時
        ////// Then: 檢查通過

        ////// Scenario: 每一集最多買 5 本
        ////// Given: 購物車已有書本 1 集 6 本
        ////// When: 購物車結帳時
        ////// Then: 檢查不通過
        #endregion

        [Theory]
        [InlineData("1,2,3,4,5", true)]
        [InlineData("1,1,1,1,1,1", false)]
        public void Same_Episode_Up_to_5_Test(string bookEpisodeList, bool expected)
        {
            //// Arrange
            var target = new StubShoppingCart();
            var actual = false;
            var bookList = bookEpisodeList.Split(',');
            var checkoutList = new List<Book>();
            for (int i = 0; i < bookList.Length; i++)
            {
                checkoutList.Add(new Book
                {
                    price = 100,
                    episode = int.Parse(bookList[i])
                });
            }
            //// Act
            target.CheckOut(checkoutList);
            actual = target.isChecked;
            //// Assert
            actual.Should().Be(expected);
        }

        private class StubShoppingCart : ShoppingCart
        {
            public bool isLoged;
            public bool isChecked;

            protected override void Log(decimal totalPrice)
            {
                base.Log(totalPrice);
                this.isLoged = true;
            }

            protected override void VaildateBooks(List<Book> bookList)
            {
                try
                {
                    base.VaildateBooks(bookList);
                    this.isChecked = true;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}