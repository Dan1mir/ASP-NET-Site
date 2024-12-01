using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMagazine8Net.Data.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [Display(Name = "Имя")]
        [StringLength(50)]
        [Required(ErrorMessage = "Вы не заполнили все поля")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        [StringLength(50)]
        [Required(ErrorMessage = "Вы не заполнили все поля")]
        public string SurName { get; set; }
        [Display(Name = "Номер телефона")]
        [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Вы не заполнили все поля")]
        public string Phone { get; set; }
        [Display(Name = "Почта")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Вы не заполнили все поля")]
        public string Email { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }
        public List<OrderDetail>? OrderDetail { get; set; }
    }
}
