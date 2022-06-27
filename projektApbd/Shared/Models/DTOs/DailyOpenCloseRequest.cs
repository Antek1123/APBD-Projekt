using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models.DTOs
{
    public class DailyOpenCloseRequest
    {
        public string Ticker { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
