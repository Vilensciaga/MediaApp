using F23.Kernel;
using F23.Kernel.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using UseCases.GetMember;

namespace MediaApp.Controllers.Member
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GetMemberController(IQueryHandler<GetMemberQuery, GetMemberQueryResult> queryHandler) : ControllerBase
    {
        //adding name to the route so i can access it in my account controller when i create a new user to return created at route 201 message
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserByUsernameAsync([FromRoute] string username)
        {
            var query = new GetMemberQuery
            {
                Username = username
            };

            var result = await queryHandler.Handle(query);

            //var memberResult = result.Map<MemberDto>(r => r.Member);

            return result.ToActionResult();
        }
    }
}
