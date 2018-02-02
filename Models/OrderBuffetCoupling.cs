using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class OrderBuffetCoupling
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public long OrderId { get; set; }
        public virtual Order Order { get; set; }

        public long BuffetID { get; set; }
        public virtual Buffet Buffet { get; set; }

        public string Notes { get; set; } 
        public int NumberOfPersons { get; set; }
        public double Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
