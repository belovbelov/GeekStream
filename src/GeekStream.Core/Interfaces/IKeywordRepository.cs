using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IKeywordRepository
    {
        public Task SaveAsync(Keyword keyword);
    }
}