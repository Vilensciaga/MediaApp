using Extensions.AppExtensions;
using F23.Kernel;
using F23.Kernel.AspNetCore;
using F23.Kernel.Results;
using FluentValidation.TestHelper;
using Helpers.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using UseCases.Member.GetMembers;

namespace API.Controllers.Member
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class GetMembersController(IQueryHandler<GetMembersQuery, GetMembersQueryResult> queryHandler) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetMembersAsync([FromQuery] UserParams userParams)
        {
            string username = User.GetUsername();

            var query = new GetMembersQuery
            {
                Username = username,
                UserParams = userParams
            };

            var result = await queryHandler.Handle(query);

            if (result is SuccessResult<GetMembersQueryResult> successResult)
            {
                var resultValue = successResult.Value;
                var pagination = resultValue.Members;

                Response.AddPaginationHeader(pagination.CurrentPage, pagination.PageSize,
                    pagination.TotalCount, pagination.TotalPages);

                return result.ToActionResult(r => Ok(resultValue.Members));
            }

            return result.ToActionResult();
        }
    }
}
