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
        [HttpGet("api/Categories")]
        public IActionResult GetCategories()
        {
            var categories = db.Categories.ToList();

            var result = new
            {
                Categories = categories.Select(c => new
                {
                    c.CategoryId,
                    c.Name,
                    c.Desc,
                    Cars = c.Cars.Select(car => new
                    {
                        car.CarId,
                        car.Name,
                        car.Price
                    })
                })
            };

            return Ok(result);
        }

        [HttpGet("api/Category/{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            var result = new
            {
                category.CategoryId,
                category.Name,
                category.Desc,
                Cars = category.Cars.Select(car => new
                {
                    car.CarId,
                    car.Name,
                    car.Price
                })
            };

            return Ok(result);
        }

        [HttpPost("api/Categories")]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new { message = "Invalid data" });
            }

            db.Categories.Add(category);
            db.SaveChanges();
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        [HttpPut("api/Category/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category updatedCategory)
        {
            if (updatedCategory == null || updatedCategory.CategoryId != id)
            {
                return BadRequest(new { message = "Invalid data" });
            }

            var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            category.Name = updatedCategory.Name;
            category.Desc = updatedCategory.Desc;
            category.Cars = updatedCategory.Cars;

            db.SaveChanges();

            return Ok(new { message = "Category updated successfully" });
        }

        [HttpDelete("api/Category/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found" });
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(new { message = "Category deleted successfully" });
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
