using System.Collections.Generic;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _userRepository.GetUsers();
        }
    }
}