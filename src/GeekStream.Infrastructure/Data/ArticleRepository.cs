using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<ArticleViewModel> GetArticles(int page, int pageSize, string searchString = null)
        {
            if (searchString == null)
            {
                return _context.Articles
                    .Where(article => article.PostedOn != null)
                    .Select(
                        a => new ArticleViewModel
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Content = a.Content,
                            PublishedDate = a.CreatedOn,//TODO заменить
                            Category = _context.Categories
                                .Where(c => c.Id == a.CategoryId)
                                .Select(c => c.Name)
                                .SingleOrDefault(),
                            // Author = article.AuthorId,
                            Rating = a.Rating
                        })
                    .ToList();
            }

            return _context.Articles
                .Where(article => article.PostedOn != null)
                .Where(article => article.Title.Contains(searchString))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(
                    a => new ArticleViewModel
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Content = a.Content,
                        PublishedDate = a.CreatedOn,
                        Category = _context.Categories
                            .Where(c => c.Id == a.CategoryId)
                            .Select(c => c.Name)
                            .SingleOrDefault(),
                        // Author = article.AuthorId,
                        Rating = a.Rating
                    })
                .ToList();

        }

        public async Task SaveArticleAsync(Article article)
        {
            var category = _context.Categories
                .First(c => c.Id == article.CategoryId);
            article.Category = category;
            _context.Add(article);
            await _context.SaveChangesAsync();
        }

        public Article GetArticle(int id)
        {
            return _context.Articles.SingleOrDefault(x => x.Id == id);
        }

        public void PublishArticle(int id)
        {
            var article  = _context.Articles.SingleOrDefault(x => x.Id == id);
            if (article != null)
            {
                article.PostedOn = DateTime.Now;
                _context.SaveChanges();
            }
        }
        public void UnPublishArticle(int id)
        {
            var article  = _context.Articles.SingleOrDefault(x => x.Id == id);
            if (article != null)
            {
                article.PostedOn = DateTime.MinValue;
                _context.SaveChanges();
            }
        }

        public void DeleteArticle(int id)
        {
            var article = GetArticle(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ArticleViewModel> FindByCategoryId(string id = null)
        {
            return _context.Articles
                .Where(article => article.PostedOn != null)
                .Where(a => a.CategoryId.ToString() == id)
                .Select(
                    a => new ArticleViewModel
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Content = a.Content,
                        PublishedDate = a.CreatedOn,
                        Category = _context.Categories
                            .Where(c => c.Id == a.CategoryId)
                            .Select(c => c.Name)
                            .SingleOrDefault(),
                        // Author = article.AuthorId,
                        Rating = a.Rating
                    })
                .ToList();
        }
    }
}
