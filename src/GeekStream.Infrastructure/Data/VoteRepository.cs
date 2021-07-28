using System;
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
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
        }

        public async Task Update(VoteOnPost vote)
        {
            _context.Votes.Update(vote);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(VoteOnPost vote)
        {
            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();
        }

        public VoteOnPost GetVoteById(string userId, int articleId)
        {
            if (userId == null)
            {
                return null;
            }
            return _context.Votes.FirstOrDefault(v => v.ApplicationUserId == userId && v.ArticleId == articleId);
        }
    }
}