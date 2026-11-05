using Core.DTO.CategoriesDTO;
using Core.DTO.Xml.DTO;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoriesDTO>> GetAsync();
         Task UploadAsync(string xmlFile);

        Task<HashSet<int>> FiltredCategories(int categoryId);
    }
}
