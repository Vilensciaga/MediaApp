using AutoMapper;
using DataService.Interface;
using FakeItEasy;
using FluentAssertions;
using Helpers.Helpers;
using Helpers.Helpers.PageList;
using MediaApp.Controllers;
using MediaApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace API.Test.Controller.Tests
{
    public class UserControllerTest
    {
        private IUserRepository userRepoFakeIt;
        private IPhotoService photoServiceFakeit;
        private UserParams userParams;
        private readonly UserController userController;
        private IPagedlistFactory pagedlistFactoryFakeIt;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private ITestOutputHelper writer;



        public UserControllerTest(ITestOutputHelper writer)
        {
            this.writer = writer;

            userRepoFakeIt = A.Fake<IUserRepository>();

            photoServiceFakeit = A.Fake<IPhotoService>();

            pagedlistFactoryFakeIt = A.Fake<IPagedlistFactory>();

            httpContextAccessor = A.Fake<HttpContextAccessor>();

            mapper = A.Fake<IMapper>();

            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<UserMappingProfile>(); // your mapping profile
            //});

            //mapper = config.CreateMapper(); // initialized manually inside

            userParams = UserDataGetter.GetUserParams();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userParams.CurrentUsername)
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            //SUT --- System Under Test
            userController = new UserController(userRepoFakeIt, mapper, photoServiceFakeit);

            userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

        }

        [Fact]
        public async void UserController_GetAllUsersAsync_ReturnsUsers()
        {
            //Arrange - what i need to bring in

            var user = UserDataGetter.GetAppUser();
            var members = A.Fake<IPagedList<MemberDto>>();

            A.CallTo(() => pagedlistFactoryFakeIt.CreateAsync(A<IQueryable<MemberDto>>._, 1, 10)).Returns(members);
            A.CallTo(() => userRepoFakeIt.GetUserbyUsernameAsync(user.UserName)).Returns(user);
            A.CallTo(() => userRepoFakeIt.GetAllMembersAsync(userParams)).Returns(members);


            //Act
            var result = await userController.GetAllUsersAsync(userParams);


            //Assert -object check actions
            writer.WriteLine("Results: "+ result);
            result.Should().BeOfType<ActionResult<IEnumerable<MemberDto>>>();
            result.Should().NotBeNull();

        }

        
    }
}

