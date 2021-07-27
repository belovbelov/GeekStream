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

        public ApplicationUser GetByName(string id)
        {
            return _context.Users.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users.ToList();
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