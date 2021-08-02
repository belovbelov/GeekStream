using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekStream.Core.Services;
using GeekStream.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GeekStream.Web.Controllers
{
    public class ChatsController : Controller
    {
        private readonly ChatService _chatService;

        public ChatsController(ChatService chatService)
        {
            _chatService = chatService;
        }
        public IActionResult Index()
        {
            var chats = _chatService.GetChats();
            return View(chats);
        }

        public IActionResult Private()
        {
            var chats = _chatService.GetPrivateChats();

            return View(chats);
        }

        public async Task<IActionResult> CreatePrivateRoom(string userId)
        {
            var id = await _chatService.CreatePrivateRoom(userId);

            return RedirectToAction("Chat", new { id });
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            return View(_chatService.GetChat(id));
        }

        public async Task<IActionResult> SendMessage(
            int roomId,
            string message,
            [FromServices] IHubContext<ChatHub> chat)
        {
            var Message = await _chatService.CreateMessage(roomId, message;

            await chat.Clients.Group(roomId.ToString())
                .SendAsync("RecieveMessage", new
                {
                    Text = Message.Text,
                    Name = Message.Name,
                    Timestamp = Message.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
                });

            return Ok();
        }
    }
}
