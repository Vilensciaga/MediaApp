using Helpers.Helpers.PageList;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.GetMembers
{
    public class GetMembersQueryResult
    {
        public IPagedList<MemberDto>? Members { get; init; }
    }
}
