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

        public ArticleService(IArticleRepository articleRepository, UserService userService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
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
                Author = article.Author.UserName,
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
                Author = _userService.GetCurrentUser(),
                CategoryId= model.CategoryId,
                Rating = 1
            };
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
                Author = article.Author.UserName,
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
                Author = article.Author.UserName,
                Category = article.Category.Name,
                Rating = article.Rating
                });
        }

        public IEnumerable<ArticleViewModel> FindByAuthorName(string name)
        {
            return _articleRepository.FindByAuthorName(name)
                .Select(article => new ArticleViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    PublishedDate = article.PostedOn,
                    Author = article.Author.UserName,
                    Category = article.Category.Name,
                    Rating = article.Rating
                });
        }
    }
}
