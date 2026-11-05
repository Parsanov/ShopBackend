using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class ShoesFilterQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public byte? Size { get; set; }
        public string? Search { get; set; }
        public string? Sort { get; set; }
    }
}
