using Xunit;
using HomeWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Theory]
        [MemberData(nameof(DiscountData))]
        public void DiscountCheckOutTest(string memberType, List<Book> bookList, decimal excepted)
        {
            //// Arrange 
            var target = new ShoppingCart();

            //// Act
            var actual = target.DiscountCheckOut(memberType, bookList);

            //// Assert
            Assert.Equal(excepted, actual);
        }

        public static IEnumerable<object[]> DiscountData
        {
            get
            {
                return new[] {
                    //// VIP滿500打8折
                    new object[] { "VIP",
                        new List<Book> {
                            new Book { price = 100, episode = 1 },
                            new Book { price = 100, episode = 2 },
                            new Book { price = 100, episode = 3 },
                            new Book { price = 100, episode = 4 },
                            new Book { price = 100, episode = 5 },
                            new Book { price = 100, episode = 1 },
                            new Book { price = 100, episode = 2 },
                            new Book { price = 100, episode = 3 },
                            new Book { price = 100, episode = 4 },
                            new Book { price = 100, episode = 5 }} ,
                        1000 * (1 - 0.25) * 0.8 },
                    //// VIP未滿500不打折
                    new object[] { "VIP",
                        new List<Book> {
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 } } ,
                        300 * (1 - 0.1) },
                    //// 一般會員超過三件但未滿千不打折
                    new object[] { "Normal",
                        new List<Book> {
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 },
                        new Book { price = 100, episode = 4 },
                        new Book { price = 100, episode = 5 }} ,
                        500 * (1 - 0.25) },
                    //// 一般會員滿千但未超過3件不打折
                    new object[] { "Normal",
                        new List<Book> {
                        new Book { price = 500, episode = 1 },
                        new Book { price = 500, episode = 2 }} ,
                        1000 * (1 - 0.05) },
                    //// 一般會員滿千且超過3件打85折
                    new object[] { "Normal",
                        new List<Book> {
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 },
                        new Book { price = 100, episode = 4 },
                        new Book { price = 100, episode = 5 },
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 },
                        new Book { price = 100, episode = 4 },
                        new Book { price = 100, episode = 5 },
                        new Book { price = 100, episode = 1 },
                        new Book { price = 100, episode = 2 },
                        new Book { price = 100, episode = 3 },
                        new Book { price = 100, episode = 4 },
                        new Book { price = 100, episode = 5 }} ,
                        1500 * (1 - 0.25) * 0.85 }
                };
            }
        }
    }
}