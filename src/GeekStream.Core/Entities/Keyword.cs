using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeekStream.Core.Entities
{
    public class Keyword
    {
        public Keyword()
        {
            
        }
        [Key]
        public string Word { get; set; }

        [Key]
        public Article Article { get; set; }
    }
}