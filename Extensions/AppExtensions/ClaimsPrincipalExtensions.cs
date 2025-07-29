using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.AppExtensions
{
    public static class ClaimsPrincipalExtensions
    {
        //extensiom method to get the currently logged in user username from the claims
        public static string GetUsername(this ClaimsPrincipal User)
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        }
    }
}
