namespace GeekStream.Core.Entities
{
    public class VoteOnPost
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public VoteType Type { get; set; }
    }
}