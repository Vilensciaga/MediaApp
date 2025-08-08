using F23.Kernel;
using Helpers.Helpers;
using Helpers.Helpers.PageList;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.GetMembers
{
    public class GetMembersQuery: IQuery<GetMembersQueryResult>
    {
        public string? Username { get; init; }
        public UserParams? UserParams { get; init; } 

    }
}
