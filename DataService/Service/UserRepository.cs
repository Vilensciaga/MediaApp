using AutoMapper;
using AutoMapper.QueryableExtensions;
using Database.Data;
using Database.Interface;
using DataService.Interface;
using Helpers.Helpers;
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


        public async Task<PagedList<MemberDto>> GetAllMembersAsync(UserParams userParams)
        {
            var query = context.Users
                //.Include(p => p.Photos)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .AsNoTracking();

            //projectTo does not need the include clause

            return await PagedList<MemberDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);


            //var users = await context.Users
            //    .Include(u => u.Photos)
            //    .ToListAsync();

            //return mapper.Map<IEnumerable<MemberDto>>(users);
        }

        public async Task<MemberDto> GetMemberByUsernameAsync(string username)
        {
            return await context.Users.Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            //var users = await context.Users
            //            .Include(u => u.Photos)
            //            .ToListAsync();

            //return mapper.Map<MemberDto>(users);

        }


        public async Task<AppUser> RegisterUserAsync(RegisterDto register)
        {
            using var hmac = new HMACSHA512();
            var user = mapper.Map<AppUser>(register);

            user.UserName = register.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
            user.PasswordSalt = hmac.Key;
            user.CreatedAt = DateTime.UtcNow;

            

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
