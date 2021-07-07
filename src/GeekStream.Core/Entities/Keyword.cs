using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.Entities
{
    public class Keyword
    {
        public Keyword()
        {
            
        }
        
        [Required]
        public int Id { get; set; }

        [Required]
        public string Word { get; set; }
    }
}