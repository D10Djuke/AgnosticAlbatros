using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class Dish
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public bool Archived { get; set; }
        public string Name { get; set; }
        public int PricePerPerson { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Preparation { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public long KitchenID { get; set; }
        public virtual Kitchen Kitchen { get; set; }
    }
}
