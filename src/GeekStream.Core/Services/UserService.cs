using System.Collections.Generic;
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

        public ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(_accessor.HttpContext.User).Result;
        }

        public void Subscribe(string subscriptionId)
        {
            var user = GetCurrentUser();
            var subscription = new Subscription
            {
                ApplicationUser = user,
                PublishSource = subscriptionId
            };
            _userRepository.Subscribe(subscription);
        }

        public void Unsubscribe(string subscriptionId)
        {
            var user = GetCurrentUser();
            var subscription = new Subscription
            {
                ApplicationUser = user,
                PublishSource = subscriptionId
            };
            _userRepository.Unsubscribe(subscription);
        }

        public UserViewModel GetUserById(string id)
        {
            var user = _userRepository.GetByName(id);
            return new UserViewModel
            {
                Id = user.Id,
                UserName = user.FirstName + " " + user.LastName,
                IsSubscribed = IsSubscribed(GetCurrentUser(), user.Id
                )};
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public bool IsSubscribed(ApplicationUser user, string subId)
        {
            return _userRepository.IsSubscribed(user, subId);
        }
    }
}