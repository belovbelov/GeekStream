using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
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
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
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

        public async Task UpdateAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Article article)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public Article GetById(int id)
        {
            var article = _context.Articles
                    .Include(article => article.Category)
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
                .Include(article => article.Keywords)
                .Include(article => article.Images)
                .Include(article => article.Comments)
                .SingleOrDefault(x => x.Id == id);
            return article;
        }

        public IEnumerable<Article> FindByCategoryId(int id)
        {
            return _context.Articles
                    .Include(article => article.Category)
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
                .Where(article => article.PostedOn != null)
                .Where(a => a.CategoryId == id);
        }

        public IEnumerable<Article> FindByAuthorId(string id)
        {
            return _context.Articles
                    .Include(article => article.Category)
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
                .Where(article => article.PostedOn != null)
                .Where(a => a.Author.Id == id);
        }

        public async Task<IEnumerable<Article>> FindBySubscription(string currentUserId,string? subscriptionId)
        {
            var sub = _context.Subscription
                .Where(s => currentUserId == s.ApplicationUser.Id);

            var articles = await _context.Articles
                .Where(article => article.PostedOn != null)
                .Include(article => article.Category)
                .ThenInclude(c => c.Image)
                .Include(article => article.Author)
                .ThenInclude(a => a.Avatar)
                .ToArrayAsync();

            if (subscriptionId != null)
            {
                var allArticles = articles
                    .Where(article => article.CategoryId.ToString() == subscriptionId || article.Author.Id == subscriptionId);
            }

            var category= articles.Join(
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
            var users = articles.Join(
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
                );
            var subscriptions = category.Union(users);

            return subscriptions;
        }

        public IEnumerable<Article> FindByKeywords(List<string> words)
        {
            return _context.Articles
                    .Include(article => article.Category)
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
                .Include(article => article.Keywords)
                .Where(article => article.Keywords.Any(k => words.Contains(k.Word)));
        }
    }
}
