using System.ComponentModel.DataAnnotations;

namespace projektApbd.Shared.Models
{
    internal class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Listdate { get; set; }

        [Required]
        [MaxLength(10)]
        public string Ticker { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [DataType(DataType.Url)]
        public string Url { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Sector { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Logo { get; set; } = string.Empty;

        [Required]
        public int Employees { get; set; }

        [Required]
        [MaxLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Ceo { get; set; } = string.Empty;

        [Required]
        [MaxLength(4000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(3)]
        public string Exchange { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? HqAddress { get; set; }

        [MaxLength(3)]
        public string? HqState { get; set; }

        [MaxLength(3)]
        public string? HqCountry { get; set; }

        [Required]
        [MaxLength(10)]
        public Boolean Active { get; set; }
    }
}
