
namespace Core.DTO.Xml.DTO
{
    public class XMLShoesDTO
    {
        public string BrandName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string VendorCode { get; set; }
        public decimal Weight { get; set; }
        public string ShoesSizes { get; set; }
        public List<string> Images { get; set; }
        public int CategoriesId { get; set; }
        public int QuantityInStock { get; set; }
    }
}
