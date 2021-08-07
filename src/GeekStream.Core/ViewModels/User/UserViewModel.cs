using System.Collections.Generic;
using GeekStream.Core.Entities;
using GeekStream.Core.ViewModels.Article;

namespace GeekStream.Core.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string UserMail { get; set; }
        public FilePath UserIcon { get; set; }

        public bool IsSubscribed { get; set; }
        public IEnumerable<ArticleFeedViewModel> Articles { get; set; }
    }
}