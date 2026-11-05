using Core.DTO.CardDTO;
using Core.DTO.ShoesDTO;
using Core.Interfaces;
using Core.Models;
using Core.Pagination;

namespace Application.Repository.Shoes
{
    public class ShoesRepository : IShoesRepository
    {
        private readonly IDBShoesRepository _dBShoes;
        private readonly IXMLShoesParser _xmlShoes;
        private readonly IShoesFilterQuery _shoesFilter;
        private readonly ICategoryRepository _categoryRepository;

        public ShoesRepository(IDBShoesRepository dBShoes, IXMLShoesParser xmlShoes, IShoesFilterQuery shoesFilter, ICategoryRepository categoryRepository)
        {
            _dBShoes = dBShoes;
            _xmlShoes = xmlShoes;
            _shoesFilter = shoesFilter;
            _categoryRepository = categoryRepository;
        }

        public async Task<ShoesDTO?> GetOneShoes(int id, CancellationToken ct)
        {
            return await _dBShoes.GetOneShoes(id, ct);
        }

        public async Task<PagedResult<ShoesCardDTO>> GetShoesAsync(PaginationParams pagination, CancellationToken ct)
        {
            return await _dBShoes.GetShoesAsync(pagination, ct);
        }

        public async Task UploadAsync(string xmlFile)
        {
            var xmlShoes = await _xmlShoes.ParseShoesAsync(xmlFile);
            await _dBShoes.UpdateShoesAsync(xmlShoes);
        }
        public async Task AddAsync(string xmlFile)
        {
            var xmlShoes = await _xmlShoes.ParseShoesAsync(xmlFile);
            await _dBShoes.AddShoesAsync(xmlShoes);
        }

        public async Task<PagedResult<ShoesCardDTO>> SortAsync(ShoesFilterQuery filter)
        {
            HashSet<int>? categoryIds = null;

            if (filter.CategoryId is int selectedCategoriesId)
                categoryIds = await _categoryRepository.FiltredCategories(selectedCategoriesId);

            return await _shoesFilter.GetFilteredAsync(filter, categoryIds);
        }
    }
}
 