using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Core.Services
{
    public class ArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly UserService _userService;
        private readonly KeywordService _keywordService;
        private readonly CommentService _commentService;

        public ArticleService(IArticleRepository articleRepository, UserService userService, KeywordService keywordService, CommentService commentService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
            _keywordService = keywordService;
            _commentService = commentService;
        }

        public IEnumerable<ArticleViewModel> GetAllArticles()
        {
            return _articleRepository.GetAll(page: 1, pageSize: 20)
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
                Rating = article.Rating,
                Images = article.Images,
                UserIcon = article.Author.Avatar,
                CategoryIcon = article.Category.Image
                });

            }

        public async Task UpdateArticleRatingAsync(int articleId, int votes)
        {
            var article = _articleRepository.GetById(articleId);
            article.Rating = votes;
            await _articleRepository.UpdateAsync(article);
        }

        public async Task ProcessArticle(ArticleEditViewModel model, string action)
        {
            Article article;
            try
            {
                article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedOn = DateTime.UtcNow,
                    PostedOn = null,
                    Author = _userService.GetCurrentUser(),
                    CategoryId= model.CategoryId,
                    Rating = 0,
                    Images = model.FilePaths,
                    Type = ArticleType.Draft
                };

                await _articleRepository.SaveAsync(article);

                await _keywordService.SaveKeywordsAsync(model.Keywords, article);

            }
            catch (DbUpdateException e)
            {
                article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    CategoryId= model.CategoryId,
                    Images = model.FilePaths
                };
                await _articleRepository.UpdateAsync(article);
            }

            if (action == "Опубликовать")
            {
                article.Type = ArticleType.Ready;
                await _articleRepository.UpdateAsync(article);
            }
        }

        public async Task UpdateArticleAsync(ArticleEditViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                CategoryId= model.CategoryId,
                Images = model.FilePaths
            };
            await _articleRepository.UpdateAsync(article);
        }

        public async Task SaveArticleAsync(ArticleEditViewModel model)
        {
            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                CreatedOn = DateTime.UtcNow,
                PostedOn = DateTime.UtcNow,
                Author = _userService.GetCurrentUser(),
                CategoryId= model.CategoryId,
                Rating = 0,
                Images = model.FilePaths,
                Type = ArticleType.Ready
            };

            await _articleRepository.SaveAsync(article);

            await _keywordService.SaveKeywordsAsync(model.Keywords, article);
        }

        public async Task Approve(int id)
        {
            var article = _articleRepository.GetById(id);
            article.PostedOn = DateTime.UtcNow;
            await _articleRepository.UpdateAsync(article);
            await _commentService.RemoveAll(id);
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = _articleRepository.GetById(id);
            await _articleRepository.DeleteAsync(article);
        }

        public ArticleEditViewModel GetArticleToEditById(int id)
        {
            var article = _articleRepository.GetById(id);
            var keywords = article.Keywords
                .Select(k => k.Word).ToString();
            return new ArticleEditViewModel
            {
                Title = article.Title,
                Content = article.Content,
                CategoryId = article.CategoryId,
                Keywords = keywords,
                FilePaths = article.Images.ToList()
            };
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
                Rating = article.Rating,
                Images = article.Images,
                Comments = article.Comments,
                UserIcon = article.Author.Avatar,
                CategoryIcon = article.Category.Image
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
                Rating = article.Rating,
                Images = article.Images,
                UserIcon = article.Author.Avatar,
                CategoryIcon = article.Category.Image
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
                    Rating = article.Rating,
                    Images = article.Images,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image
                });
        }

        public async Task<IEnumerable<ArticleViewModel>> FindBySubscription(string? subscriptionId = null)
        {
            var userId = _userService.GetCurrentUser().Id;
            var articles = await _articleRepository.FindBySubscription(userId, subscriptionId);
                return articles.Select(article => new ArticleViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content,
                    PublishedDate = article.PostedOn,
                    Author = article.Author.FirstName + " " + article.Author.LastName,
                    AuthorId = article.Author.Id,
                    Category = article.Category.Name,
                    CategoryId = article.CategoryId,
                    Rating = article.Rating,
                    Images = article.Images,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image
                });

        }

        public IEnumerable<ArticleViewModel> FindByKeywords(string? words)
        {
            var keywords = words.Split(" ").ToList();
            return _articleRepository.FindByKeywords(keywords)
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
                    Rating = article.Rating,
                    Images = article.Images,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image
                });
        }

        public IEnumerable<ArticleViewModel> PendingArticles()
        {
            return _articleRepository.GetPending()
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
                    Rating = article.Rating,
                    Images = article.Images,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image
                });
        }
    }
}
