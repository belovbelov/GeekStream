using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GeekStream.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;


        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager,IHttpContextAccessor accessor)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _accessor = accessor;
        }


        public void UpdateUserRating(string userId, int rating)
        {
            var user = GetUserById(userId);
            user.Rating = rating;
            _userRepository.UpdateRating(user);
        }

        public int GetUserRating(string userId)
        {
            return _userRepository.GetUserRating(userId);
        }
        public ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(_accessor.HttpContext.User).Result;
        }

        public async Task SubscribeAsync(string subscriptionId)
        {
            var user = GetCurrentUser();
            var subscription = new Subscription
            {
                ApplicationUser = user,
                PublishSource = subscriptionId
            };
            await _userRepository.SubscribeAsync(subscription);
        }

        public async Task UnsubscribeAsync(string subscriptionId)
        {
            var user = GetCurrentUser();
            var subscription = new Subscription
            {
                ApplicationUser = user,
                PublishSource = subscriptionId
            };
            await _userRepository.UnsubscribeAsync(subscription);
        }

        public ApplicationUser GetUserById(string id)
        {
            return _userRepository.GetByName(id);
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public bool IsSubscribed(ApplicationUser user, string subId = null)
        {
            return _userRepository.IsSubscribed(user, subId);
        }
    }
}