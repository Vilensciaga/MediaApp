using MediaApp.Models;
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
        
    }
}
