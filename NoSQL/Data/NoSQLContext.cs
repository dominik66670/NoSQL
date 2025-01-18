using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoSQL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NoSQL.Data
{
    public class NoSQLContext : IdentityDbContext<IdentityUser>
    {
        public NoSQLContext (DbContextOptions<NoSQLContext> options)
            : base(options)
        {
        }

        public DbSet<NoSQL.Models.Book> Book { get; set; } = default!;
    }
}
