using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Shoes
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string VendorCode { get; set; }
        public decimal Weight { get; set; }
        public List<ShoesSize> ShoesSizes { get; set; }
        public List<ShoesImages> Images { get; set; }

        public int CategoriesId { get; set; }
        public Categories Categories { get; set; }

    }
}