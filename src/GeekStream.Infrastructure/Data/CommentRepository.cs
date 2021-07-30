using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Infrastructure.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }
    }
}