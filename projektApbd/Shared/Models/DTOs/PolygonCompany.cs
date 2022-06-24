using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models.DTOs
{
    public class PolygonCompany
    {
        public DateTime List_Date { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Homepage_url { get; set; } = string.Empty;

    }

    public class PolygonResponse
    {
        public PolygonCompany Results { get; set; }
    }
}
