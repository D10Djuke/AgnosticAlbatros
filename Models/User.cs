using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class User
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }

        public bool Archived { get; set; }

        public string Email { get; set; }
        public string PassWord { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }

        public bool Admin { get; set; }

        public DateTime LastSeen { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public long UserTitleID { get; set; }
        public virtual UserTitle UserTitle { get; set; }
        public long KitchenID { get; set; }
        public virtual Kitchen Kitchen { get; set; }
        public long CompanyID { get; set; } 
        public virtual Company Company { get; set; }
    }
}
