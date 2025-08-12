using Azure.Identity;
using F23.Kernel;
using F23.Kernel.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using UseCases.RegisterUsecases.Register;

namespace API.Controllers.Register
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController(IQueryHandler<RegisterQuery, RegisterQueryResult> queryHandler) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto user)
        {
            var query = new RegisterQuery
            {
                RegisterDto = user
            };

            var result = await queryHandler.Handle(query);

            return result.ToActionResult(r => CreatedAtRoute("GetMember",new { username = r.User.UserName}, r.User));
        }
    }
}
