using Extensions.AppExtensions;
using F23.Kernel;
using F23.Kernel.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.PhotoUsecases.DeletePhoto;

namespace API.Controllers.Photo
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeletePhotoController(IQueryHandler<DeletePhotoQuery, DeletePhotoQueryResult> queryHandler) : ControllerBase
    {
        [HttpDelete("{photoId}")]
        public async Task<IActionResult> DeletePhotoAsync(int photoId)
        {
            var query = new DeletePhotoQuery
            {
                PhotoId = photoId,
                Username = User.GetUsername()
            };

            var result = await queryHandler.Handle(query);

            if(result.IsSuccess)
            {
                return result.ToActionResult(r => Ok());
            }

            return result.ToActionResult();
        }

    }
}
