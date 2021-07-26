﻿using System.Diagnostics;
using System.Linq;
using GeekStream.Core.Services;
using GeekStream.Core.ViewModels;
using GeekStream.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeekStream.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArticleService _articleService;
        private readonly UserService _userService;

        public HomeController(ArticleService articleService, UserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (_userService.IsSubscribed(_userService.GetCurrentUser()))
                {
                    var allArticles = _articleService.FindBySubscription(id);
                    return View(allArticles);
                }

                return View();
            }

            var articles = _articleService.GetAllArticles();
            return View(articles);
        }

        [HttpPost]
        public IActionResult Subscribe(bool isSubscribed, string id)
        {
            if (ModelState.IsValid)
            {
                _userService.Subscribe(id);
                return RedirectToAction("Index", "Categories");
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Unsubscribe(bool isSubscribed, string id)
        {
            if (ModelState.IsValid)
            {
                _userService.Unsubscribe(id);
                return RedirectToAction("Index", "Categories");
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
