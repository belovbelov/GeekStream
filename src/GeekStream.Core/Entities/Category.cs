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
        [Display(Name = "Название")]
        public string Name { get; set; }

        public int ImageId { get; set; }
        public FilePath Image { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
