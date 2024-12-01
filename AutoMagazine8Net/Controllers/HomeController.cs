
using AutoMagazine8Net.Data;
using AutoMagazine8Net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoMagazine8Net.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext db;
        int pageSize = 6;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, StoreContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index(int catId, int page = 1)
        {
            ViewBag.Title = "Главная страница";
            if(catId > 0)
            {
                return View(new IndexViewModel
                {
                    PageName = "Все автомобили",
                    PageInfo = new PageInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = db.Cars.Where(c => c.CategoryId == catId).Count()
                    },
                    Cars = db.Cars.Where(p => p.CategoryId == catId).OrderBy(c => c.CarId).Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                    CurrentCategory = catId
                });
            }
            else
            {
                return View(new IndexViewModel
                {
                    PageName = "Все автомобили",
                    PageInfo = new PageInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = db.Cars.Count()
                    },
                    Cars = db.Cars.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                    CurrentCategory = catId
                });
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
