using Core.DTO.CardDTO;
using Core.DTO.Xml.DTO;
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

        public async Task<PagedResult<ShoesCardDTO>> GetShoesAsync(PaginationParams pagination, CancellationToken ct)
        {
            return await _dBShoes.GetShoesAsync(pagination, ct);
        }

        public async Task UploadAsync(string xmlFile)
        {
            var xmlShoes = await _xmlShoes.ParseShoesAsync(xmlFile);
            await _dBShoes.UploadShoesAsync(xmlShoes);
        }


    }
}
 