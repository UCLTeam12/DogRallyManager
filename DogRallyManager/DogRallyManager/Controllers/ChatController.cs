using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DogRallyManager.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IMessageCreationService _messageCreationService;
        private readonly UserManager<RallyUser> _userManager;
        private readonly IMapper _mapper;

        public ChatController(IDataService dataService, 
            IMessageCreationService messageCreationService,
            IMapper mapper,
            UserManager<RallyUser> userManager)
        {
            _dataService = dataService;
            _messageCreationService = messageCreationService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var chatRoomsEntities = await _dataService.GetAssociatedChatRoomsAsync(user.Id);

            // Map entities to view models
            var chatRoomsVM = _mapper.Map<IEnumerable<ChatRoomVM>>(chatRoomsEntities);

            // Combine messages from all chat rooms
            var allMessagesEntities = chatRoomsEntities.SelectMany(room => room.Messages);
            var allMessagesVM = _mapper.Map<IEnumerable<ChatMessageVM>>(allMessagesEntities);

            var viewModel = new ChatViewModel
            {
                ChatRooms = chatRoomsVM,
                ChatMessages = allMessagesVM
            };

            return View("Chat", viewModel);
        }
        // Example action method to handle message sending
        [HttpPost]
        public async Task<IActionResult> SendMessage(ChatMessageVM chatMessageVM, 
            ChatRoomVM chatRoomVM)
        {
            // Create the message entity using MessageCreationService
            await _messageCreationService.CreateMessageAsync(chatMessageVM, chatRoomVM);

            // Redirect or return appropriate response
            return RedirectToAction("Index");
        }

    }
}