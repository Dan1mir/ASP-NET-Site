using AutoMagazine8Net.Data;
using AutoMagazine8Net.Data.Models;
using AutoMagazine8Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

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
        // 𐐘⚔ඞ
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

        [HttpGet("api/Car/{id}")]
        public IActionResult GetCar(int id)
        {
            var car = db.Cars.FirstOrDefault(p => p.CarId == id);

            if (car == null)
            {
                return NotFound(new { message = "Car not found" });
            }

            var result = new
            {
                car.CarId,
                car.Name,
                car.Price,
                Category = new
                {
                    car.CategoryId,
                }
            };

            return Ok(result);
        }

        [HttpPost("api/Cars")]
        public IActionResult CreateCar([FromBody] Car car) 
        { 
            if (car == null) 
            { 
                return BadRequest(new { message = "Invalid data" }); 
            } 
            db.Cars.Add(car); 
            db.SaveChanges(); 
            return CreatedAtAction(nameof(GetCars), new { id = car.CarId }, car); 
        }
        [HttpPut("api/Car/{id}")]
        public IActionResult UpdateCar(int id, [FromBody] Car updatedCar)
        {
            if (updatedCar == null || updatedCar.CarId != id)
            {
                return BadRequest(new { message = "Invalid data" });
            }

            var car = db.Cars.FirstOrDefault(p => p.CarId == id);
            if (car == null)
            {
                return NotFound(new { message = "Car not found" });
            }

            car.Name = updatedCar.Name;
            car.Price = updatedCar.Price;
            car.CategoryId = updatedCar.CategoryId;

            db.SaveChanges();

            return Ok(new { message = "Car updated successfully" });
        }
        [HttpDelete("api/Car/{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = db.Cars.FirstOrDefault(p => p.CarId == id);
            if (car == null)
            {
                return NotFound(new { message = "Car not found" });
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(new { message = "Car deleted successfully" });
        }
        // ඞ

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
