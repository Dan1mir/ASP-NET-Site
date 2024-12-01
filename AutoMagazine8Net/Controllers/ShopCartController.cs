using AutoMagazine8Net.Data.Models.Cart;
using AutoMagazine8Net.Data;
using Microsoft.AspNetCore.Mvc;

namespace AutoMagazine8Net.Controllers
{
    public class ShopCartController : Controller
    {
        StoreContext db;
        ShopCart _shopCart;
        public ShopCartController(StoreContext context, ShopCart shopCart)
        {
            db = context;
            _shopCart = shopCart;
        }
        [HttpGet]
        public IActionResult Cart()
        {
            _shopCart.ListShopItems = _shopCart.GetShopItems();
            return View(_shopCart.ListShopItems);
        }

        public RedirectToActionResult Add2Cart(int carId) 
        {
            var item = db.Cars.Find(carId);
            if (item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Cart");
        }
    }
}
