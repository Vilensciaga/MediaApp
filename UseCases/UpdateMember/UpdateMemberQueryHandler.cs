using AutoMapper;
using DataService.Interface;
using F23.Kernel;
using F23.Kernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.GetMembers;

namespace UseCases.UpdateMember
{
    public class UpdateMemberQueryHandler(IUserRepository userRepository, IValidator<UpdateMemberQuery> validator, IMapper mapper):
        IQueryHandler<UpdateMemberQuery, UpdateMemberQueryResult>
    {
        public async Task<Result<UpdateMemberQueryResult>> Handle(UpdateMemberQuery query, CancellationToken cancellationToken = default)
        {

            if(await validator.Validate(query, cancellationToken) is ValidationFailedResult failed)
            {
                return Result<UpdateMemberQueryResult>.ValidationFailed(failed.Errors);
            }


            var user = await userRepository.GetUserbyUsernameAsync(query.UserName);

            mapper.Map(query.MemberDto, user);

            await userRepository.UpdateUserAsync(user);

            var result = new UpdateMemberQueryResult
            {
            };

            if (await userRepository.SaveAllAsync())
            {
                result.SuccessStatus = true;
                return Result<UpdateMemberQueryResult>.Success(result);
            }

            result.SuccessStatus = false;
            return Result<UpdateMemberQueryResult>.PreconditionFailed(PreconditionFailedReason.Conflict, "Failed to update user.");

        }

    }
}
