using System.Collections.Generic;
using GeekStream.Core.Entities;
using GeekStream.Core.ViewModels;

namespace GeekStream.Core.Interfaces
{
    public interface IUserRepository
    {
        public void Add(ApplicationUser user);
        public void Delete(int id);
        public void Edit(ApplicationUser user);
        public ApplicationUser FindById(int id);
        public IEnumerable<ApplicationUser> GetAll();
    }
}