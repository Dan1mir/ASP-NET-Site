using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMagazine8Net.Data.Models.Cart
{
    public class ShopCart
    {
        private StoreContext db;
        public ShopCart(StoreContext context)
        {
            db = context;
        }

        public string ShopCartId { get; set; } 
        public List<ShopCartItem> ListShopItems { get; set; } 

        public static ShopCart GetCart(IServiceProvider service)
        {
            
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<StoreContext>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shopCartId); 
            return new ShopCart(context)
            {
                ShopCartId = shopCartId
            };
        }

        public void AddToCart(Car car)
        {
            db.ShopCart.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                Car = car,
                Price = car.Price,
            });
            db.SaveChanges();
        }

        public List<ShopCartItem> GetShopItems()
        {
            return db.ShopCart.Where(c => c.ShopCartId == ShopCartId).Include(cr=>cr.Car).ToList();
        }

        public void ClearCart() {
            ListShopItems = new List<ShopCartItem>();
            db.ShopCart.RemoveRange(db.ShopCart.Where(c => c.ShopCartId == ShopCartId));
            db.SaveChanges(); 
        }
    }
}
