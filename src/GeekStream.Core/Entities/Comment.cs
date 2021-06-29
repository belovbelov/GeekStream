using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStream.Core.Entities
{
    public class Comment
    {
        public Comment(string name, string content)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException(nameof(content));
            }
        }

        public string Name
        { get; set; }

        public string Content 
        { get; set; }

        public int ArticleID 
        { get; set; }
    }
}
