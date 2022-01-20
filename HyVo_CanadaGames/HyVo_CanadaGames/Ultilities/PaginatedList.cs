using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HyVo_CanadaGames.Ultilities
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageAmount)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageAmount);
            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get => PageIndex > 1;
        }

        public bool HasNextPage
        {
            get => PageIndex < TotalPages;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageAmount)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((pageIndex - 1) * pageAmount)
                .Take(pageAmount)
                .ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageAmount);
        }
    }
}
