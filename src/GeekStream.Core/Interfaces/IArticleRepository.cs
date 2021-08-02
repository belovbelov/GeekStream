using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.ViewModels;

namespace GeekStream.Core.Interfaces
{
    public interface IArticleRepository
    {
        public Task SaveAsync(Article article);
        public Task UpdateAsync(Article article);
        public Task DeleteAsync(Article article);
        public Article GetById(int id);
        public IEnumerable<Article> GetAll(int page, int pageSize);
        public IEnumerable<Article> FindByCategoryId(int id);
        public IEnumerable<Article> FindByAuthorId(string name);
        public Task<IEnumerable<Article>> FindBySubscription(string currentUserId,string subscriptionId);
        public IEnumerable<Article> FindByKeywords(List<string> words);
    }
}
