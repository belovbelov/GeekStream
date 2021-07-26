using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.Entities
{
    public class Subscription
    {
        public Subscription()
        {
            
        }

        [Key]
        public ApplicationUser ApplicationUser  { get; set; }

        [Key]
        public string PublishSource { get; set; }

    }
}