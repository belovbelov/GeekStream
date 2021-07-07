using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IUserRepository
    {
        public void SaveUser(User user);
        public void DeleteUser(int id);
        public User GetUser(int id);
        public ICollection<User> GetUsers();
    }
}