using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models.DTOs
{
    internal class DailyOpenClose
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public string Volume { get; set; } = string.Empty;

        public decimal? AfterHours { get; set; }

        public int? PreMarket { get; set; }
    }
}
