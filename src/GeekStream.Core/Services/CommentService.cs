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
            await _commentRepository.Create(comment);
        }
    }
}