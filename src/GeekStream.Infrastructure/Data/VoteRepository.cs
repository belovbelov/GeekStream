using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Infrastructure.Data
{
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;

        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(VoteOnPost vote)
        {
            _context.Add(vote);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync(VoteOnReply vote)
        {
            _context.Add(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VoteOnPost vote)
        {
            _context.Update(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(VoteOnReply vote)
        {
            _context.Update(vote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(VoteOnPost vote)
        {
            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(VoteOnReply vote)
        {
            _context.VotesOnReplies.Remove(vote);
            await _context.SaveChangesAsync();
        }

        public bool GetVoteOnReply(string userId, int commentId, VoteType type)
        {
            if (userId == null)
            {
            }

            return _context.VotesOnReplies
                .Any(v => v.ApplicationUserId == userId && v.CommentId == commentId && type == v.Type);
        }

        public bool GetVoteOnPost(string userId, int articleId, VoteType type)
        {
            if (userId == null)
            {
            }

            return _context.Votes
                .Any(v => v.ApplicationUserId == userId && v.ArticleId == articleId && type == v.Type);
        }

        public IEnumerable<VoteOnPost> GetVotesOnPost(int articleId)
        {
            return _context.Votes
                .Where(v => v.ArticleId == articleId);
        }

        public IEnumerable<VoteOnReply> GetVotesOnReply(int commentId)
        {
            return _context.VotesOnReplies
                .Where(v => v.CommentId == commentId);
        }
    }
}