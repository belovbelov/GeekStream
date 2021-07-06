using GeekStream.Core.Entities;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class ArticleDbContext : DbContext
    {
        public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options)
        {
            
        }

        public DbSet<Article> Articles { get; set; }
    }
}