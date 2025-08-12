using F23.Kernel;
using Microsoft.Extensions.DependencyInjection;
using UseCases.MemberUsecases.GetMember;
using UseCases.Member.GetMembers;
using UseCases.Member.UpdateMember;
using UseCases.PhotoUsecases.AddPhoto;
using UseCases.PhotoUsecases.DeletePhoto;
using UseCases.PhotoUsecases.SetMainPhoto;
using UseCases.LoginUsecases;
using UseCases.RegisterUsecases.Register;

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
            services.RegisterQueryHandler<SetMainPhotoQuery, SetMainPhotoQueryResult, SetMainPhotoQueryHandler>();
            services.RegisterQueryHandler<LoginQuery, LoginQueryResult, LoginQueryHandler>();
            services.RegisterQueryHandler<RegisterQuery, RegisterQueryResult, RegisterQueryHandler>();






            return services;
        }
     }
}
