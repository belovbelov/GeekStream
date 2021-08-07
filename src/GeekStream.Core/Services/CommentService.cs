using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Interfaces;

namespace GeekStream.Core.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserService _userService;

        public CommentService(ICommentRepository commentRepository, UserService userService)
        {
            _commentRepository = commentRepository;
            _userService = userService;
        }

        public async Task LeaveComment(int articleId, string text)
        {
            var user = _userService.GetCurrentUser();
            var comment = new Comment
            {
                UserName = user.FirstName + " " + user.LastName,
                Content = text,
                ArticleId = articleId,
                ApplicationUserId = user.Id
            };
            await _commentRepository.CreateAsync(comment);
        }

        public async Task UpdateCommentRating(int commentId, int votes)
        {
            var comment = _commentRepository.FindCommentById(commentId);
            comment.Rating = votes;
            await _commentRepository.UpdateAsync(comment);
        }

        public Comment FindCommentById(int commentId)
        {
            return _commentRepository.FindCommentById(commentId);
        }

        public async Task RemoveAll(int articleId)
        {
            await _commentRepository.RemoveAll(articleId);
        }
    }
}