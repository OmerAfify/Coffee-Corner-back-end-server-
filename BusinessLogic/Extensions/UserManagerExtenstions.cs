using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Extensions
{
    public static class UserManagerExtenstions
    {
        public static async Task<ApplicationUser> GetUserWithClaimsPrincipal(this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.Email == email);

            return user;

        }
        
        public static async Task<ApplicationUser> GetUserAddressWithClaimsPrincipal(this UserManager<ApplicationUser> userManager, ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.Users.Where(u => u.Email == email)
                       .Include(a => a.address).SingleOrDefaultAsync();

            return user;

        }

    }
}
