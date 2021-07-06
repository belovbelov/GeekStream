using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Core.Services
{
    class ArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IList<Article> GetArticles()
        {
            return _articleRepository.GetArticles();
        }
        //TODO Логика СервисаСтатьи
    }
}
