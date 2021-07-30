using GeekStream.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Subscription>()
                .HasKey(s => new {s.PublishSource, s.ApplicationUserId}
                );

            builder.Entity<VoteOnPost>()
                .HasKey(v => new {v.ApplicationUserId, v.ArticleId}
                );

            builder.Entity<VoteOnReply>()
                .HasKey(v => new {v.ApplicationUserId, v.CommentId}
                );
        }

        public new DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<FilePath> Files { get; set; }
        public DbSet<VoteOnPost> Votes { get; set; }
        public DbSet<VoteOnReply> VotesOnReplies { get; set; }
    }
}