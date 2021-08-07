using System;
using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? PublishedDate { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }
        public FilePath CategoryIcon { get; set; }
        public FilePath UserIcon { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }

        public int Rating { get; set; }

        public IEnumerable<FilePath> Images { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
    }
}
