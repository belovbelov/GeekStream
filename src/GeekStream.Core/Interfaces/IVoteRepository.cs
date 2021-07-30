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
        public Task Save(VoteOnReply vote);
        public Task Update(VoteOnReply vote);
        public Task Delete(VoteOnReply vote);
        public bool GetVoteOnPost(string userId, int articleId, VoteType type);
        public IEnumerable<VoteOnPost> GetVotesOnPost(int articleId);
        public bool GetVoteOnReply(string userId, int commentId, VoteType type);
        public IEnumerable<VoteOnReply> GetVotesOnReply(int commentId);
    }
}