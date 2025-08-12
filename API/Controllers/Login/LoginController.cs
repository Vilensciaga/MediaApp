using F23.Kernel;
using F23.Kernel.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using UseCases.LoginUsecases;

namespace API.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IQueryHandler<LoginQuery, LoginQueryResult> queryHandler) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto user)
        {
            var query = new LoginQuery
            {
                User = user
            };

            var result = await queryHandler.Handle(query);

            return result.ToActionResult();
        }
    }
}
