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

        [HttpGet]
        public async Task<IActionResult> JoinRoom(int id)
        {
            await _chatService.JoinRoom(id);

            return RedirectToAction("Chat", "Chats", new { id = id });}

        [HttpGet]
        public IActionResult Index()
        {
            var chats = _chatService.GetChats();
            return View(chats);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Private()
        {
            var chats = _chatService.GetPrivateChats();

            return View(chats);
        }

        public async Task<IActionResult> CreatePrivateRoom(string id)
        {
            var chatId = await _chatService.CreatePrivateRoom(id);

            return RedirectToAction(nameof(Chat), new{ id = chatId} );
        }

        public async Task<IActionResult> SendMessage(
            int roomId,
            string message,
            [FromServices] IHubContext<ChatHub> chat)
        {
            var Message = await _chatService.CreateMessage(roomId, message);

            await chat.Clients.Group(roomId.ToString())
                .SendAsync("RecieveMessage", new
                {
                    Text = Message.Text,
                    Name = Message.Name,
                    Timestamp = Message.Timestamp
                });

            return RedirectToAction("Chat", new { id = roomId});
        }
        [HttpGet("[controller]/{id}")]
        public IActionResult Chat(int id)
        {
            return View(_chatService.GetChat(id));
        }
    }
}
