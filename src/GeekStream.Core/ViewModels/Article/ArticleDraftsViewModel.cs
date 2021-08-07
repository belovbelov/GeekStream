using System;
using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.ViewModels.Article
{
    public class ArticleDraftsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? PublishedDate { get; set; }

        public FilePath CategoryIcon { get; set; }
        public FilePath UserIcon { get; set; }

        public string Category { get; set; }

        public int CategoryId { get; set; }
    }
}
