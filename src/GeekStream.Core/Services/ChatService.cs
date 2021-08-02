using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Core.Services
{
    public class ChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly UserService _userService;

        public ChatService(IChatRepository chatRepository, UserService userService)
        {
            _chatRepository = chatRepository;
            _userService = userService;
        }

        public async Task JoinRoom(int id)
        {
            var userId = _userService.GetCurrentUser().Id;
            await _chatRepository.JoinRoom(id, userId);
        }

        public IEnumerable<Chat> GetChats()
        {
            var userId = _userService.GetCurrentUser().Id;
            return _chatRepository.GetChats(userId);
        }

        public Chat GetChat(int id)
        {
            return _chatRepository.GetChat(id);
        }

        public IEnumerable<Chat> GetPrivateChats()
        {
            var userId = _userService.GetCurrentUser().Id;
            return _chatRepository.GetPrivateChats(userId);
        }

        public async Task<Message> CreateMessage(int roomId, string message)
        {
            var userName = _userService.GetCurrentUser().FirstName + " " + _userService.GetCurrentUser().LastName;
            return await _chatRepository.CreateMessage(roomId, message, userName);
        }

        public async Task<int> CreatePrivateRoom(string userId)
        {
            var rootId = _userService.GetCurrentUser().Id;
            return await _chatRepository.CreatePrivateRoom(rootId, userId);
        }
    }
}