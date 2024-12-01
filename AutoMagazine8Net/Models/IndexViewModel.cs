using AutoMagazine8Net.Data.Models;

namespace AutoMagazine8Net.Models
{
    public class IndexViewModel
    {
        public required string PageName { get; set; }
        public required PageInfo PageInfo { get; set; }
        public int CurrentCategory { get; set; }

        public required IEnumerable<Car> Cars { get; set; }
    }
}