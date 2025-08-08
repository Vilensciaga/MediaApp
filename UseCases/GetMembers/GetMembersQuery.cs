using F23.Kernel;
using Helpers.Helpers;

namespace UseCases.GetMembers
{
    public class GetMembersQuery: IQuery<GetMembersQueryResult>
    {
        public string? Username { get; init; }
        public UserParams? UserParams { get; init; } 

    }
}
