using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.GetMember
{
    public class GetMemberQueryResult
    {
        public required MemberDto Member { get; init; } 
    }
}
