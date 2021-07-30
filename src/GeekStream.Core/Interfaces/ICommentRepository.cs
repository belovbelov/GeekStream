using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface ICommentRepository
    {
        public Task Create(Comment comment);
    }
}