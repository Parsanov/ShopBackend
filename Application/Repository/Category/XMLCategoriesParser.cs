using Core.DTO.Xml.DTO;
using Core.Interfaces;
using Persistence;
using System.Xml.Linq;

namespace Application.Repository.Category
{
    public class XMLCategoriesParser : IXMLCategoriesParser
    {

        public async Task<IEnumerable<XMLCategoryDTO>> ParseCategoriesAsync(string xmlFile)
        {
            using FileStream stream = File.OpenRead(xmlFile);

            XDocument doc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);

            var categories = doc.Descendants("category")
                .Select(category => new XMLCategoryDTO
                {
                    XmlId = (int)category.Attribute("id"),
                    ParentXmlId= (int?)category.Attribute("parentId"),
                    NameCategory = category.Value
                }).ToList();
            return categories;
        }

    }
}
