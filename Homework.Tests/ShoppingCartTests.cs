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
        [Fact()]
        public void CheckOutTest()
        {

            //// Arrange 
            var excepted = 100;
            var target = new ShoppingCart();
            var bookList = new List<Book>();
            //// Act
            var actual = target.CheckOut(bookList);            
                
            //// Assert
            Assert.True(false, "This test needs an implementation");
        }
    }
}