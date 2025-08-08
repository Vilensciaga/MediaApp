using Auth.Jwt;
using AutoMapper;
using DataService.Interface;
using FakeItEasy;
using FluentAssertions;
using MediaApp.Controllers;
using MediaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using Xunit.Abstractions;

namespace API.Test.Controller.Tests
{
    public class AccountControllerTest
    {
        private IUserRepository userRepoF;
        private readonly AccountController accountControllerF;
        private readonly ITokenService tokenServiceF;
        private readonly IMapper mapperF;
        private ITestOutputHelper output;



        public AccountControllerTest(ITestOutputHelper output)
        {
            userRepoF = A.Fake<IUserRepository>();
            tokenServiceF = A.Fake<ITokenService>();
            mapperF = A.Fake<IMapper>();

            accountControllerF = new AccountController(mapperF, userRepoF, tokenServiceF);
            this.output = output;
        }

        [Fact]
        public async void AccountController_RegisterUser_ReturnsUser()
        {
            //Arrange
            var registerDto = UserDataGetter.GetRegisterDto();
            //var registerDto2 = A.Fake<RegisterDto>();
            //var user = A.Fake<AppUser>();
            var user = UserDataGetter.GetAppUser();
            var userDto = A.Fake<UserDto>();

            A.CallTo(() => userRepoF.UserExistsAsync(registerDto.Username)).Returns(false);
            A.CallTo(() => userRepoF.RegisterUserAsync(registerDto)).Returns(user);

            //Act
            var result = await accountControllerF.RegisterUser(registerDto);

            //output.WriteLine("result: ", result.Result);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<UserDto>>();
        }
    }
}
