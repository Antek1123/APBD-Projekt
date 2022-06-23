using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(320)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        public virtual ICollection<UserCompany> UserCompanies { get; set; }
    }
}