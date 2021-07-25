using System.Collections.Generic;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public void Subscribe(Subscription subscription)
        {
            _context.Subscription.Add(subscription);
            _context.SaveChanges();
        }

        public void Unsubscribe(Subscription subscription)
        {
            _context.Subscription.Remove(subscription);
            _context.SaveChanges();
        }

        public bool IsSubscribed(ApplicationUser user, string subId)
        {
            return _context.Subscription.Any(s => s.ApplicationUser == user && s.PublishSource == subId);
        }
    }
}