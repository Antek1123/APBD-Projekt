using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models.DTOs
{
    public class PolygonResponse
    {
        public PolygonCompany results { get; set; }
    }

    public class PolygonCompany
    {
        public int Id { get; set; }
        public string Ticker { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Homepage_url { get; set; } = string.Empty;

        public string Locale { get; set; } = string.Empty;

        public Branding branding { get; set; }

        public string Phone_Number { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Currency_Name { get; set; } = string.Empty;

        public Boolean Active { get; set; }
    }

    public class Branding
    {
        public string Logo_Url { get; set; } = string.Empty;    
    }
}
