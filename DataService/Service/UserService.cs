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
    public class UserService: IUserService
    {
        private readonly IAppDbContext context;
        public UserService(IAppDbContext context) 
        {
            this.context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUsersByIdAsync(int id)
        {
            return await context.Users.FirstOrDefaultAsync(c => c.AppUserId == id);
        }

        public async Task<AppUser>GetUserbyUsername(string username)
        {
            return await context.Users.SingleOrDefaultAsync(s => s.UserName == username);
        }

        public async Task<AppUser> RegisterUser(RegisterDto register)
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


        
        public async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName == username.ToLower());
    
        }
    }
}
