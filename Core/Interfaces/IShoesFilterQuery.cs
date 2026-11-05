using Core.DTO.CardDTO;
using Core.Models;
using Core.Pagination;

namespace Core.Interfaces
{
    public interface IShoesFilterQuery
    {
        Task<PagedResult<ShoesCardDTO>> GetFilteredAsync(ShoesFilterQuery filter, HashSet<int>? categoryIds);
    }
}
