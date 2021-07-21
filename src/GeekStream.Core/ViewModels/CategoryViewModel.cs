using System.Collections.Generic;

namespace GeekStream.Core.ViewModels
{
    public class CategoryViewModel
    {
        
        public string Name { get; set; }

        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}