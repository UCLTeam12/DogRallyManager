using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DogRallyManager.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IDataService _dataService;
        private readonly UserManager<RallyUser> _userManager;
        private readonly IMapper _mapper;

        public ChatController(IDataService dataService,
            IMapper mapper,
            UserManager<RallyUser> userManager)
        {
            _dataService = dataService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            
            var chatRoomsEntities = await _dataService.GetAssociatedChatRoomsAsync(user.Id);

            if (chatRoomsEntities == null)
            {

            }

            var chatRoomsVM = _mapper.Map<IEnumerable<ChatRoomVM>>(chatRoomsEntities);


            return View(chatRoomsVM);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string messageBody)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var chatRooms = await _dataService.GetAssociatedChatRoomsAsync(user.Id);
                var entityChatRoom = chatRooms.FirstOrDefault();

                if (entityChatRoom == null)
                {
                    entityChatRoom = new ChatRoom();
                    entityChatRoom.ParticipatingUsers.Add(user);
                    await _dataService.AddChatRoomAsync(entityChatRoom);
                }

                var newMessage = new ChatMessageVM
                {
                    MessageBody = messageBody,
                    Sender = user,
                    TimeStamp = DateTime.UtcNow,
                    ChatRoomId = entityChatRoom.Id
                };

                var entityMessage = _mapper.Map<Message>(newMessage);
                entityChatRoom.ChatMessages.Add(entityMessage);

                await _dataService.AddMessageAsync(entityMessage);

                return Json(new { success = true, message = "The message was succesfully persisted in database" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error sending message: {ex.Message}" });
            }

        }
    }
}