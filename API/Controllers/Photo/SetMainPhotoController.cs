using Extensions.AppExtensions;
using F23.Kernel;
using F23.Kernel.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.PhotoUsecases.SetMainPhoto;

namespace API.Controllers.Photo
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SetMainPhotoController(IQueryHandler<SetMainPhotoQuery, SetMainPhotoQueryResult> queryHandler) : ControllerBase
    {
        [HttpPut("{photoId}")]
        public async Task<IActionResult> SetMainPhotoAsync(int photoId)
        {
            var query = new SetMainPhotoQuery
            {
                UserName = User.GetUsername(),
                PhotoId = photoId,
            };

            var result = await queryHandler.Handle(query);

            if(result.IsSuccess)
            {
                return result.ToActionResult(r => NoContent());
            }

            return result.ToActionResult();
        }
    }
}
