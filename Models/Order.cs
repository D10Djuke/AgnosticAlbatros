using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class Order
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string Notes { get; set; }
    }
}
