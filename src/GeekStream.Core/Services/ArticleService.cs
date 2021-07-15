using System;
using System.Collections.Generic;
using System.Linq;
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
                // Author = article.Author,
                Category = article.Category.Name,
                Rating = article.Rating
                });

            }

        public async Task SaveArticleAsync(ArticleCreationViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                CreatedOn = DateTime.Now,
                PostedOn = DateTime.MinValue,
                // AuthorId = model.Author,
                CategoryId= model.CategoryId,
                Rating = 1
            };
            await _articleRepository.SaveArticleAsync(article);
        }

        public ArticleViewModel GetArticleById(int id)
        {
            var article = _articleRepository.GetArticle(id);
            return new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                PublishedDate = article.PostedOn,
                // Author = article.Author,
                Category = article.Category.Name,
                Rating = article.Rating
            };
        }

        public IEnumerable<ArticleViewModel> FindByCategoryId(string id = null)
        {
            return _articleRepository.FindByCategoryId(id)
                .Select(article => new ArticleViewModel
                 {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                PublishedDate = article.PostedOn,
                // Author = article.Author,
                Category = article.Category.Name,
                Rating = article.Rating
                });
        }
    }
}
