using AutoMagazine8Net.Data;
using AutoMagazine8Net.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AutoMagazine8Net.Controllers
{
    public class CategoryController : Controller
    {
        StoreContext db;
        public CategoryController(StoreContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)  
        {
            if (category != null)
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            return RedirectToRoute(new { controller = "Category", action = "ListCategories" });
        }
        [HttpGet]
        public IActionResult ListCategories()
        {
            return View(db.Categories.ToList());
        }
        public IActionResult DeleteCategory(int? categoryId)
        {
            if (categoryId != null)
            {
                db.Categories.Remove(db.Categories.Find(categoryId));
                db.SaveChanges();
            }
            return RedirectToAction("ListCategories");
        }
        public IActionResult EditCategory(int? categoryId)
        {
            if (categoryId == null)
            {
                return RedirectToAction("ListCategories");
            }

            return View(db.Categories.Find(categoryId));
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (category != null)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListCategories");
        }
    }
}
