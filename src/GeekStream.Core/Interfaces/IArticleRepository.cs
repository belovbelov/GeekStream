using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IArticleRepository
    {
        public void SaveArticle(Article article);
        public void DeleteArticle(int id);
        public void PublishArticle(int id);
        public void UnPublishArticle(int id);
        public Article GetArticle(int id);
        public IEnumerable<Article> GetArticles(int page, int pageSize, string searchString);
    }
}
