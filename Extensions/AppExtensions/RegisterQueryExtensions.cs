using F23.Kernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.GetMember;
using UseCases.GetMembers;

namespace Extensions.AppExtensions
{
    public static class RegisterQueryExtensions
    {
        public static IServiceCollection AddRegisterQueryServices(this IServiceCollection services)
        {
            services.RegisterQueryHandler<GetMemberQuery, GetMemberQueryResult, GetMemberQueryHandler>();
            services.RegisterQueryHandler<GetMembersQuery, GetMembersQueryResult, GetMembersQueryHandler>();

            return services;
        }
     }
}
