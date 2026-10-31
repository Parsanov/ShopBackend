using Core.DTO.CardDTO;
using Core.DTO.ShoesDTO;
using Core.Interfaces;
using Core.Pagination;

namespace Application.Repository.Shoes
{
    public class ShoesRepository : IShoesRepository
    {
        private readonly IDBShoesRepository _dBShoes;
        private readonly IXMLShoesParser _xmlShoes;

        public ShoesRepository(IDBShoesRepository dBShoes, IXMLShoesParser xmlShoes)
        {
            _dBShoes = dBShoes;
            _xmlShoes = xmlShoes;
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


    }
}
 