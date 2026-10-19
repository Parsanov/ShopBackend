using Core.DTO.Xml.DTO;

namespace Core.Interfaces
{
    public interface ICategoryRepository
    {
         Task<IEnumerable<XMLCategoryDTO>> GetAsync();
         Task UploadAsync(string xmlFile);
    }
}
