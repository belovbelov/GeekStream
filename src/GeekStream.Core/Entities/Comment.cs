using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStream.Core.Entities
{
    public class Comment
    {
        public Comment(string name, string content, int articleId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException(nameof(content));
            }

            if (articleId == null)
            {
                throw new ArgumentException(nameof(articleId));
            }
        }
        
        public string UserName{ get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Содержимое")]
        public string Content { get; set; }

        public int ArticleId { get; set; }
    }
}
