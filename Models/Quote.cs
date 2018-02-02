using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class Quote
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public DateTime DeliveryDate { get; set; }
        public DateTime ExpirationDate { get; set; } 

        public int GuestCount { get; set; }
        public long TotalPrice { get; set; }

        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public long CityID { get; set; }
        public virtual City City { get; set; }
        public long OrderID { get; set; }
        public Order Order { get; set; }
        public long ClientID { get; set; }
        public virtual Client Client { get; set; }

    }
}
