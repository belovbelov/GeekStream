using System.Collections.Generic;
using GeekStream.Core.Entities;
using GeekStream.Core.ViewModels.Article;

namespace GeekStream.Core.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FilePath CategoryIcon { get; set; }
        public bool IsSubscribed { get; set; }
        public IEnumerable<ArticleFeedViewModel> Articles { get; set; }
    }
}