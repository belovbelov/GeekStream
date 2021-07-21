using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}