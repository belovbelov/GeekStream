using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Core.Services
{
    public class VoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }


        public async Task CreateOrUpdateVoteOnPost(string userId, int articleId, VoteType type)
        {
            var vote = new VoteOnPost
            {
                ApplicationUserId = userId,
                ArticleId = articleId,
                Type = type
            };

            try
            {
                await _voteRepository.Save(vote);
            }
            catch (DbUpdateException e)
            {
                await _voteRepository.Update(vote);
            }
        }
        public async Task CreateOrUpdateVoteOnReply(string userId, int commentId, VoteType type)
        {
            var vote = new VoteOnReply
            {
                ApplicationUserId = userId,
                CommentId = commentId,
                Type = type
            };

            try
            {
                await _voteRepository.Save(vote);
            }
            catch (DbUpdateException e)
            {
                await _voteRepository.Update(vote);
            }
        }

        public async Task RemoveVoteFromPost(string userId, int articleId, VoteType type)
        {
            var vote = new VoteOnPost
            {
                ApplicationUserId = userId,
                ArticleId = articleId,
                Type = type
            };
            await _voteRepository.Delete(vote);
        }

        public async Task RemoveVoteFromReply(string userId, int commentId, VoteType type)
        {
            var vote = new VoteOnReply
            {
                ApplicationUserId = userId,
                CommentId = commentId,
                Type = type
            };
            await _voteRepository.Delete(vote);
        }
        public int GetRatingForPost(int articleId)
        {
            var votes = _voteRepository.GetVotesOnPost(articleId);
            return votes.Sum(v => (int) v.Type);
        }
        public int GetRatingForReply(int commentId)
        {
            var votes = _voteRepository.GetVotesOnReply(commentId);
            return votes.Sum(v => (int) v.Type);
        }

        public bool CheckIfPostIsVoted(string userId, int articleId, VoteType type)
        {
            var vote = _voteRepository.GetVoteOnPost(userId, articleId, type);
            return vote;
        }
        public bool CheckIfReplyIsVoted(string userId, int commentId, VoteType type)
        {
            var vote = _voteRepository.GetVoteOnReply(userId, commentId,type);
            return vote;
        }
    }
}