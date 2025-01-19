using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoSQL.Data;
using System;
using System.Linq;
namespace NoSQL.Models
{
    // dodać hashowanie w ssedowaniu
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new NoSQLContext(serviceProvider.GetRequiredService<DbContextOptions<NoSQLContext>>()))
            {
                if (context.Roles.Any())
                {
                   
                }
                else
                {
                    var roles = new[] {
                         new Microsoft.AspNetCore.Identity.IdentityRole("Admin"){ NormalizedName="ADMIN"},
                         new Microsoft.AspNetCore.Identity.IdentityRole("Customer"){ NormalizedName="CUSTOMER"},
                         new Microsoft.AspNetCore.Identity.IdentityRole("Moderator"){ NormalizedName="CUSTOMER"}
                    };
                    foreach (var role in roles)
                    {
                        context.Roles.Add(role);
                    }
                }
                
                context.SaveChanges();
            }
        }
    }
}
