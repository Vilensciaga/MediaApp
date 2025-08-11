using AutoMapper;
using DataService.Interface;
using F23.Kernel;
using F23.Kernel.Results;
using Models.Dtos.User;
using Models.Models;


namespace UseCases.PhotoUsecases.AddPhoto
{
    public class AddPhotoQueryHandler(IPhotoService photoService, IUserRepository userRepository, IValidator<AddPhotoQuery> validator, IMapper mapper) :
        IQueryHandler<AddPhotoQuery, AddPhotoQueryResult>
    {
        public async Task<Result<AddPhotoQueryResult>> Handle(AddPhotoQuery query, CancellationToken cancellationToken = default)
        {
            if( await validator.Validate(query, cancellationToken) is ValidationFailedResult failed)
            {
                return Result<AddPhotoQueryResult>.ValidationFailed(failed.Errors);
            }

            var user = await userRepository.GetUserbyUsernameAsync(query.UserName);

            //Adding photo to cloudinary
            var photoResult = await photoService.AddPhotoAsync(query.File);

            if(photoResult.Error != null)
            {
                var failedToAdd = new ValidationFailedResult(
                [ new("ErrorMessage", photoResult.Error.Message)]);

                return Result<AddPhotoQueryResult>
                    .ValidationFailed(failedToAdd.Errors);
            }

            var photo = new Photo
            {
                Url = photoResult.SecureUrl.AbsoluteUri,
                PublicId = photoResult.PublicId,
            };

            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if (await userRepository.SaveAllAsync())
            {
                var photoDto = mapper.Map<PhotoDto>(photo);
                var result = new AddPhotoQueryResult
                {
       
                    Photo = photoDto
                };

                return Result<AddPhotoQueryResult>.Success(result);
            }

            var failedToUpload = new ValidationFailedResult(
                [new("ErrorMessage", "Issues encountered uploading the photo")]);

            return Result<AddPhotoQueryResult>
                .ValidationFailed(failedToUpload.Errors);

        }
    }
}
