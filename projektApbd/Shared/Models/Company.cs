using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projektApbd.Shared.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Ticker { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Url)]
        public string Homepage_url { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Locale { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Logo_Url { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string Phone_Number { get; set; } = string.Empty;

        [Required]
        [MaxLength(4000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Currency_Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public Boolean Active { get; set; }

        public virtual ICollection<UserCompany> UserCompanies { get; set; }

        public virtual ICollection<DailyOpenClose> DailyOpenCloses { get; set; }
    }
}
