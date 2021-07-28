using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IVoteRepository
    {

        public Task Save(VoteOnPost vote);
        public Task Update(VoteOnPost vote);
        public Task Delete(VoteOnPost vote);
        public VoteOnPost GetVoteById(string userId, int articleId);
    }
}