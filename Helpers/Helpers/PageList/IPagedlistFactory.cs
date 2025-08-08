using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Helpers.PageList
{
    public interface IPagedlistFactory
    {
        //allows me to inject a page list factory to bypass static method of creating the pagedList
        Task<IPagedList<T>> CreateAsync<T>(IQueryable<T> source, int pageNumber, int pageSize);
    }
}
