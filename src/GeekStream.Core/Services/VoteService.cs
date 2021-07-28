using System;
using System.Data.Common;
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


        public void CreateOrUpdateVote(string userId, int articleId, VoteType type)
        {
            var vote = new VoteOnPost
            {
                ApplicationUserId = userId,
                ArticleId = articleId,
                Type = type
            };

            try
            {
                _voteRepository.Save(vote);
            }
            catch (DbUpdateException e)
            {
                _voteRepository.Update(vote);
            }
        }

        public void RemoveVote(string userId, int articleId, VoteType type)
        {
            var vote = new VoteOnPost
            {
                ApplicationUserId = userId,
                ArticleId = articleId,
                Type = type
            };
            _voteRepository.Delete(vote);
        }

        public bool CheckIfPostIsVoted(string userId, int articleId)
        {
            var vote = _voteRepository.GetVoteById(userId, articleId);
            if (vote == null)
            {
                return false;
            }

            return true;
        }
    }
}