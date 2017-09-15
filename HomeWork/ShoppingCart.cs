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
            var packageList = this.PackageBookList(bookList);
            var totalPrice = 0m;
            foreach (var item in packageList)
            {
                var packagePrice = item.Sum(x => x.price);
                var totalEpisode = item.GroupBy(x => x.episode).Count();
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
                    case 5:
                        discount = 0.25m;
                        break;
                    default:
                        break;
                }
                totalPrice += (1 - discount) * packagePrice;                
            }
            return totalPrice;
        }

        private List<List<Book>> PackageBookList(List<Book> bookList)
        {
            var result = new List<List<Book>>();
            const int lastEpisode = 5;
            var sortedBookList = bookList.OrderBy(x => x.episode);
            for (int i = 0; i < lastEpisode; i++)
            {
                var sameEpisodeBooks = bookList.Where(x => x.episode == i + 1).ToList();
                var diff = sameEpisodeBooks.Count - result.Count;                
                for (int j = 0; j < diff; j++)
                {
                    result.Add(new List<Book>());
                }
                for (int k = 0; k < sameEpisodeBooks.Count; k++)
                {
                    result.ElementAt(k).Add(sameEpisodeBooks.ElementAt(k));
                }
            }
            

            return result;
        }
    }
}
