using F23.Kernel;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Member.UpdateMember
{
    public class UpdateMemberQuery:IQuery<UpdateMemberQueryResult>
    {
        public string UserName { get; set; }
        public UpdateMemberDto MemberDto { get; set; }
    }
}
