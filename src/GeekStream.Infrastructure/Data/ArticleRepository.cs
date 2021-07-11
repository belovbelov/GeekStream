﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<Article> GetArticles(int page, int pageSize, string searchString = null)
        {
            if (searchString == null)
            {
                return _context.Articles
                    .Where(article => article.PostedOn != null)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            return _context.Articles
                .Where(article => article.PostedOn != null)
                .Where(article => article.Title.Contains(searchString))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public void SaveArticle(Article article)
        {
            _context.Add(article);
            _context.SaveChanges();
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
    }
}
