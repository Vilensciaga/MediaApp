using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Helpers.PageList
{
    public class PagedListFactory : IPagedlistFactory
    {
        //Allows me to moq the result of pagelist rather than the static method
        public async Task<IPagedList<T>> CreateAsync<T>(IQueryable<T> source, int pageNumber, int pageSize)
        {
            return await PagedList<T>.CreateAsync(source, pageNumber, pageSize);
        }
    }
}
