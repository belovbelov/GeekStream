using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface ICommentRepository
    {
        public Task CreateAsync(Comment comment);
        public Task UpdateAsync(Comment comment);
        public Comment FindCommentById(int id);
        public Task RemoveAll(int articleId);
    }
}