using Extensions.AppExtensions;
using F23.Kernel;
using F23.Kernel.AspNetCore;
using Helpers.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using UseCases.GetMembers;

namespace API.Controllers.Member
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class GetMembersController(IQueryHandler<GetMembersQuery, GetMembersQueryResult> queryHandler) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetMembersAsync([FromRoute] UserParams userParams)
        {
            string username = User.GetUsername();

            var query = new GetMembersQuery
            {
                Username = username,
                UserParams = userParams
            };

            var result = await queryHandler.Handle(query);

            return result.ToActionResult();
        }
    }
}
