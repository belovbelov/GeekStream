using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.ViewModels
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }
        public IEnumerable<Message> Messages { get; set; } 
        public IEnumerable<ChatUser> Users { get; set; }

        public IEnumerable<Chat> Chats { get; set; }
    }
}
