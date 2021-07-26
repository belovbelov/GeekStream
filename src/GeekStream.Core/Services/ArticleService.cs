using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GeekStream.Core.Services
{
    public class ArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly UserService _userService;
        private readonly KeywordService _keywordService;

        public ArticleService(IArticleRepository articleRepository, UserService userService, KeywordService keywordService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
            _keywordService = keywordService;
        }

        public IEnumerable<ArticleViewModel> GetAllArticles(string searchString = null)
        {
            return _articleRepository.GetAll(page: 1, pageSize: 20, searchString)
                .Select(article => new ArticleViewModel
                {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                PublishedDate = article.PostedOn,
                Author = article.Author.FirstName + " " + article.Author.LastName,
                AuthorId = article.Author.Id,
                Category = article.Category.Name,
                CategoryId = article.CategoryId,
                Rating = article.Rating
                });

            }

        public async Task SaveArticleAsync(ArticleCreationViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                CreatedOn = DateTime.UtcNow,
                PostedOn = null,
                Author = _userService.GetCurrentUser(),
                CategoryId= model.CategoryId,
                Rating = 1,
            };

            
            article.Keywords = keywords;
            await _articleRepository.SaveAsync(article);
        }

        public ArticleViewModel GetArticleById(int id)
        {
            var article = _articleRepository.GetById(id);
            return new ArticleViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                PublishedDate = article.PostedOn,
                Author = article.Author.FirstName + " " + article.Author.LastName,
                AuthorId = article.Author.Id,
                Category = article.Category.Name,
                CategoryId = article.CategoryId,
                Rating = article.Rating
            };
        }

        public IEnumerable<ArticleViewModel> FindByCategoryId(int id)
        {
            return _articleRepository.FindByCategoryId(id)
                .Select(article => new ArticleViewModel
                 {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                PublishedDate = article.PostedOn,
                Author = article.Author.FirstName + " " + article.Author.LastName,
                AuthorId = article.Author.Id,
                Category = article.Category.Name,
                CategoryId = article.CategoryId,
                Rating = article.Rating
                });
        }

        public IEnumerable<ArticleViewModel> FindByAuthorId(string id)
        {
            return _articleRepository.FindByAuthorId(id)
                .Select(article => new ArticleViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    PublishedDate = article.PostedOn,
                    Author = article.Author.FirstName + " " + article.Author.LastName,
                    AuthorId = article.Author.Id,
                    Category = article.Category.Name,
                    CategoryId = article.CategoryId,
                    Rating = article.Rating
                });
        }

        public IEnumerable<ArticleViewModel> FindBySubscription(string? subscriptionId = null)
        {
            var userId = _userService.GetCurrentUser().Id;
            var articles = _articleRepository.FindBySubscription(userId, subscriptionId)
                .Select(article => new ArticleViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    PublishedDate = article.PostedOn,
                    Author = article.Author.FirstName + " " + article.Author.LastName,
                    AuthorId = article.Author.Id,
                    Category = article.Category.Name,
                    CategoryId = article.CategoryId,
                    Rating = article.Rating
                });

            return articles;
        }
    }
}
