using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Pagination
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;

        private int _page = 1;
        private int _pageSize = 20;

        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Clamp(value, 1, MaxPageSize);
        }
    }
}
