using GeekStream.Core.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}