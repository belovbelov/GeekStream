namespace GeekStream.Core.Entities
{
    public class VoteOnReply : Vote
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public int CommentId{ get; set; }

        public Comment Comment{ get; set; }

        public VoteType Type { get; set; }
    }
}