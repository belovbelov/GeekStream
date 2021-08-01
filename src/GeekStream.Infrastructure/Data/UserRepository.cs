using System.Collections.Generic;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStream.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }


        public void UpdateRating(ApplicationUser user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public ApplicationUser GetByName(string id)
        {
            return _context.Users
                .Include(u => u.Avatar)
                .SingleOrDefault(a => a.Id == id);
        }

        public int GetUserRating(string userId)
        {
            var rating = _context.Votes
                .Include(v => v.Article)
                .Where(v => v.Article.Author.Id == userId)
                .Sum(v => (int) v.Type);
            rating += _context.VotesOnReplies
                .Include(v => v.Comment)
                .Where(v => v.Comment.ApplicationUserId == userId)
                .Sum(v => (int) v.Type);
            return rating;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users
                .Include(u => u.Avatar)
                .ToList();
        }

        public async Task SubscribeAsync(Subscription subscription)
        {
            _context.Add(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task UnsubscribeAsync(Subscription subscription)
        {
            _context.Subscription.Remove(subscription);
            await _context.SaveChangesAsync();

        }

        public bool IsSubscribed(ApplicationUser user, string? subId)
        {
            if (subId != null)
            {
                return _context.Subscription.Any(s => s.ApplicationUser == user && s.PublishSource == subId);
            }
            return _context.Subscription.Any(s => s.ApplicationUser == user);
        }
    }
}