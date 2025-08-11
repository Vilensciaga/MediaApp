using DataService.Interface;
using F23.Kernel;
using F23.Kernel.Results;
using FakeItEasy;
using FluentAssertions;
using Helpers.Helpers;
using Helpers.Helpers.PageList;
using MediaApp.Models;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Member.GetMembers;

namespace API.Test.Handler.Tests
{
    public class GetMembersQueryHandlerTest
    {
        private IUserRepository userRepository;
        private IValidator<GetMembersQuery> validator;
        private GetMembersQueryHandler queryHandlerTest;

        public GetMembersQueryHandlerTest()
        {
            userRepository = A.Fake<IUserRepository>();
            validator = A.Fake<IValidator<GetMembersQuery>>();
            queryHandlerTest = new GetMembersQueryHandler(userRepository, validator);
        }


        [Fact]
        public async void HandlerTest()
        {
            //Arrange
            var user = UserDataGetter.GetAppUser();
            var query = new GetMembersQuery
            {
                Username = "lisanaoooo",
                UserParams = UserDataGetter.GetUserParams()
            };
            var members = A.Fake<IPagedList<MemberDto>>();
            var validationPassedResult = A.Fake<ValidationPassedResult>();

            A.CallTo(() => validator.Validate(query, default)).Returns(validationPassedResult);
            A.CallTo(() => userRepository.GetUserbyUsernameAsync(query.Username)).Returns(user);
            A.CallTo(()=> userRepository.GetAllMembersAsync(query.UserParams)).Returns(members);


            //Act

            var result = await queryHandlerTest.Handle(query, default);




            //Assert
            result.Should().BeAssignableTo<Result<GetMembersQueryResult>>();

        }

    }
}
