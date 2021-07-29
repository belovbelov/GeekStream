using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Infrastructure.Data
{
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;

        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Save(VoteOnPost vote)
        {
            _context.Add(vote);
            await _context.SaveChangesAsync();
        }

        public async Task Update(VoteOnPost vote)
        {
            _context.Update(vote);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(VoteOnPost vote)
        {
            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
        }

        public bool GetVoteOnPost(string userId, int articleId)
        {
            if (userId == null)
            {
            }

            return _context.Votes
                .Any(v => v.ApplicationUserId == userId && v.ArticleId == articleId);
        }

        public IEnumerable<VoteOnPost> GetVotesOnPost(int articleId)
        {
            return _context.Votes
                .Where(v => v.ArticleId == articleId);
        }
    }
}