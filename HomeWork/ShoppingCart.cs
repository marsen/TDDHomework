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
            if (totalEpisode == 2)
            {
                totalPrice = (1-0.05m) * totalPrice;
            }
            else if (totalEpisode == 3)
            {
                totalPrice = (1 - 0.1m) * totalPrice;
            }
            else if (totalEpisode == 4)
            {
                totalPrice = (1 - 0.2m) * totalPrice;
            }
            return totalPrice;
        }
    }
}
