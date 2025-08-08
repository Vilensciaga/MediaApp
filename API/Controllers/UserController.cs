using AutoMapper;
using DataService.Interface;
using MediaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Models.Dtos.User;
using System.Security.Claims;
using DataService.Service;
using Models.Models;
using CloudinaryDotNet.Actions;
using Extensions.AppExtensions;
using Helpers.Helpers;
using F23.Kernel;
using UseCases.GetMember;
using F23.Kernel.AspNetCore;

namespace MediaApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userService;
        private readonly IMapper mapper;
        private readonly IPhotoService photoservice;
        //private readonly IQueryHandler<GetMemberQuery, GetMemberQueryResult> queryHandler;
        public UserController(IUserRepository userService, IMapper mapper, IPhotoService photoservice) 
        {
            this.userService = userService;
            this.mapper = mapper;
            this.photoservice = photoservice;
            //this.queryHandler = queryHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllUsersAsync([FromQuery]UserParams userParams)
        {
            var user = await userService.GetUserbyUsernameAsync(User.GetUsername());
            userParams.CurrentUsername = user.UserName;

            if(string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = user.Gender == "male" ? "female" : "male";
            }

            //users is now of type paged list rather than i enumerable
            var users = await userService.GetAllMembersAsync(userParams);
            //var u = mapper.Map<IEnumerable<MemberDto>>(users);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            if (users is null)
            {
                return NotFound("Database is empty");
            }

            return Ok(users);
        }
        
        //Not in use by the front end

        //[HttpGet("id:{id}")]
        //public async Task<ActionResult<MemberDto>> GetUseByIdAsync([FromRoute] int id)
        //{
        //    AppUser user = await userService.GetUsersByIdAsync(id);
        //    var u = mapper.Map<MemberDto>(user);

        //    if(user is null)
        //    {
        //        return BadRequest("User does not exist");
        //    }

        //    return Ok(u);
        //}

        //adding name to the route so i can access it in my account controller when i create a new user to return created at route 201 message
        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUserByUsernameAsync([FromRoute] string username)
        {
            if (username is null)
            {
                return BadRequest("Please enter a Username");
            }

            var user = await userService.GetMemberByUsernameAsync(username);

            if (user is null)
            {
                return NotFound("User not found");
            }



            return Ok(user);

        }


        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateMemberDto updateMemberDto)
        {
            //grabing the username from user token with this extension method
            var username = User.GetUsername();

            var user = await userService.GetUserbyUsernameAsync(username);

            mapper.Map(updateMemberDto, user);

            await userService.UpdateUserAsync(user);

            if (await userService.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to Update");
        }


        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhotoAsync(IFormFile file)
        {
            var username = User.GetUsername();
            var user = await userService.GetUserbyUsernameAsync(username);

            var results = await photoservice.AddPhotoAsync(file);

            if(results.Error != null)
            {
                return BadRequest(results.Error.Message);
            }

            var photo = new Photo
            {
                Url = results.SecureUrl.AbsoluteUri,
                PublicId = results.PublicId,
            };

            if(user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if(await userService.SaveAllAsync())
            {
                // we could use create at route if we dont have a method in this controller to grab a user by or username
                //returns a 201 because we created something
                return CreatedAtAction(nameof(GetUserByUsernameAsync), new {username = user.UserName}, mapper.Map<PhotoDto>(photo));
            }

            return BadRequest("Issues encountered uploading the photo");
        }


        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhotoAsync(int photoId)
        {
            var username = User.GetUsername();
            AppUser user = await userService.GetUserbyUsernameAsync(username);

            Photo photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null)
            {
                return NotFound();
            }

            if(photo.IsMain)
            {
                return BadRequest("Cannot delete main photo");
            }

            if(photo.PublicId != null)
            {
                var result = await photoservice.DeletePhotoAsync(photo.PublicId);
                if(result.Error != null)
                {
                    return BadRequest(result.Error.Message);
                }
            }

            user.Photos.Remove(photo);

            if(await userService.SaveAllAsync())
            {
                return Ok();
            }
               
            
            return BadRequest("Failed to delete the photo.");
        }


        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await userService.GetUserbyUsernameAsync(User.GetUsername());
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo.IsMain) return BadRequest("This is already the main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if(currentMain != null)
            {
                currentMain.IsMain = false;
                photo.IsMain = true;
            }

            if (await userService.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to set main photo");


        }



    }
}
