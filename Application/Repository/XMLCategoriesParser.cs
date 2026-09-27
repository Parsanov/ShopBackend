
using Core.DTO.Xml.DTO;
using Core.Interfaces;
using Persistence;
using System.Xml.Linq;

namespace Application.Repository
{
    public class XMLCategoriesParser : IXMLCategoriesParser
    {
        private readonly DataContext _dataContext;

        public XMLCategoriesParser(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<IEnumerable<XMLCategoryDTO>> ParseCategoriesAsync(string xmlFile)
        {
            using FileStream stream = File.OpenRead(xmlFile);

            XDocument doc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);

            var categories = doc.Descendants("category")
                .Select(category => new XMLCategoryDTO
                {
                    NameCategory = category.Value,
                    CategoryId = (int)category.Attribute("id")
                }).ToList();
            return categories;
        }



    }
}
