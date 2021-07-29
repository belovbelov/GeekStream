using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
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


        public async Task CreateOrUpdateVote(string userId, int articleId, VoteType type)
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

        public async Task RemoveVote(string userId, int articleId, VoteType type)
        {
            var vote = new VoteOnPost
            {
                ApplicationUserId = userId,
                ArticleId = articleId,
                Type = type
            };
            await _voteRepository.Delete(vote);
        }

        public int GetRatingForPost(int articleId)
        {
            var votes = _voteRepository.GetVotesOnPost(articleId);
            return votes.Sum(v => (int) v.Type);
        }

        public bool CheckIfPostIsVoted(string userId, int articleId)
        {
            var vote = _voteRepository.GetVoteOnPost(userId, articleId);
            return vote;
        }
    }
}