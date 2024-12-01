using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMagazine8Net.Data.Models.Cart
{
    public class ShopCartItem
    {
        public int ShopCartItemId { get; set; }
        public Car Car { get; set; }
        public int Price { get; set; }
        public string ShopCartId { get; set; }
    }
}
