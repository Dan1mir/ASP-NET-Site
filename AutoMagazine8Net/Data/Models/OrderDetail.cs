namespace AutoMagazine8Net.Data.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int CarId { get; set; }
        public int Price { get; set; }
        public virtual Car Car { get; set; }
        public virtual Order Order { get; set; }
    }
}