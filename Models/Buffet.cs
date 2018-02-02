using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class Buffet
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public long TotalPrice { get; set; }

        public bool Archived { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
