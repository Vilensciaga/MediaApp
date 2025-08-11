using Models.Dtos.User;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.PhotoUsecases.AddPhoto
{
    public class AddPhotoQueryResult
    {
        public PhotoDto? Photo { get; set; }
    }
}
