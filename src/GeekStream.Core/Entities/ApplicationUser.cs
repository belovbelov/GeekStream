using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GeekStream.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Rating { get; set; }

        public int? AvatarId { get; set; }
        public FilePath? Avatar { get; set; }

        public IEnumerable<Article> AuthoredArticles { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Subscription> Subscriptions { get; set; }

        public IEnumerable<ChatUser> Chats { get; set; }
    }
}