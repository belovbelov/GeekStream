using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IVoteRepository
    {
        public Task Save(VoteOnPost vote);
        public Task Update(VoteOnPost vote);
        public Task Delete(VoteOnPost vote);
        public bool GetVoteOnPost(string userId, int articleId);
        public IEnumerable<VoteOnPost> GetVotesOnPost(int articleId);
    }
}