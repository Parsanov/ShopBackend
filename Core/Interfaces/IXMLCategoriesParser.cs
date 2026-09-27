using Core.DTO.Xml.DTO;
using Core.Models;

namespace Core.Interfaces
{
    public interface IXMLCategoriesParser
    {
        Task<IEnumerable<XMLCategoryDTO>> ParseCategoriesAsync(string xmlFile);
    }
}
