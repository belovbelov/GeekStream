using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.NetworkInformation;
using GeekStream.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GeekStream.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public File Avatar { get; set; } 

        public ICollection<Article> AuthoredArticles { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}