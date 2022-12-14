using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyResume.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyResume.Domain.Models.DataContexts
{
    public static class MyResumeDbSeed
    {
        public static IApplicationBuilder SeedMembership(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<MyResumeUser>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MyResumeUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<MyResumeRole>>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                string superAdminRoleName = configuration["defaultAccount:superAdmin"];
                string superAdminEmail = configuration["defaultAccount:email"];
                string superAdminUserName = configuration["defaultAccount:username"];
                string superAdminPassword = configuration["defaultAccount:password"];

                var superAdminRole = roleManager.FindByNameAsync(superAdminRoleName).Result;


                if (superAdminRole == null)
                {
                    superAdminRole = new MyResumeRole
                    {
                        Name = superAdminRoleName,
                    };

                    var roleResult = roleManager.CreateAsync(superAdminRole).Result;

                    if (!roleResult.Succeeded)
                    {
                        throw new Exception("Problem at RoleCreating.....");
                    }
                }

                var superAdminUser = userManager.FindByEmailAsync(superAdminEmail).Result;

                if (superAdminUser == null)
                {
                    superAdminUser = new MyResumeUser
                    {
                        Email = superAdminEmail,
                        UserName = superAdminUserName,
                        EmailConfirmed = true
                    };

                    var userResult = userManager.CreateAsync(superAdminUser, superAdminPassword).Result;

                    if (!userResult.Succeeded)
                    {
                        throw new Exception("Problem occurred when user is created");
                    }
                }

                var isInRole = userManager.IsInRoleAsync(superAdminUser, superAdminRole.Name).Result;

                if (isInRole != true)
                {
                    userManager.AddToRoleAsync(superAdminUser, superAdminRole.Name).Wait();
                }

            }


            return app;
        }
        public static void SeedUserRole(RoleManager<MyResumeRole> roleManager)
        {

            if (!roleManager.RoleExistsAsync("user").Result)
            {
                MyResumeRole role = new MyResumeRole();
                role.Name = "user";

                IdentityResult roleResult = roleManager.

                CreateAsync(role).Result;
            }

        }
    }
}
