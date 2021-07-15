using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.ViewModels;

namespace GeekStream.Core.Interfaces
{
    public interface IArticleRepository
    {
        public Task SaveArticleAsync(Article article);
        public void DeleteArticle(int id);
        public void PublishArticle(int id);
        public void UnPublishArticle(int id);
        public Article GetArticle(int id);
        public IEnumerable<ArticleViewModel> GetArticles(int page, int pageSize, string searchString);
        public IEnumerable<ArticleViewModel> FindByCategoryId(string id);
    }
}
