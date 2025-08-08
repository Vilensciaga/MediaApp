using AutoMapper;
using Database.Data;
using Database.Interface;
using DataService.Service;
using FakeItEasy;
using FluentAssertions;
using Helpers.Helpers;
using Helpers.Helpers.PageList;
using MediaApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using Models.Dtos.User;
using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace API.Test.Repository.Test
{
    public class UserRepositoryTests
    {
        private IPagedlistFactory pagedlistFactoryFakeIt;
        private readonly IMapper mapper;
        private readonly ITestOutputHelper output;


        public UserRepositoryTests(ITestOutputHelper output)
        {
            this.output = output;

            pagedlistFactoryFakeIt = A.Fake<IPagedlistFactory>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMappingProfile>(); // your mapping profile
            });

            mapper = config.CreateMapper(); // initialized manually inside
        }
        public async Task<IAppDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbcontext = new AppDbContext(options);
            dbcontext.Database.EnsureCreated();

            if(await dbcontext.Users.CountAsync() < 0)
            {
                var path = Path.GetFullPath(
                        Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Repository.Test", "SeedingData", "UserSeedData.json"));

                var userData = System.IO.File.ReadAllText(path);
                var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();
                    user.UserName = user.UserName.ToLower();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1"));
                    user.PasswordSalt = hmac.Key;

                    await dbcontext.Users.AddAsync(user);
                }

            }
            return dbcontext;

        }



        [Fact]
        //testing add user from my UserRepository
        public async void ClubRepository_Add_ReturnsAppUser()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var userRepo = new UserRepository(dbContext, mapper, pagedlistFactoryFakeIt);
            var registerUser = UserDataGetter.GetRegisterDto();

            //Act
    
            var result = await userRepo.RegisterUserAsync(registerUser);

            //Assert
            output.WriteLine("username: " + result.UserName);
            result.Should().BeOfType<AppUser>();

        }


    }
}
