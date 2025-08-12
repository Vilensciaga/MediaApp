using F23.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.PhotoUsecases.SetMainPhoto
{
    public class SetMainPhotoQuery:IQuery<SetMainPhotoQueryResult>
    {
        public int PhotoId { get; set; }
        public string UserName { get; set; }
    }
}
