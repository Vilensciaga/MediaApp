using Helpers.Helpers.PageList;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestHelpers
{
    /*
     * Generic class that extends list
     * can take any type of entity by specifying T
     * T will be swapped out at compile time depending on what we use, MemberDto or whatever
     */
    public class PagedListTest<T> : List<T>
    {
        public PagedListTest(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }




        public static async Task<PagedList<T>> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count(); //will maje a database query but its unavoidable
            /*
             * setting the size of page and the amount of item per page
             * splitting the users across the pages
             * return paged list
             */
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
