using DataService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F23.Kernel.AspNetCore;
using F23.Kernel;
using F23.Kernel.Results;

namespace UseCases.MemberUsecases.GetMember
{
    public class GetMemberQueryHandler(IUserRepository userRepo, IValidator<GetMemberQuery> validator)
    : IQueryHandler<GetMemberQuery, GetMemberQueryResult>
    {
        public async Task<Result<GetMemberQueryResult>> Handle(GetMemberQuery query, CancellationToken cancellationToken = default)
        {
            if (await validator.Validate(query, cancellationToken) is ValidationFailedResult failed)
            {
                return Result<GetMemberQueryResult>.ValidationFailed(failed.Errors);
            }

            var member = await userRepo.GetMemberByUsernameAsync(query.Username);

            if(member is null)
            {
                return Result<GetMemberQueryResult>
                    .PreconditionFailed(PreconditionFailedReason.NotFound);
            }

            var result = new GetMemberQueryResult
            {
                Member = member
            };


            return Result<GetMemberQueryResult>.Success(result);
        }

    }
}
