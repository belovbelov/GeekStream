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

        public void SaveUser(User user)
        {
            _context.Entry(user).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = GetUser(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User GetUser(int id)
        {
            return _context.Users.SingleOrDefault(x => x.Id == id);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}