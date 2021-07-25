using System.Collections.Generic;
using GeekStream.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GeekStream.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Article> AuthoredArticles { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}