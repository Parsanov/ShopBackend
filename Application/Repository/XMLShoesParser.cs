

using Core.DTO.Xml.DTO;
using Core.Interfaces;
using System.Xml.Linq;

namespace Application.Repository
{
    public class XMLShoesParser : IXMLShoesParser
    {
        public async Task<IEnumerable<XMLShoesDTO>> ParseShoesAsync(string xmlFile)
        {
            using FileStream stream = File.OpenRead(xmlFile);

            XDocument doc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);

            var shoes = doc.Descendants("offer")
                .Where(o => o.Element("name") != null && o.Element("categoryId") != null)
                .Select(shoe => new XMLShoesDTO
                {
                    BrandName = shoe.Element("name").Value,
                    Description = shoe.Element("description")?.Value ?? string.Empty,
                    Price = decimal.TryParse(shoe.Element("price")?.Value, out var price) ? price : 0m,
                    CategoriesId = int.TryParse(shoe.Element("categoryId")?.Value, out var cat) ? cat : 0,
                    Available = bool.TryParse(shoe.Element("available")?.Value, out var avail) ? avail : false,
                    VendorCode = shoe.Element("vendorCode")?.Value ?? string.Empty,
                    Weight = decimal.TryParse(shoe.Element("weight")?.Value, out var w) ? w : 0m,
                    ShoesSizes = shoe.Element("param")?.Value ?? string.Empty,
                    Images = shoe.Elements("picture").Select(p => p.Value).ToList(), // use specific element name for images if applicable
                    QuantityInStock = int.TryParse(shoe.Element("quantity_in_stock")?.Value, out var q) ? q : 0
                })
                .ToList();
            return shoes;
        }
    }
}
