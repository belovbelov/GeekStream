using System.Linq;
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

        public async Task CreateAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public Comment FindCommentById(int id)
        {
            return _context.Comments
                .FirstOrDefault(c => c.Id == id);
        }

        public async Task RemoveAll(int articleId)
        {
            var commentList = _context.Comments.Where(c => c.ArticleId == articleId);
            _context.Comments.RemoveRange(commentList);
            await _context.SaveChangesAsync();
        }
    }
}