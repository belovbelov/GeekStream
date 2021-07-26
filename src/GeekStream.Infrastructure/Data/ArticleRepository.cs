using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using GeekStream.Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Article> GetAll(int page, int pageSize)
        {
            return _context.Articles
                    .Include(article => article.Category)
                    .Include(article => article.Author)
                    .Where(article => article.PostedOn != null)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public async Task SaveAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public Article GetById(int id)
        {
            var article = _context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Include(article => article.Images)
                .SingleOrDefault(x => x.Id == id);
            return article;
        }

        public void Publish(int id)
        {
            var article  = _context.Articles.SingleOrDefault(x => x.Id == id);
            if (article != null)
            {
                article.PostedOn = DateTime.UtcNow;
                _context.SaveChanges();
            }
        }
        public void UnPublish(int id)
        {
            var article  = _context.Articles.SingleOrDefault(x => x.Id == id);
            if (article != null)
            {
                article.PostedOn = null;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var article = GetById(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Article> FindByCategoryId(int id)
        {
            return _context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Where(article => article.PostedOn != null)
                .Where(a => a.CategoryId == id);
        }

        public IEnumerable<Article> FindByAuthorId(string id)
        {
            return _context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Where(article => article.PostedOn != null)
                .Where(a => a.Author.Id == id);
        }

        public IEnumerable<Article> FindBySubscription(string currentUserId,string? subscriptionId)
        {
            var sub = _context.Subscription
                    .Where(s => currentUserId == s.ApplicationUser.Id);

            var allArticles = _context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Where(article => article.PostedOn != null);

            if (subscriptionId != null)
            {
                allArticles = allArticles
                    .Where(article => article.CategoryId.ToString() == subscriptionId || article.Author.Id == subscriptionId);
            }

            var articles = allArticles.Join(
                sub,
                a => a.CategoryId.ToString(),
                s => s.PublishSource,
                (a, s) => new Article
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    CreatedOn = a.CreatedOn,
                    PostedOn = a.PostedOn,
                    Author = a.Author,
                    Category = a.Category,
                    CategoryId = a.CategoryId,
                    Rating = a.Rating,
                }
            );
            return allArticles.Join(
                    sub,
                    a => a.Author.Id,
                    s => s.PublishSource,
                    (a, s) => new Article
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Content = a.Content,
                        CreatedOn = a.CreatedOn,
                        PostedOn = a.PostedOn,
                        Author = a.Author,
                        Category = a.Category,
                        CategoryId = a.CategoryId,
                        Rating = a.Rating,
                    }
                )
                .Concat(articles);
        }


        public IEnumerable<Article> FindByKeywords(List<string> words)
        {
            return _context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Include(article => article.Keywords)
                .Where(article => article.Keywords.Any(k => words.Contains(k.Word)));
        }
    }
}
