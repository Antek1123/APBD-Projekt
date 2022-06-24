using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models
{
    [Table("DailyOpenClose")]
    public class DailyOpenClose
    {
        [Required]
        [ForeignKey("Company")]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Open { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal High { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Low { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Close { get; set; }

        [Required]
        [MaxLength(255)]
        public string Volume { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? AfterHours { get; set; }

        public int? PreMarket { get; set; }

        public virtual Company Company { get; set; }
    }
}
