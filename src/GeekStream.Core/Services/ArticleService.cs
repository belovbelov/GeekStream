﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task UpdateArticleAsync(ArticleEditViewModel model, string action)
        {
            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                PostedOn = null,
                Author = _userService.GetCurrentUser(),
                CategoryId= model.CategoryId,
                Rating = 0,
                Images = model.FilePaths
            };

            if (action == "Опубликовать")
            {
                article.Type = ArticleType.Ready;
                await _articleRepository.UpdateAsync(article);
            }

            if (action == "Статья одобрена")
            {
                await Post(model.Id);
            }

            if (action == "Скрыть")
            {
                
                await Post(model.Id);
            }

            if (action == null)
            {
                await _articleRepository.UpdateAsync(article);
            }
        }

        public async Task SaveArticleAsync(ArticleEditViewModel model,string action)
        {
            var article = new Article
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
            if (action == "Опубликовать")
            {
                article.Type = ArticleType.Ready;
                await _articleRepository.UpdateAsync(article);
            }
        }

        public async Task Approve(int id)
        {
            var article = _articleRepository.GetById(id);
            article.Type = ArticleType.Approved;
            await _articleRepository.UpdateAsync(article);
        }


        public async Task Post(int id)
        {
            var article = _articleRepository.GetById(id);
            if (article.Type == ArticleType.Hidden)
            {
                
                article.Type = ArticleType.Posted;
            }

            if (article.Type == ArticleType.Posted)
            {
                article.Type = ArticleType.Hidden;
            }

            if (article.Type == ArticleType.Approved)
            {
                article.PostedOn = DateTime.UtcNow;
                article.Rating = 0;
                article.Type = ArticleType.Posted;
            }
            await _articleRepository.UpdateAsync(article);
            await _commentService.RemoveAll(id);
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = _articleRepository.GetById(id);
            if (article.Type == ArticleType.Posted)
            {
                return;
            }
            await _articleRepository.DeleteAsync(article);
        }

        public ArticleEditViewModel GetArticleToEditById(int id)
        {
            var article = _articleRepository.GetById(id);

            if (article.Author.Id != _userService.GetCurrentUser().Id)
            {
                return null;
            }
            var keywords = article.Keywords
                .Select(k => k.Word).ToList().Aggregate((i, j) => i + " " + j);
            return new ArticleEditViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                CategoryId = article.CategoryId,
                Keywords = keywords,
                FilePaths = article.Images.ToList(),
                ArticleType = article.Type
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

        public IEnumerable<ArticleViewModel> GetDrafts()
        {
            var userId = _userService.GetCurrentUser().Id;
            var articles = _articleRepository.GetDrafts(userId)
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
                    CategoryIcon = article.Category.Image,
                });

            return articles;
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
