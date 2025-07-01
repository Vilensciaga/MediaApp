using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database.Data;
using Database.Interface;
using DataService.Interface;
using MediaApp.Models;
using Microsoft.EntityFrameworkCore;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Service
{
    public class UserRepository: IUserRepository
    {
        private readonly IAppDbContext context;
        private readonly IMapper mapper;
        public UserRepository(IAppDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            return await context.Users
                //.Include(p => p.Photos)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            //project does not need the include clause
        }

        public async Task<MemberDto?> GetMemberByUsernameAsync(string username)
        {
            return await context.Users.Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

        }


        public async Task<AppUser> RegisterUserAsync(RegisterDto register)
        {
            using var hmac = new HMACSHA512();
            AppUser user = new AppUser
            {
                UserName = register.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key

            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;

        }



        public async Task<bool> UserExistsAsync(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName == username.ToLower());

        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public Task UpdateUserAsync(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
            return Task.CompletedTask;
        }




        //older methods
        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await context.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<AppUser?> GetUsersByIdAsync(int id)
        {
            return await context.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(c => c.AppUserId == id);

        }

        public async Task<AppUser?> GetUserbyUsernameAsync(string username)
        {
            return await context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(s => s.UserName == username);
        }
    }
}
