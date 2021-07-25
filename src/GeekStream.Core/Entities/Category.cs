using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using GeekStream.Core.Interfaces;

namespace GeekStream.Core.Entities
{
    public class Category
    {
        public Category()
        {
            
        }

        public Category(string name, string description, IEnumerable<Article> articles)
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
        
        [Required]
        [StringLength(32,MinimumLength = 5)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        public File Image { get; set; }

        [Required]
        [StringLength(200,MinimumLength = 10)]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
