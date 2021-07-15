using System.Collections.Generic;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;

namespace GeekStream.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        public ApplicationUser FindById(int id)
        {
            return _userRepository.FindById(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}