using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;

namespace GeekStream.Core.Services
{
    public class ArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IEnumerable<ArticleViewModel> GetArticles(string searchString = null)
        {
            return _articleRepository.GetArticles(page: 1, pageSize: 20, searchString);
            }

        public async Task SaveArticleAsync(ArticleCreationViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                CreatedOn = DateTime.Now,
                PostedOn = null,
                // AuthorId = model.Author,
                CategoryId= model.CategoryId,
                Rating = 1
            };
            await _articleRepository.SaveArticleAsync(article);
        }

        public IEnumerable<ArticleViewModel> FindByCategoryId(string id = null)
        {
            return _articleRepository.FindByCategoryId(id);
        }
    }
}
