using Auth.Jwt;
using AutoMapper;
using DataService.Interface;
using DataService.Service;
using MediaApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userService;
        private readonly ITokenService tokenService;
        private IMapper mapper;

        public AccountController(IMapper mapper, IUserRepository userService, ITokenService tokenService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.tokenService = tokenService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] RegisterDto registerDto)
        {

            if(registerDto == null)
            {
                return BadRequest("Field cannot be null");
            }

            bool userExists = await userService.UserExistsAsync(registerDto.Username);

            if (userExists)
            {
                return BadRequest("User already exists in database");
            }

            AppUser user = await userService.RegisterUserAsync(registerDto);

            UserDto dto = new UserDto
            {
                UserName = user.UserName,
                Token = await tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };

            //using create at route because the method to grab user by id or username is in the user controller
            return CreatedAtRoute("GetUser", new {username = user.UserName }, dto);

        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto user)
        {
            if(user == null)
            {
                return BadRequest("Please enter UserName and Password");
            }


            AppUser existingUser = await userService.GetUserbyUsernameAsync(user.Username);

            if(existingUser == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            string token = await tokenService.Authenticate(existingUser, user);

            if(token == null)
            {
                return Unauthorized("Invalid Username or Password.");
            }

            UserDto dto = new UserDto
            {
                UserName = existingUser.UserName,
                Token = await tokenService.CreateToken(existingUser),
                PhotoUrl = existingUser.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = existingUser.KnownAs,
                Gender = existingUser.Gender
            };

            return Ok(dto);
        }


    }
}
