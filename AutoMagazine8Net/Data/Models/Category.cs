using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMagazine8Net.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
