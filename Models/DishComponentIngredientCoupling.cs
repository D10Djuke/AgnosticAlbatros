using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class DishComponentIngredientCoupling
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public long IngredientID { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        public long DichComponentID { get; set; }
        public virtual DishComponent DishComponent { get; set; }

        public string Notes { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
