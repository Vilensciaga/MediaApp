using MediaApp.Models;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Interface
{
    public interface IUserService
    {
        public  Task<IEnumerable<AppUser>> GetAllUsersAsync();


        public Task<AppUser> GetUsersByIdAsync(int id);

        public Task<AppUser> RegisterUser(RegisterDto register);

        public Task<bool> UserExists(string username);

        public Task<AppUser> GetUserbyUsername(string username);

        //public Task<bool> Authenticate(LoginDto user, AppUser existingUser);


    }
}
