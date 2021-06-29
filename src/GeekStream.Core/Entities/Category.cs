using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekStream.Core.Entities
{
    public class Category
    {

        public Category(string name, string description, IList<Article> articles)
        {
            
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException(nameof(description));
            }

            Name = name;
            Description = description;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<Article> Articles { get; set; }
    }
}
