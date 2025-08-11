using AutoMapper;
using DataService.Interface;
using DataService.Service;
using F23.Kernel;
using F23.Kernel.Results;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.PhotoUsecases.AddPhoto;

namespace UseCases.PhotoUsecases.DeletePhoto
{
    public class DeletePhotoQueryHandler(IPhotoService photoService, IUserRepository userRepository, IValidator<DeletePhotoQuery> validator) :
        IQueryHandler<DeletePhotoQuery, DeletePhotoQueryResult>
    {
        public async Task<Result<DeletePhotoQueryResult>> Handle(DeletePhotoQuery query, CancellationToken cancellationToken = default)
        {
            if( await validator.Validate(query, cancellationToken) is ValidationFailedResult failed)
            {
                return Result<DeletePhotoQueryResult>.ValidationFailed(failed.Errors);
            }

            var user = await userRepository.GetUserbyUsernameAsync(query.Username);
            Photo? photo = user.Photos.FirstOrDefault(x => x.Id == query.PhotoId);

            if(photo is null)
            {
                return Result<DeletePhotoQueryResult>
                    .PreconditionFailed(PreconditionFailedReason.NotFound);
            }

            if (photo.IsMain)
            {
                var cantDeleteMain = new ValidationFailedResult(
                    [ new("ErrorMessage", "Cannot delete main photo.")]);

                return Result<DeletePhotoQueryResult>
                    .ValidationFailed(cantDeleteMain.Errors);
            }

            if (photo.PublicId != null)
            {
                var result = await photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null)
                {
                    return Result<DeletePhotoQueryResult>
                        .PreconditionFailed(PreconditionFailedReason.Conflict, result.Error.Message);
                }
            }

            user.Photos.Remove(photo);

            if (await userRepository.SaveAllAsync())
            {
                var result = new DeletePhotoQueryResult
                {
                    Message = "Photo successfully deleted."
                };

                return Result<DeletePhotoQueryResult>.Success(result);
            }


            var resultF = new DeletePhotoQueryResult
            {
                Message = "Failed to delete the photo."
            };

            var couldNotDelete = new ValidationFailedResult(
                [ new("ErrorMessage", resultF.Message)]);

            return Result<DeletePhotoQueryResult>
                .ValidationFailed(couldNotDelete.Errors);
        }
    }
}
