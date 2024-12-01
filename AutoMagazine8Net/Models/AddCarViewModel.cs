using AutoMagazine8Net.Data.Models;

namespace AutoMagazine8Net.Models
{
    public class AddCarViewModel
    {
        public Car Car { get; set; }
        public List<Category> Categories { get; set; }
    }
}
