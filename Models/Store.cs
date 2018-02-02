using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class Store
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public bool Archived { get; set; }
        public string Name { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public long CityID { get; set; }
        public virtual City City { get; set; }
    }
}
