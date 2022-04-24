using ChatAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppAPI.Data
{
    public static class ChatAppSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new ChatAppContext(
                serviceProvider.GetRequiredService<DbContextOptions<ChatAppContext>>()))
            {
                if (!context.AppUsers.Any())
                {
                    List<AppUser> appUsers = new List<AppUser>() { 
                        new AppUser
                        {
                            UserName = "HelloWorld",
                            Email = "hello@outlook.com"
                        },
                        new AppUser
                        {
                            UserName = "Omega",
                            Email = "omega@outlook.com"
                        },
                        new AppUser
                        {
                            UserName = "Alpha",
                            Email = "alpha@outlook.com"
                        },
                        new AppUser
                        {
                            UserName = "Beta",
                            Email = "beta@outlook.com"
                        }

                    };
                    context.AddRange(appUsers);
                    context.SaveChanges();

                }
            }
        }
    }
}
