using Core.DTO.CategoriesDTO;
using Core.DTO.Xml.DTO;
using Core.Models;

namespace Core.Interfaces
{
    public interface IDBCategoryRepository
    {
        Task<List<CategoriesDTO>> GetAllCategoriesTreeAsync();
        Task<HashSet<int>> GetCategoryWithDescendantsAsync(int categoryId);
        Task UploadCategoriesAsync(IEnumerable<XMLCategoryDTO> categories);

    }
}
