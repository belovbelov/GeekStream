using System;

namespace GeekStream.Core.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? PublishedDate { get; set; }

        public int Author { get; set; }

        public string Category { get; set; }

        public int Rating { get; set; }
    }
}