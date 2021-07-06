using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IArticleRepository
    {
        public IList<Article> GetArticles();
    }
}
