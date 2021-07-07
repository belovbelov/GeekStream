using GeekStream.Core.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}