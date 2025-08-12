using F23.Kernel;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.LoginUsecases
{
    public class LoginQuery:IQuery<LoginQueryResult>
    {
        public LoginDto? User { get; set; }
    }
}
