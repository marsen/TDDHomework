using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    public class ShoppingCart
    {
        public decimal CheckOut(List<Book> bookList)
        {
            var totalPrice = bookList.Sum(x => x.price);
            var totalEpisode = bookList.GroupBy(x => x.episode).Count();
            var discount = 0m;
            switch (totalEpisode)
            {
                case 2:
                    discount = 0.05m;
                    break;
                case 3:
                    discount = 0.1m;
                    break;
                case 4:
                    discount = 0.2m;
                    break;
                default:
                    break;
            }
            totalPrice = (1 - discount) * totalPrice;
            return totalPrice;
        }
    }
}
