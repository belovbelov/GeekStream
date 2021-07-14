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
            return _articleRepository.GetArticles(page: 1, pageSize: 20, searchString)
                .Select(article => new ArticleViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    PublishedDate = article.PostedOn,
                    // Author = article.AuthorId,
                    Category = article.Categories?.First().Name,
                    Rating = article.Rating
                });
        }

        public async Task SaveArticleAsync(ArticleViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                PostedOn = model.PublishedDate,
                // AuthorId = model.Author,
                Categories = new List<Category>(),
                Rating = 1
            };
            await _articleRepository.SaveArticleAsync(article);
        }
    }
}
