using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;

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

        public async Task<ChatViewModel> GetChat(int id)
        {
            var chat = _chatRepository.GetChat(id);
            var chatViewModel = new ChatViewModel
            {
                Id = chat.Id,
                Name = chat.Name,
                Type = chat.Type,
                Messages = chat.Messages,
                Users = chat.Users,
                Chats = await _chatRepository.GetPrivateChats(_userService.GetCurrentUser().Id)
            };
            return chatViewModel;
        }

        public async Task<IEnumerable<Chat>> GetPrivateChats()
        {
            var userId = _userService.GetCurrentUser().Id;
            return await _chatRepository.GetPrivateChats(userId);
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