using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class KeywordRepository : IKeywordRepository
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