using System.Collections.Generic;
using System.Threading.Tasks;
using GeekStream.Core.Entities;

namespace GeekStream.Core.Interfaces
{
    public interface IVoteRepository
    {
        public Task SaveAsync(VoteOnPost vote);
        public Task UpdateAsync(VoteOnPost vote);
        public Task DeleteAsync(VoteOnPost vote);
        public Task SaveAsync(VoteOnReply vote);
        public Task UpdateAsync(VoteOnReply vote);
        public Task DeleteAsync(VoteOnReply vote);
        public bool GetVoteOnPost(string userId, int articleId, VoteType type);
        public IEnumerable<VoteOnPost> GetVotesOnPost(int articleId);
        public bool GetVoteOnReply(string userId, int commentId, VoteType type);
        public IEnumerable<VoteOnReply> GetVotesOnReply(int commentId);
    }
}