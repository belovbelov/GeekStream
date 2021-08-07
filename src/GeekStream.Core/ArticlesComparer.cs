using System.Collections.Generic;
using GeekStream.Core.Entities;

namespace GeekStream.Core
{
    public class ArticlesComparer : IEqualityComparer<Article>
    {
        public bool Equals(Article x, Article y)
        {
            return x.Id == y.Id &&
                   x.Author == y.Author;
        }

        public int GetHashCode(Article obj)
        {
            return obj.Id.GetHashCode() ^
                   obj.Author.GetHashCode();
        }
    }
}