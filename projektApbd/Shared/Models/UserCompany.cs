using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models
{
    [Table("UserCompany")]
    public class UserCompany
    {
        [Required]
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [Key]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        
        public virtual User User { get; set; }

        public virtual Company Company { get; set; }
    }
}
