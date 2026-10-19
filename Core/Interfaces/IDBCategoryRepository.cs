using Core.DTO.Xml.DTO;
using Core.Models;

namespace Core.Interfaces
{
    public interface IDBCategoryRepository
    {
        Task<IEnumerable<Categories>> GetAllCategoriesAsync();
        Task UploadCategoriesAsync(IEnumerable<XMLCategoryDTO> categories);
    }
}
