using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.AccountVMs;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace DogRallyManager.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IChatService _chatService;
        private readonly UserManager<RallyUser> _userManager;
        private readonly IMapper _mapper;

        public ChatController(IDataService dataService,
            IChatService chatService,
            IMapper mapper,
            UserManager<RallyUser> userManager)
        {
            _dataService = dataService;
            _chatService = chatService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            
            var chatRoomsVM = await _chatService.GetUserAssociatedChatRoomsVM(user.Id);
            return View(chatRoomsVM);
            
        }

        [HttpGet]
        public async Task<IActionResult> StartChat(string recipientUserName)
        {
            var user = await _userManager.GetUserAsync(User);

            bool userExists = await _chatService.DoesUserExistAsync(recipientUserName);

            if (!userExists)
            {
                return NotFound("A user with that name does not exist");
            }

            await _chatService.InitiateChat(user.UserName, recipientUserName);

            return View("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> SendMessage(string messageBody, int chatRoomId)
        {
            try
            {
                var userSender = await _userManager.GetUserAsync(User);

                await _chatService.SendMessageAsync(messageBody, userSender, chatRoomId);

                return Json(new { success = true, message = "The message was succesfully persisted in database" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error sending message: {ex.Message}" });
            }
        }
    }
}