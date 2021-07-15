using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace GeekStream.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Article> AuthoredArticles { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}