using Core.DTO.CategoriesDTO;
using Core.Interfaces;

namespace Application.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IXMLCategoriesParser _xmlCategoriesParser;
        private readonly IDBCategoryRepository _dBCategoryRepository;

        public CategoryRepository(IXMLCategoriesParser xmlCategoriesParser, IDBCategoryRepository dBCategoryRepository)
        {
            _xmlCategoriesParser = xmlCategoriesParser;
            _dBCategoryRepository = dBCategoryRepository;
        }

        public async Task<HashSet<int>> FiltredCategories(int categoryId)
        {
            return await _dBCategoryRepository.GetCategoryWithDescendantsAsync(categoryId);
        }

        public async Task<List<CategoriesDTO>> GetAsync()
        {
            return await _dBCategoryRepository.GetAllCategoriesTreeAsync();
        }

        public async Task UploadAsync(string xmlFile)
        {
            var xmlCategories = await _xmlCategoriesParser.ParseCategoriesAsync(xmlFile);
            await _dBCategoryRepository.UploadCategoriesAsync(xmlCategories);
        }


    }
}
