using Auth.Jwt;
using DataService.Interface;
using F23.Kernel;
using F23.Kernel.Results;
using MediaApp.Models;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.LoginUsecases;

namespace UseCases.RegisterUsecases.Register
{
    public class RegisterQueryHandler(IUserRepository userRepository, ITokenService tokenService, IValidator<RegisterQuery> validator)
        : IQueryHandler<RegisterQuery, RegisterQueryResult>
    {
        public async Task<Result<RegisterQueryResult>> Handle(RegisterQuery query, CancellationToken cancellationToken = default)
        {
            if (await validator.Validate(query, cancellationToken) is ValidationFailedResult failed)
            {
                return Result<RegisterQueryResult>
                    .ValidationFailed(failed.Errors);
            }

            if (query.RegisterDto is null)
            {
                var userIsNull = new ValidationFailedResult(
                [new("ErrorMessage", "Fields cannot be null.")]);

                return Result<RegisterQueryResult>
                    .ValidationFailed(userIsNull.Errors);
            }

            bool userExists = await userRepository.UserExistsAsync(query.RegisterDto.Username);

            if(userExists)
            {
                var userExist = new ValidationFailedResult(
                [new("ErrorMessage", "Username is already taken.")]);

                return Result<RegisterQueryResult>
                    .ValidationFailed(userExist.Errors);
            }

            AppUser user = await userRepository.RegisterUserAsync(query.RegisterDto);

            UserDto dto = new UserDto
            {
                UserName = user.UserName,
                Token = await tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };

            var result = new RegisterQueryResult
            {
                User = dto
            };

            return Result<RegisterQueryResult>.Success(result);

        }
    }
}
