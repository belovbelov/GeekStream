using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Infrastructure.Data
{
    public class KeywordRepository
    {
        private readonly AppDbContext _context;

        public KeywordRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task SaveAsync(Keyword keyword)
        {
            _context.Add(keyword);
            await _context.SaveChangesAsync();
        }
    }
}