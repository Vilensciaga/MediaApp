using F23.Kernel;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.RegisterUsecases.Register
{
    public class RegisterQuery:IQuery<RegisterQueryResult>
    {
        public RegisterDto? RegisterDto { get; set; }
    }
}
