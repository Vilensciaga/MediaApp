using Database.Interface;
using DataService.Interface;
using MediaApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
