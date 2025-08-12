using Extensions.AppExtensions;
using F23.Kernel;
using F23.Kernel.AspNetCore;
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
        public async Task<IActionResult> GetMembersAsync([FromRoute] UserParams userParams)
        {
            string username = User.GetUsername();

            var query = new GetMembersQuery
            {
                Username = username,
                UserParams = userParams
            };

            var result = await queryHandler.Handle(query);

            var pagination = result.Map(r => r.Members);
            
            //var CurrentPage = Convert.ToInt32(pagination.Map(r => r.CurrentPage).ToString());
            //var PageSize = Convert.ToInt32(pagination.Map(r => r.PageSize).ToString());
            //var TotalCount = Convert.ToInt32(pagination.Map(r => r.TotalCount).ToString());
            //var TotalPages = Convert.ToInt32(pagination.Map(r => r.TotalPages).ToString());


            //Response.AddPaginationHeader(CurrentPage, PageSize, TotalCount, TotalPages);


            return result.ToActionResult();
        }
    }
}
