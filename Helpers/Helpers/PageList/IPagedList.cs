using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Helpers.PageList
{
    public interface IPagedList<T>: IReadOnlyList<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}

/*
 * The IPagedList<T> interface doesn't mock the static method. It lets you mock the result of the static method.
 * Why IReadOnlyList<T>?
It lets you iterate over the collection.

It gives you .Count and index access ([i]), like a normal list.

It avoids exposing mutation (e.g., no .Add, .Remove, etc.).
 */