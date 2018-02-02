using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class Ingredient
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public bool Archived { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public long StoreID { get; set; }
        public virtual Store Store { get; set; }
    }
}