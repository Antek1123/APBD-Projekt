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
        [Key]
        [ForeignKey("Company")]
        public int Id { get; set; }

        [Required]
        [Key]
        public DateTime Date { get; set; }

        [Required]
        public double Open { get; set; }

        [Required]
        public double High { get; set; }

        [Required]
        public double Low { get; set; }

        [Required]
        public double Close { get; set; }

        [Required]
        [MaxLength(255)]
        public string Volume { get; set; } = string.Empty;

        public double? AfterHours { get; set; }

        public int? PreMarket { get; set; }

        public virtual Company Company { get; set; }
    }
}
