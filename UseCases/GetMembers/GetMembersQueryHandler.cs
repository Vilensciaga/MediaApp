using DataService.Interface;
using F23.Kernel;
using F23.Kernel.Results;

namespace UseCases.GetMembers
{
    public class GetMembersQueryHandler(IUserRepository userRepository, IValidator<GetMembersQuery> validator) :
        IQueryHandler<GetMembersQuery, GetMembersQueryResult>
    {
        public async Task<Result<GetMembersQueryResult>> Handle(GetMembersQuery query, CancellationToken cancellationToken = default)
        {
            if (await validator.Validate(query, cancellationToken) is ValidationFailedResult failed)
            {
                return Result<GetMembersQueryResult>.ValidationFailed(failed.Errors);
            }

            var user = await userRepository.GetUserbyUsernameAsync(query.Username);

            query.UserParams.CurrentUsername = user.UserName;

            if (string.IsNullOrEmpty(query.UserParams.Gender))
            {
                query.UserParams.Gender = user.Gender == "male" ? "female" : "male";
            }

            var members = await userRepository.GetAllMembersAsync(query.UserParams);

            if (members is null)
            {
                return  Result<GetMembersQueryResult>.PreconditionFailed(PreconditionFailedReason.NotFound);
            }

            var result = new GetMembersQueryResult
            {
                Members = members,
            };

            return Result<GetMembersQueryResult>.Success(result);

        }
    }
}
