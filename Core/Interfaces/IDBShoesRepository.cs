using Core.DTO.CardDTO;
using Core.DTO.Xml.DTO;
using Core.Models;
using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IDBShoesRepository
    {
        Task<PagedResult<ShoesCardDTO>> GetShoesAsync(PaginationParams pagination, CancellationToken ct);
        Task UploadShoesAsync(IEnumerable<XMLShoesDTO> categories);
    }
}
