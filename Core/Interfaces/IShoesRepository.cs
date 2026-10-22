using Core.DTO.CardDTO;
using Core.DTO.Xml.DTO;
using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IShoesRepository
    {
        Task<PagedResult<ShoesCardDTO>> GetShoesAsync(PaginationParams pagination, CancellationToken ct);
        Task UploadAsync(string xmlFile);
    }
}
