using AutoMapper;
using DataService.Interface;
using Extensions.AppExtensions;
using F23.Kernel;
using F23.Kernel.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using Models.Models;
using UseCases.PhotoUsecases.AddPhoto;

namespace API.Controllers.Photo
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddPhotoController(IQueryHandler<AddPhotoQuery, AddPhotoQueryResult> queryHandler) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddPhotoAsync(IFormFile file)
        {
            var username = User.GetUsername();

            var query = new AddPhotoQuery
            {
                UserName = username,
                File = file,
            };

            var result = await queryHandler.Handle(query);
            var photoDto = result.Map(r => r.Photo);

            if(result.IsSuccess)
            {
                return result.ToActionResult(r => CreatedAtRoute("GetMember", new { username = username }, r.Photo));

            }

            return result.ToActionResult();
        }
    }
}
