using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoSQL.Models;

namespace NoSQL.Data
{
    public class NoSQLContext : DbContext
    {
        public NoSQLContext (DbContextOptions<NoSQLContext> options)
            : base(options)
        {
        }

        public DbSet<NoSQL.Models.Book> Book { get; set; } = default!;
    }
}
