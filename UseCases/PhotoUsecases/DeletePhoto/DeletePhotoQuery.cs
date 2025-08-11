using F23.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.PhotoUsecases.DeletePhoto
{
    public class DeletePhotoQuery:IQuery<DeletePhotoQueryResult>
    {
        public int PhotoId { get; set; }
        public string Username { get; set; }
    }
}
