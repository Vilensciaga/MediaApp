using MediaApp.Models;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.Helpers;
using Helpers.Helpers.PageList;
namespace DataService.Interface
{
    public interface IUserRepository
    {
        public Task<IPagedList<MemberDto>> GetAllMembersAsync(UserParams userParams);

        public Task<MemberDto> GetMemberByUsernameAsync(string username /*, CancellationToken cancellationToken = default*/);

        public Task<AppUser> RegisterUserAsync(RegisterDto register);

        public Task<bool> UserExistsAsync(string username);

        public Task<AppUser> GetUserbyUsernameAsync(string username);

        public Task UpdateUserAsync(AppUser user);

        public Task<bool> SaveAllAsync();

        public  Task<IEnumerable<AppUser>> GetAllUsersAsync();

        public Task<AppUser> GetUsersByIdAsync(int id);

   

    }
}
