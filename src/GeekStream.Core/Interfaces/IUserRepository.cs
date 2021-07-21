using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IUserRepository
    {
        public void Add(ApplicationUser user);
        public void Delete(int id);
        public void Edit(ApplicationUser user);
        public ApplicationUser GetByName(string name);
        public IEnumerable<ApplicationUser> GetAll();
    }
}