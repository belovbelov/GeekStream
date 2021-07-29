using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> ProcessVotes(int id, VoteType type)
        {
            if (User.Identity == null)
            {
                return BadRequest();
            }

            var userId = _userService.GetCurrentUser().Id;
            var wasOn = _voteService.CheckIfPostIsVoted(userId, id);
            if (!wasOn)
            {
                await _voteService.CreateOrUpdateVote(userId, id, type);
            }
            else
            {
                await _voteService.RemoveVote(userId, id, type);
            }

            var votes = _voteService.GetRatingForPost(id);
            _articleService.UpdateArticle(id, votes);

            return NoContent();
        }
    }
}
