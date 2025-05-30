using AutoMapper;
using DataService.Interface;
using MediaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Models.Dtos.User;

namespace MediaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public UserController(IUserService userService, IMapper mapper) 
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsersAsync()
        {
            var users = await userService.GetAllUsersAsync();
            var u = mapper.Map<IEnumerable<AppUserDto>>(users);

            if (users is null)
            {
                return BadRequest("Database is empty");
            }

            return Ok(u);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetUseByIdAsync([FromRoute] int id)
        {
            var user = await userService.GetUsersByIdAsync(id);
            var u = mapper.Map<AppUserDto>(user);

            if(user is null)
            {
                return BadRequest("User does not exist");
            }

            return Ok(u);
        }
        
        
    }
}
