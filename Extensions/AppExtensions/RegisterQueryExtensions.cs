using F23.Kernel;
using Microsoft.Extensions.DependencyInjection;
using UseCases.GetMember;
using UseCases.GetMembers;
using UseCases.UpdateMember;

namespace Extensions.AppExtensions
{
    public static class RegisterQueryExtensions
    {
        public static IServiceCollection AddRegisterQueryServices(this IServiceCollection services)
        {
            services.RegisterQueryHandler<GetMemberQuery, GetMemberQueryResult, GetMemberQueryHandler>();
            services.RegisterQueryHandler<GetMembersQuery, GetMembersQueryResult, GetMembersQueryHandler>();
            services.RegisterQueryHandler<UpdateMemberQuery, UpdateMemberQueryResult, UpdateMemberQueryHandler>();


            return services;
        }
     }
}
