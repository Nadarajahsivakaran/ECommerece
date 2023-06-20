using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repositories
{
    public static class DbSeeder
    {
        public static async Task SeedRoleAndAdmin(IServiceProvider service)
        {
            // Seed Role
            var _userManager = service.GetService<UserManager<ApplicationUser>>();
            var _roleManager = service.GetService<RoleManager<IdentityRole>>();
            _roleManager.CreateAsync(new IdentityRole(Roles.USERS.ToString())).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString())).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Roles.SUPER_ADMIN.ToString())).GetAwaiter().GetResult();

            var user = new ApplicationUser
            {
                Name = "Admin",
                UserName = "admin@gmail.com",
                Email  = "admin@gmail.com",
                Address = "Jaffna"
            };

            var isUserInDb = _userManager.FindByEmailAsync(user.Email).GetAwaiter().GetResult();

            if(isUserInDb == null)
            {
                _userManager.CreateAsync(user, "Admin@123").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, Roles.ADMIN.ToString()).GetAwaiter().GetResult();
            }

        }
    }
}
