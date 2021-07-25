using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeekStream.Core.Interfaces;

namespace GeekStream.Core.Entities
{
    public class Article
    {
        public Article()
        {
            
        }

        public Article(string title, string content)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(nameof(title));
            }

            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException(nameof(content));
            }

            Title = title;
            Content = content;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(120,MinimumLength = 5)]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000,MinimumLength = 2)]
        [Display(Name = "Содержание")]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата публикации")]
        public DateTime? PostedOn { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;


        [Display(Name = "Автор")]
        public ApplicationUser Author { get; set; }

        public int Rating { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        public ICollection<File> Images { get; set; }
    }
}
