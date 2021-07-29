using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string UserMail { get; set; }

        public bool IsSubscribed { get; set; }
        public IEnumerable<ArticleViewModel> Articles { get; set; }
    }
}