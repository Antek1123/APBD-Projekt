using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projektApbd.Shared.Models.DTOs
{
    internal class Company
    {
        public int Id { get; set; }

        public DateTime Listdate { get; set; }

        public string Ticker { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Industry { get; set; } = string.Empty;

        public string Sector { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;

        public int Employees { get; set; }

        public string Phone { get; set; } = string.Empty;

        public string Ceo { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Exchange { get; set; } = string.Empty;

        public string? HqAddress { get; set; }

        public string? HqState { get; set; }

        public string? HqCountry { get; set; }

        public Boolean Active { get; set; }
    }
}
