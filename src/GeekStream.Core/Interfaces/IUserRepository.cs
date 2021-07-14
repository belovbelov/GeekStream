using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IUserRepository
    {
        public void SaveUser(ApplicationUser user);
        public void DeleteUser(int id);
        public ApplicationUser GetUser(int id);
        public IEnumerable<ApplicationUser> GetUsers();
    }
}