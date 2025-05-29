using MediaApp.Models;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Jwt
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
        Task<string> Authenticate(AppUser existingUser, LoginDto user);
    }
}
