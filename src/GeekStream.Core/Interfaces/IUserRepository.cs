using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IUserRepository
    {
        public ApplicationUser GetByName(string name);
        public IEnumerable<ApplicationUser> GetAll();
        public void Subscribe(Subscription subscription);
        public void Unsubscribe(Subscription subscription);
        public bool IsSubscribed(ApplicationUser user, string subId);
    }
}