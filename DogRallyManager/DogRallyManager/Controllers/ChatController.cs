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
            // Gets the Id of the User.Identity object via userManager.
            var user = await _userManager.GetUserAsync(User);
           
            var participatingChatRoomsEntities = await _dataService.GetAssociatedChatRoomsAsync(user.Id);

            //_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
            var participatingChatRoomsVM = _mapper.Map<IEnumerable<ChatRoomVM>>(participatingChatRoomsEntities);
            return View("Chat", participatingChatRoomsVM);
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