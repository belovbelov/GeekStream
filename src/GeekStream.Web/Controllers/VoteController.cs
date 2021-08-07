using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Services;

namespace GeekStream.Web.Controllers
{
    public class VoteController : Controller
    {
        private readonly VoteService _voteService;
        private readonly UserService _userService;
        private readonly ArticleService _articleService;
        private readonly CommentService _commentService;

        public VoteController(VoteService voteService, UserService userService, ArticleService articleService, CommentService commentService)
        {
            _voteService = voteService;
            _userService = userService;
            _articleService = articleService;
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessVotes(int articleId, VoteType type)
        {
            if (User.Identity == null)
            {
                return BadRequest();
            }

            var user = _userService.GetCurrentUser();
            var wasOn = _voteService.CheckIfPostIsVoted(user.Id, articleId, type);

            if (!wasOn)
            {
                await _voteService.CreateOrUpdateVoteOnPost(user.Id, articleId, type);
            }
            else
            {
                await _voteService.RemoveVoteFromPost(user.Id, articleId, type);
            }

            var votes = _voteService.GetRatingForPost(articleId);
            var articleAuthorId = _articleService.GetArticleById(articleId).Author.Id;

            await _articleService.UpdateArticleRatingAsync(articleId, votes);

            var userRating = _userService.GetUserRating(articleAuthorId);
            _userService.UpdateUserRating(articleAuthorId, userRating);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessVotesOnReply(int commentId, VoteType type)
        { 
            if (User.Identity == null)
            {
                return BadRequest();
            }

            var user = _userService.GetCurrentUser();
            var wasOn = _voteService.CheckIfReplyIsVoted(user.Id, commentId, type);

            if (!wasOn)
            {
                await _voteService.CreateOrUpdateVoteOnReply(user.Id, commentId, type);
            }
            else
            {
                await _voteService.RemoveVoteFromReply(user.Id, commentId, type);
            }

            var votes = _voteService.GetRatingForReply(commentId);
            var comment = _commentService.FindCommentById(commentId);
            await _commentService.UpdateCommentRating(commentId, votes);

            var userRating = _userService.GetUserRating(comment.ApplicationUserId);
            _userService.UpdateUserRating(comment.ApplicationUserId, userRating);

            return NoContent();
        }
    }
}
