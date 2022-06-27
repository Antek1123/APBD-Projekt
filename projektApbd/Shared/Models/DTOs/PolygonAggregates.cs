using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models.DTOs
{
    public class PolygonAggregatesResponse
    {
        public List<PolygonAggregates> Results { get; set; }
    }

    public class PolygonAggregates
    {
        public decimal C { get; set; }
        public decimal H { get; set; }
        public decimal L { get; set; }
        public decimal O { get; set; }
        public long T { get; set; }
        public decimal V { get; set; }
    }
}
