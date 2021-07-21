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
//----------------------------------------------------------------
        public void Add(ApplicationUser user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = (id);
            if (user != null)
            {
                _context.SaveChanges();
            }
        }

        public void Edit(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
//----------------------------------------------------------------
        public ApplicationUser GetByName(string name)
        {
            return _context.Users.SingleOrDefault(a => a.UserName == name);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Users.ToList();
        }
    }
}