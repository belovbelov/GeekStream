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
                .Include(article => article.Comments)
                    .Include(article => article.Category)
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
                    .Where(article => article.Type == ArticleType.Posted)
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
            _context.Entry(article).State = EntityState.Modified;
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
                    .Include(article => article.Comments)
                    .Include(article => article.Category)
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
                    .Where(article => article.Type == ArticleType.Posted)
                .Where(a => a.CategoryId == id);
        }

        public IEnumerable<Article> FindByAuthorId(string id)
        {
            return _context.Articles
                    .Include(article => article.Comments)
                    .Include(article => article.Category)
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
                    .Where(article => article.Type == ArticleType.Posted)
                .Where(a => a.Author.Id == id);
        }

        public async Task<IEnumerable<Article>> FindByAuthorSubscription(string currentUserId)
        {
            var sub = _context.Subscription
                .Where(s => currentUserId == s.ApplicationUser.Id);

            var articles = await _context.Articles
                .Where(article => article.PostedOn != null)
                .Include(article => article.Comments)
                .Include(article => article.Category)
                .ThenInclude(c => c.Image)
                .Include(article => article.Author)
                .ThenInclude(a => a.Avatar)
                .ToArrayAsync();

                return articles.Join(
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
                        Comments = a.Comments
                    }
                );


        }
        public async Task<IEnumerable<Article>> FindByCategorySubscription(string currentUserId, string? subscriptionId)
        {

            var sub = _context.Subscription
                .Where(s => currentUserId == s.ApplicationUser.Id);

            var articles = await _context.Articles
                .Where(article => article.PostedOn != null)
                .Include(article => article.Comments)
                .Include(article => article.Category)
                .ThenInclude(c => c.Image)
                .Include(article => article.Author)
                .ThenInclude(a => a.Avatar)
                .ToArrayAsync();

            if (subscriptionId != null)
            {
                articles = articles
                    .Where(article => article.CategoryId.ToString() == subscriptionId || article.Author.Id == subscriptionId)
                    .ToArray();
            }
            return articles.Join(
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
                        Comments = a.Comments
                }
            );
        }
        
        public IEnumerable<Article> FindByKeywords(List<string> words)
        {
            return _context.Articles
                    .Include(article => article.Category)
                    .ThenInclude(c => c.Image)
                    .Include(article => article.Author)
                    .ThenInclude(a => a.Avatar)
                .Include(article => article.Keywords)
                .Include(article => article.Comments)
                .Where(article => article.Keywords.Any(k => words.Contains(k.Word)));
        }

        public IEnumerable<Article> GetPending()
        {
            return _context.Articles
                .Include(article => article.Comments)
                .Include(article => article.Category)
                .ThenInclude(c => c.Image)
                .Include(article => article.Author)
                .ThenInclude(a => a.Avatar)
                .Include(article => article.Keywords)
                .Where(a => a.Type == ArticleType.Ready);
        }


        public IEnumerable<Article> GetDrafts(string userId)
        {
            return _context.Articles
                .Include(article => article.Category)
                .ThenInclude(c => c.Image)
                .Include(article => article.Author)
                .ThenInclude(a => a.Avatar)
                .Include(article => article.Keywords)
                .Where(a => a.Type == ArticleType.Draft && a.Author.Id == userId ||
                            a.Author.Id == userId && a.Type == ArticleType.Approved||
                            a.Author.Id == userId && a.Type == ArticleType.Hidden)
                .OrderByDescending(article => article.Type);
        }
    }
}
