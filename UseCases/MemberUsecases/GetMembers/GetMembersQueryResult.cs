using Helpers.Helpers.PageList;
using Models.Dtos.User;

namespace UseCases.Member.GetMembers
{
    public class GetMembersQueryResult
    {
        public IPagedList<MemberDto>? Members { get; init; }
    }
}
