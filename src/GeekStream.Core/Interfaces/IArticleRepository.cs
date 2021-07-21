using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IArticleRepository
    {
        public Task SaveAsync(Article article);
        public void Delete(int id);
        public void Publish(int id);
        public void UnPublish(int id);
        public Article GetById(int id);
        public IEnumerable<Article> GetAll(int page, int pageSize, string searchString);
        public IEnumerable<Article> FindByCategoryId(string id);
        public IEnumerable<Article> FindByAuthorName(string name);
    }
}
