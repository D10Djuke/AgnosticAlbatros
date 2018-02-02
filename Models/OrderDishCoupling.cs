using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class OrderDishCoupling
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public long OrderID { get; set; }
        public virtual Order Order { get; set; }

        public long DishID { get; set; }
        public virtual Dish Dish { get; set; }

        public string Notes { get; set; }
        public int NumberOfPersons { get; set; }
        public double Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
