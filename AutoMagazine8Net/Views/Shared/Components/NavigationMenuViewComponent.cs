using AutoMagazine8Net.Data;
using Microsoft.AspNetCore.Mvc;

namespace AutoMagazine8Net.Views.Shared.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public StoreContext db;
        public NavigationMenuViewComponent(StoreContext context)
        {
            db = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["catId"];
            return View(db.Categories.ToList());
        }
    }
}
