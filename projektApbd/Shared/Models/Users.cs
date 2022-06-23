using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models
{
    [Table("Users")]
    internal class Users
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string Username { get; set; } = String.Empty;

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = String.Empty;

        [Required]
        [MaxLength(320)]
        public string Email { get; set; } = String.Empty;
    }
}