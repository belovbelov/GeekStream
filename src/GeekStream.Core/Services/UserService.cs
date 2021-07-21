using System.Collections.Generic;
using System.Security.Principal;
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

        public ApplicationUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(_accessor.HttpContext.User).Result;
        }

        public void Add(RegisterViewModel model)
        {
            var user = new ApplicationUser();
            _userRepository.Add(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public void Edit(ApplicationUser user)
        {
            _userRepository.Edit(user);
        }

        public UserViewModel GetUserByName(string name)
        {
            var user = _userRepository.GetByName(name);
            return new UserViewModel
            {
                UserName = user.UserName
            };
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
    }
}