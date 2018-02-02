using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class DishComponentCoupling
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public long DishComponentID { get; set; }
        public virtual DishComponent DishComponent { get; set; }

        public long DishID { get; set; }
        public virtual Dish Dish { get; set; }

        public string Notes { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
