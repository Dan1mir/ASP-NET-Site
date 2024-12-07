using AutoMagazine8Net.Data;
using AutoMagazine8Net.Data.Models;
using AutoMagazine8Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoMagazine8Net.Controllers
{
    public class CarController : Controller
    {
        StoreContext db;
        IWebHostEnvironment env;
        public CarController(StoreContext context, IWebHostEnvironment environment)
        {
            db = context;
            env = environment;
        }
        [HttpGet]
        public IActionResult AddCar()
        {
            return View(
                new AddCarViewModel
                {
                    Car = new Car(),
                    Categories = db.Categories.ToList()
                });
        }
        [HttpGet("api/Cars")]
        public IActionResult GetCars()
        {
            var cars = db.Cars.ToList();

            var result = new
            {
                Cars = cars.Select(p => new
                {
                    p.CarId,
                    p.Name,
                    p.Price,
                    Category = new
                    {
                        p.CategoryId,
                    }
                })
            };

            return Ok(result);
        }
    

        [HttpPost]
        public async Task<IActionResult> AddCar(Car car, IFormFile uploadedFile)
        {
            try
            {
                if (uploadedFile != null)
                {
                    string path = $"/img/{uploadedFile.FileName}";
                    car.Img = path;
                    using (var filestream = new FileStream(env.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(filestream);
                    }
                }
                else
                {
                    car.Img = "/img/none.png";
                }

                await db.Cars.AddAsync(car);
                await db.SaveChangesAsync();

                return RedirectToAction("ListCars");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return BadRequest("Ошибка");
            }
        }

        public IActionResult PageCar(int carId)
        {
            return View(db.Cars.Find(carId));
        }
        public IActionResult ListCars()
        {
            return View(db.Cars.ToList());
        }

        public IActionResult DeleteCar(int? carId)
        {
            if (carId != null)
            {
                db.Cars.Remove(db.Cars.Find(carId));
                db.SaveChanges();
            }
            return RedirectToAction("ListCars");
        }

        [HttpGet]
        public IActionResult EditCar(int? carId)
        {
            if (carId == null)
            {
                return RedirectToAction("Table");
            }
            else
            {
                return View(new AddCarViewModel
                {
                    Car = db.Cars.Find(carId),
                    Categories = db.Categories.ToList()
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditCar(Car car, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = $"/img/{uploadedFile.FileName}";
                car.Img = path;
                using (var filestream = new FileStream(env.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(filestream);
                }
            }
            else
            {
                car.Img = "/img/none.png";
            }

            db.Entry(car).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return RedirectToAction("ListCars");
        }

    }
}
