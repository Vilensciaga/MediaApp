using Models.Dtos.User;

namespace UseCases.LoginUsecases
{
    public class LoginQueryResult
    {
        public UserDto? UserResponse { get; set; }
    }
}