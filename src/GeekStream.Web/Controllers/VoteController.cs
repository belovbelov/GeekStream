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

        public VoteController(VoteService voteService, UserService userService, ArticleService articleService)
        {
            _voteService = voteService;
            _userService = userService;
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessVotes(int articleId, VoteType type)
        {
            if (User.Identity == null)
            {
                return BadRequest();
            }

            var user = _userService.GetCurrentUser();
            var wasOn = _voteService.CheckIfPostIsVoted(user.Id, articleId);

            if (!wasOn)
            {
                await _voteService.CreateOrUpdateVote(user.Id, articleId, type);
            }
            else
            {
                await _voteService.RemoveVote(user.Id, articleId, type);
            }

            var votes = _voteService.GetRatingForPost(articleId);
            var articleAuthorId = _articleService.GetArticleById(articleId).AuthorId;

            _articleService.UpdateArticleRating(articleId, votes);

            var userRating = _userService.GetUserRating(articleAuthorId);
            _userService.UpdateUserRating(articleAuthorId, userRating);

            return NoContent();
        }
    }
}
