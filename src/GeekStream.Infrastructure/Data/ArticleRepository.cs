using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Infrastructure.Data
{
    public class ArticleRepository : IArticleRepository
    {
        public IList<Article> GetArticles()
        {
            //TODO Логика репозитория
            return new List<Article>
            {
                new Article("Test", "First test Article", new List<string>
                {
                    "Tecnhologies", 
                    "Memes"
                }),
            };
        }
    }
}
