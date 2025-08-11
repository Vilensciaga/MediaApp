using F23.Kernel;
using Microsoft.Extensions.DependencyInjection;
using UseCases.MemberUsecases.GetMember;
using UseCases.Member.GetMembers;
using UseCases.Member.UpdateMember;
using UseCases.PhotoUsecases.AddPhoto;
using UseCases.PhotoUsecases.DeletePhoto;

namespace Extensions.AppExtensions
{
    public static class RegisterQueryExtensions
    {
        public static IServiceCollection AddRegisterQueryServices(this IServiceCollection services)
        {
            services.RegisterQueryHandler<GetMemberQuery, GetMemberQueryResult, GetMemberQueryHandler>();
            services.RegisterQueryHandler<GetMembersQuery, GetMembersQueryResult, GetMembersQueryHandler>();
            services.RegisterQueryHandler<UpdateMemberQuery, UpdateMemberQueryResult, UpdateMemberQueryHandler>();
            services.RegisterQueryHandler<AddPhotoQuery, AddPhotoQueryResult, AddPhotoQueryHandler>();
            services.RegisterQueryHandler<DeletePhotoQuery, DeletePhotoQueryResult, DeletePhotoQueryHandler>();



            return services;
        }
     }
}
