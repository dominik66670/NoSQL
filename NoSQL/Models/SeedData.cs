using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoSQL.Data;
using System;
using System.Linq;
namespace NoSQL.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new NoSQLContext(serviceProvider.GetRequiredService<DbContextOptions<NoSQLContext>>()))
            {
                if (context.Roles.Any())
                {
                    return;
                }
                else
                {
                    var roles = new[] { "Admin", "Customer", "Moderator" };
                    foreach (var role in roles)
                    {
                        context.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole(role));
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
