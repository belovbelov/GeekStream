using GeekStream.Core.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class KeywordDbContext : DbContext
    {
        public KeywordDbContext(DbContextOptions<KeywordDbContext> options) : base(options)
        {
            
        }

        public DbSet<Keyword> Keywords { get; set; }
    }
}