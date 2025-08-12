using DataService.Interface;
using F23.Kernel;
using F23.Kernel.Results;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.PhotoUsecases.DeletePhoto;

namespace UseCases.PhotoUsecases.SetMainPhoto
{
    public class SetMainPhotoQueryHandler(IUserRepository userRepository, IValidator<SetMainPhotoQuery> validator)
        : IQueryHandler<SetMainPhotoQuery, SetMainPhotoQueryResult>
    {
        public async Task<Result<SetMainPhotoQueryResult>> Handle(SetMainPhotoQuery query, CancellationToken cancellationToken = default)
        {
            if(await validator.Validate(query, cancellationToken) is ValidationFailedResult failed)
            {
                return Result<SetMainPhotoQueryResult>
                    .ValidationFailed(failed.Errors);
            }

            var user = await userRepository.GetUserbyUsernameAsync(query.UserName);
            var photo = user.Photos.FirstOrDefault(x => x.Id == query.PhotoId);

            if (photo.IsMain)
            {
                var alreadyMain = new ValidationFailedResult(
                   [new("ErrorMessage", "This is already the main photo.")]);

                return Result<SetMainPhotoQueryResult>
                    .ValidationFailed(alreadyMain.Errors);
            }

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMain != null)
            {
                currentMain.IsMain = false;
                photo.IsMain = true;
            }

            if (await userRepository.SaveAllAsync())
            {
                var result = new SetMainPhotoQueryResult
                {
                    Message = "Successfully set main photo."
                };

                return Result<SetMainPhotoQueryResult>.Success(result);
            }

            var failedToSet = new ValidationFailedResult(
                   [new("ErrorMessage", "Failed to set main photo.")]);

            return Result<SetMainPhotoQueryResult>
                .ValidationFailed(failedToSet.Errors);
        }
    }
}
