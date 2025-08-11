using Extensions.AppExtensions;
using F23.Kernel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using UseCases.Member.UpdateMember;

namespace API.Controllers.Member
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UpdateMemberController(IQueryHandler<UpdateMemberQuery, UpdateMemberQueryResult> queryHandler) : ControllerBase
    {

        [HttpPut]
        public async Task<IActionResult> UpdateMemberAsync([FromBody] UpdateMemberDto updateMemberDto)
        {
            string username = User.GetUsername();

            var query = new UpdateMemberQuery
            {
                UserName = username,
                MemberDto = updateMemberDto
            };

            var result = await queryHandler.Handle(query);

            if(result.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(result.Message);
        }
    }
}
