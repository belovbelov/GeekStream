using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IUserRepository
    {
        public ApplicationUser GetByName(string name);
        public int GetUserRating(string userId);
        public void UpdateRating(ApplicationUser user);
        public IEnumerable<ApplicationUser> GetAll();
        public Task SubscribeAsync(Subscription subscription);
        public Task UnsubscribeAsync(Subscription subscription);
        public bool IsSubscribed(ApplicationUser user, string subId);
    }
}