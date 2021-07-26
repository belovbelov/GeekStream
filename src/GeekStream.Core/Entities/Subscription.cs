using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Core.Entities
{
    public class Subscription
    {
        public Subscription()
        {
            
        }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string PublishSource { get; set; }

    }
}