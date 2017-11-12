using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HomeWork
{
    /// <summary>
    /// ShoppingCart
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// 目前最大集數
        /// </summary>
        private const int LastEpisode = 5;

        private Dictionary<int, decimal> DiscountDic
        {
            get
            {
                return new Dictionary<int, decimal>()
                {
                    { 1, 0m },
                    { 2, .05m },
                    { 3, .1m},
                    { 4, .2m},
                    { 5, .25m}
                };
            }
        }

        /// <summary>
        /// CheckOut
        /// </summary>
        /// <param name="bookList">bookList</param>
        /// <returns>price</returns>
        public decimal CheckOut(List<Book> bookList)
        {
            //Checked BookList
            this.VaildateBooks(bookList);
            var packageList = this.PackageBookList(bookList);
            var totalPrice = 0m;

            foreach (var item in packageList)
            {
                var packagePrice = item.Sum(x => x.price);
                var totalEpisode = item.GroupBy(x => x.episode).Count();
                var discount = this.DiscountDic[totalEpisode];
                totalPrice += (1 - discount) * packagePrice;
            }
            // 物流 功能
            var shipping = new Shipping()
            {
                Type = ShippingTypeEnum.BlackCat,
                Fee = 100
            };
            this.ShippingList.Add(shipping);
            if (bookList.Count < 5)
            {
                this.ShippingList.Add(new Shipping()
                {
                    Type = ShippingTypeEnum.Post,
                    Fee = 50
                });
            }
            // 金流 功能
            this.Log(totalPrice);

            return totalPrice;
        }

        protected virtual void VaildateBooks(List<Book> bookList)
        {
            if (bookList.Count > 30)
            {
                throw new ApplicationException("Up to 30 books");
            }
        }

        protected virtual void Log(decimal totalPrice)
        {
            File.WriteAllText("D:\\Log.txt", totalPrice.ToString());
        }

        public List<Shipping> GetShippingList()
        {
            return this.ShippingList;
        }

        /// <summary>
        /// 購物車可用的物流方式
        /// </summary>
        private List<Shipping> ShippingList = new List<Shipping>();

        /// <summary>
        /// 將不同的集數的書合成一套打包,
        /// EX: 所有集數為 1,1,2,2,3,4,5
        /// 會打包成 1,2,3,4,5 與 1,2 兩包
        /// </summary>
        /// <param name="bookList">bookList</param>
        /// <returns>打包後的書</returns>
        private List<List<Book>> PackageBookList(List<Book> bookList)
        {
            var result = new List<List<Book>>();
            var sortedBookList = bookList.OrderBy(x => x.episode);
            for (int i = 0; i < LastEpisode; i++)
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