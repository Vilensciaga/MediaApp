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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userService;
        private readonly IMapper mapper;
        public UserController(IUserRepository userService, IMapper mapper) 
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllUsersAsync()
        {
            var users = await userService.GetAllMembersAsync();
            //var u = mapper.Map<IEnumerable<MemberDto>>(users);

            if (users is null)
            {
                return NotFound("Database is empty");
            }

            return Ok(users);
        }

        [HttpGet("id:{id}")]
        public async Task<ActionResult<MemberDto>> GetUseByIdAsync([FromRoute] int id)
        {
            AppUser user = await userService.GetUsersByIdAsync(id);
            var u = mapper.Map<MemberDto>(user);

            if(user is null)
            {
                return BadRequest("User does not exist");
            }

            return Ok(u);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUserByUsernameAsync([FromRoute] string username)
        {
            if(username is null)
            {
                return BadRequest("Please enter a Username");
            }

            var user = await userService.GetMemberByUsernameAsync(username);

            if(user is null)
            {
                return NotFound("User not found");
            }

          

            return Ok(user);

        }

        
        
    }
}
