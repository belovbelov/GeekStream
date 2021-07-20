﻿using System;
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

        public IEnumerable<Article> GetAll(int page, int pageSize, string searchString = null)
        {
            if (searchString == null)
            {
                return _context.Articles
                    .Include(article => article.Category)
                    .Include(article => article.Author)
                    .Where(article => article.PostedOn != null)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            return _context.Articles
                    .Include(article => article.Category)
                    .Include(article => article.Author)
                    .Where(article => article.PostedOn != null)
                    .Where(article => article.Title.Contains(searchString))
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public async Task SaveAsync(Article article)
        {
            _context.Add(article);
            await _context.SaveChangesAsync();
        }

        public Article GetById(int id)
        {
            var article = _context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .SingleOrDefault(x => x.Id == id);
            return article;
        }

        public void Publish(int id)
        {
            var article  = _context.Articles.SingleOrDefault(x => x.Id == id);
            if (article != null)
            {
                article.PostedOn = DateTime.Now;
                _context.SaveChanges();
            }
        }
        public void UnPublish(int id)
        {
            var article  = _context.Articles.SingleOrDefault(x => x.Id == id);
            if (article != null)
            {
                article.PostedOn = DateTime.MinValue;
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

        public IEnumerable<Article> FindByCategoryId(string id = null)
        {
            return _context.Articles
                .Include(article => article.Category)
                .Include(article => article.Author)
                .Where(article => article.PostedOn != null)
                .Where(a => a.CategoryId.ToString() == id)
                .ToList();
        }
    }
}
