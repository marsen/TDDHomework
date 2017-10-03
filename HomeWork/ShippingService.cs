using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork
{
    public class ShippingService
    {
        /// <summary>
        /// 取得物流方式
        /// </summary>
        /// <param name="qty"></param>
        /// <returns></returns>
        public List<string> GetShippingType(int qty)
        {
            return new List<string> { "BlackCat" };
        }
    }
}
