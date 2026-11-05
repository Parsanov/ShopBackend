using Core.DTO.CardDTO;
using Core.DTO.ShoesDTO;
using Core.DTO.Xml.DTO;
using Core.Models;
using Core.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IShoesRepository
    {
        Task<PagedResult<ShoesCardDTO>> GetShoesAsync(PaginationParams pagination, CancellationToken ct);

        Task<ShoesDTO?> GetOneShoes(int id, CancellationToken ct);

        Task UploadAsync(string xmlFile);

        Task AddAsync(string xmlFile);
        Task<PagedResult<ShoesCardDTO>> SortAsync(ShoesFilterQuery filter);
    }
}
