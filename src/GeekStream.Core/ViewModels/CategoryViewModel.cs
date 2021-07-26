using System.Collections.Generic;

namespace GeekStream.Core.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribed { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}