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
        public void CheckOutTest(List<Book> bookList,decimal excepted)
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
                    new object[] { new List<Book> { new Book { price = 100, episode = 1 } } ,100 }
                };
            }
        }
    }
}