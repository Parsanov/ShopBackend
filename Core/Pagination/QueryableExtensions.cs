
using Microsoft.EntityFrameworkCore;

namespace Core.Pagination
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
                this IQueryable<T> query,
                PaginationParams pagination,
                CancellationToken ct = default
            )

        {
            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(ct);


            return new PagedResult<T>(items, pagination.Page, pagination.PageSize, totalCount);    
        }

    }
}
