using Core.DTO.CardDTO;
using Core.Interfaces;
using Core.Models;
using Core.Pagination;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repository.Shoes
{
    public class ShoesFilterQueryRepository : IShoesFilterQuery
    {
        private readonly DataContext _dataContext;

        public ShoesFilterQueryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<PagedResult<ShoesCardDTO>> GetFilteredAsync(ShoesFilterQuery filter, HashSet<int>? categoryIds)
        {
            int pageSize = Math.Clamp(filter.PageSize, 1, 50);
            int page = Math.Max(filter.Page, 1);


            IQueryable<Core.Models.Shoes> query = _dataContext.Shoes
                .AsNoTracking()
                .Where(s => s.IsActive);


            if (categoryIds is not null)
                query = query.Where(s => categoryIds.Contains(s.CategoriesId));

            if (filter.MinPrice is decimal minPrice)
                query = query.Where(c => c.Price >= minPrice);

            if (filter.MaxPrice is decimal maxPrice)
                query = query.Where(c => c.Price <= maxPrice);

            if (filter.Size is byte size)
                query = query.Where(c => c.ShoesSizes.Any(
                    z => z.Size == size && z.Available && z.QuantityInStock > 0));

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                string[] searchWords = filter.Search
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);


                foreach (string word in searchWords)
                {
                    string pattern = $"%{word}%";
                    query = query.Where(s => EF.Functions.ILike(s.BrandName, pattern));
                }


            }

            query = filter.Sort switch
            {
                "price_asc" => query.OrderBy(s => s.Price).ThenBy(i => i.Id),
                "price_desc" => query.OrderByDescending(s => s.Price).ThenBy(i => i.Id),
                _ => query.OrderByDescending(s => s.CreatedAt).ThenByDescending(i => i.Id)
            };

            int totalCount = await query.CountAsync();

            List<ShoesCardDTO> items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ShoesCardDTO
                (
                   s.Id,
                   s.BrandName,
                   s.Price,
                   s.Images.Select(i => i.ImageUrl).FirstOrDefault(),
                   s.CreatedAt
                ))
                .ToListAsync();

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResult<ShoesCardDTO>(
                items,
                page,
                pageSize,
                totalCount
           );

        }
    }
}
