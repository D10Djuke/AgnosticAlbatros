using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgnosticAlbatros.Models
{
    public class UserTitle
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public Guid Guid { get; set; }
        
        public bool Archived { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual List<UserTitle> UserTitles { get; set; }
    }
}
