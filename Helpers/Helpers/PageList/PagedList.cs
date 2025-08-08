using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Helpers.Helpers.PageList
{

    /*
     * Generic class that extends list
     * can take any type of entity by specifying T
     * T will be swapped out at compile time depending on what we use, MemberDto or whatever
     * Made a PagedList Interface
     * Now any method that returns a paged list can use the IPagedList<T> interface instead of the concrete class.
     */
    public class PagedList<T>:List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count/(double) pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }




        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync(); //will make a database query but its unavoidable
            /*
             * setting the size of page and the amount of item per page
             * splitting the users across the pages
             * return paged list
             */
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
