using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;
using GeekStream.Core.ViewModels.Article;

namespace GeekStream.Core.Services
{
    public class ArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly UserService _userService;
        private readonly KeywordService _keywordService;
        private readonly CommentService _commentService;

        private const int ArticleFeedContentLength = 256;

        public ArticleService(IArticleRepository articleRepository, UserService userService, KeywordService keywordService, CommentService commentService)
        {
            _articleRepository = articleRepository;
            _userService = userService;
            _keywordService = keywordService;
            _commentService = commentService;
        }

        public IEnumerable<ArticleFeedViewModel> GetAllArticles()
        {
            return _articleRepository.GetAll(page: 1, pageSize: 20)
                .Select(article => new ArticleFeedViewModel
                {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content[..Math.Min(article.Content.Length, ArticleFeedContentLength)] + (article.Content.Length > ArticleFeedContentLength ? "..." : ""),
                PublishedDate = article.PostedOn,
                Author = article.Author.FirstName + " " + article.Author.LastName,
                AuthorId = article.Author.Id,
                Category = article.Category.Name,
                CategoryId = article.CategoryId,
                Rating = article.Rating,
                UserIcon = article.Author.Avatar,
                CategoryIcon = article.Category.Image,
                CommentCount = article.Comments.Any() ? article.Comments.Count() : 0
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
            var article = _articleRepository.GetById(model.Id);
            article.Title = model.Title;
                 article.Content = model.Content;
                 article.Author = _userService.GetCurrentUser();
                 article.CategoryId = model.CategoryId;

            if (action == "Опубликовать")
            {
                article.Type = ArticleType.Ready;
                await _articleRepository.UpdateAsync(article);
            }

            if (action == "Статья одобрена")
            {
                await Post(article);
            }

            if (action == "Скрыть")
            {
                
                await Hide(article);
            }

            if (action == "Сохранить черновик")
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


        public async Task Hide(Article article)
        {

            article.Type = ArticleType.Hidden;
            await _articleRepository.UpdateAsync(article);
        }

        public async Task Post(Article article)
        {
            if (article.Type == ArticleType.Hidden)
            {
                article.Type = ArticleType.Posted;
            }

            if (article.Type == ArticleType.Approved)
            {
                article.PostedOn = DateTime.UtcNow;
                article.Rating = 0;
                article.Type = ArticleType.Posted;
                await _commentService.RemoveAll(article.Id);
            }
            await _articleRepository.UpdateAsync(article);
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

        public Article GetArticleById(int id)
        {
            var article = _articleRepository.GetById(id);
            return article;
        }

        public IEnumerable<ArticleFeedViewModel> FindByCategoryId(int id)
        {
            return _articleRepository.FindByCategoryId(id)
                .Select(article => new ArticleFeedViewModel
                 {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content[..Math.Min(article.Content.Length, ArticleFeedContentLength)] + (article.Content.Length > ArticleFeedContentLength ? "..." : ""),
                PublishedDate = article.PostedOn,
                Author = article.Author.FirstName + " " + article.Author.LastName,
                AuthorId = article.Author.Id,
                Category = article.Category.Name,
                CategoryId = article.CategoryId,
                Rating = article.Rating,
                UserIcon = article.Author.Avatar,
                CategoryIcon = article.Category.Image,
                    CommentCount = article.Comments.Any() ? article.Comments.Count() : 0
                });
        }

        public IEnumerable<ArticleFeedViewModel> FindByAuthorId(string id)
        {
            return _articleRepository.FindByAuthorId(id)
                .Select(article => new ArticleFeedViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content[..Math.Min(article.Content.Length, ArticleFeedContentLength)] + (article.Content.Length > ArticleFeedContentLength ? "..." : ""),
                    PublishedDate = article.PostedOn,
                    Author = article.Author.FirstName + " " + article.Author.LastName,
                    AuthorId = article.Author.Id,
                    Category = article.Category.Name,
                    CategoryId = article.CategoryId,
                    Rating = article.Rating,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image,
                    CommentCount = article.Comments.Any() ? article.Comments.Count() : 0
                });
        }

        public async Task<IEnumerable<ArticleFeedViewModel>> FindBySubscription(string subscriptionId = null)
        {
            var userId = _userService.GetCurrentUser().Id;
            var articlesByCategory= await _articleRepository.FindByCategorySubscription(userId, subscriptionId);
            var articlesByAuthor = await _articleRepository.FindByAuthorSubscription(userId);
            var articles = articlesByAuthor.Union(articlesByCategory).Distinct(new ArticlesComparer());
                return articles.Select(article => new ArticleFeedViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content[..Math.Min(article.Content.Length, ArticleFeedContentLength)] + (article.Content.Length > ArticleFeedContentLength ? "..." : ""),
                    PublishedDate = article.PostedOn,
                    Author = article.Author.FirstName + " " + article.Author.LastName,
                    AuthorId = article.Author.Id,
                    Category = article.Category.Name,
                    CategoryId = article.CategoryId,
                    Rating = article.Rating,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image, 
                    CommentCount = article.Comments.Any() ? article.Comments.Count() : 0
                });

        }

        public IEnumerable<ArticleFeedViewModel> FindByKeywords(string? words)
        {
            var keywords = words.Split(" ").ToList();
            return _articleRepository.FindByKeywords(keywords)
                  .Select(article => new ArticleFeedViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content[..Math.Min(article.Content.Length, ArticleFeedContentLength)] + (article.Content.Length > ArticleFeedContentLength ? "..." : ""),
                    PublishedDate = article.PostedOn,
                    Author = article.Author.FirstName + " " + article.Author.LastName,
                    AuthorId = article.Author.Id,
                    Category = article.Category.Name,
                    CategoryId = article.CategoryId,
                    Rating = article.Rating,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image,
                    CommentCount = article.Comments.Any() ? article.Comments.Count() : 0
                });
        }

        public IEnumerable<ArticleDraftsViewModel> GetDrafts()
        {
            var userId = _userService.GetCurrentUser().Id;
            var articles = _articleRepository.GetDrafts(userId)
                .Select(article => new ArticleDraftsViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    PublishedDate = article.PostedOn,
                    Category = article.Category.Name,
                    CategoryId = article.CategoryId,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image,
                });

            return articles;
        }

        public IEnumerable<ArticleFeedViewModel> PendingArticles()
        {
            return _articleRepository.GetPending()
            .Select(article => new ArticleFeedViewModel
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content[..Math.Min(article.Content.Length, ArticleFeedContentLength)] + (article.Content.Length > ArticleFeedContentLength ? "..." : ""),
                    PublishedDate = article.PostedOn,
                    Author = article.Author.FirstName + " " + article.Author.LastName,
                    AuthorId = article.Author.Id,
                    Category = article.Category.Name,
                    CategoryId = article.CategoryId,
                    Rating = article.Rating,
                    UserIcon = article.Author.Avatar,
                    CategoryIcon = article.Category.Image,
                    CommentCount = article.Comments.Any() ? article.Comments.Count() : 0
                });
        }
    }
}
