using F23.Kernel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.PhotoUsecases.AddPhoto
{
    public class AddPhotoQuery:IQuery<AddPhotoQueryResult>
    {
        public string UserName { get; set; }
        public IFormFile File { get; set; }
    }
}
