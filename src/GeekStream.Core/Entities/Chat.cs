using System.Collections.Generic;

namespace GeekStream.Core.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }
        public IEnumerable<Message> Messages { get; set; } = new();
        public IEnumerable<ChatUser> Users { get; set; } = new();
    }
}