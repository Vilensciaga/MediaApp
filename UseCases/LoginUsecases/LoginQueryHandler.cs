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

namespace UseCases.LoginUsecases
{
    public class LoginQueryHandler(IUserRepository userRepository, ITokenService tokenService, IValidator<LoginQuery> validator)
        : IQueryHandler<LoginQuery, LoginQueryResult>
    {
        public async Task<Result<LoginQueryResult>> Handle(LoginQuery query, CancellationToken cancellationToken = default)
        {
            if(await validator.Validate(query, cancellationToken) is ValidationFailedResult failed)
            {
                return Result<LoginQueryResult>
                    .ValidationFailed(failed.Errors);
            }

            if (query.User is null)
            {
                var userIsNull = new ValidationFailedResult(
                [new("ErrorMessage", "Please enter UserName and Password.")]);

                return Result<LoginQueryResult>
                    .ValidationFailed(userIsNull.Errors);
            }

            var existingUser = await userRepository.GetUserbyUsernameAsync(query.User.Username);

            if (existingUser is null)
            {
                var userIsNull = new ValidationFailedResult(
                [new("ErrorMessage", "Invalid username or password.")]);

                return Result<LoginQueryResult>
                    .ValidationFailed(userIsNull.Errors);
            }

            string token = await tokenService.Authenticate(existingUser, query.User);

            if(token is null)
            {
                var tokenIsNull = new ValidationFailedResult(
                [new("ErrorMessage", "Invalid username or Password.")]);

                return Result<LoginQueryResult>
                    .ValidationFailed(tokenIsNull.Errors);
            }

            UserDto dto = new UserDto
            {
                UserName = existingUser.UserName,
                Token = await tokenService.CreateToken(existingUser),
                PhotoUrl = existingUser?.Photos?.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = existingUser.KnownAs,
                Gender = existingUser.Gender
            };

            var result = new LoginQueryResult
            {
                UserResponse = dto
            };


            return Result<LoginQueryResult>.Success(result);
        }
    }
}
