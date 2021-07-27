using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public IEnumerable<Article> AuthoredArticles { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}