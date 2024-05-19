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
            
            var chatRoomsEntities = await _dataService.GetAssociatedChatRoomsWithMessagesAsync(user.Id);

            if (chatRoomsEntities == null)
            {
                // TO-DO:
                // If ChatRoomsEntities is null, we will get an error on the load of messages
                // saying that it is trying to read from something without an object reference.
                // Probably alot of ways to fix this. We could put an if loop in the razor page? 
                // Maybe the fix actually lies somewhere else.... let me see... 
            }

            // This could be done in dataservice
            var chatRoomsVM = _mapper.Map<IEnumerable<ChatRoomVM>>(chatRoomsEntities);

            return View(chatRoomsVM);
        }

        [HttpGet]
        public async Task<IActionResult> StartChat(string recipientUserName)
        {
            // TO-DO:
            // use AJAX to request an update of the chatview incase signalR is down.
            // Until then, we will add it to the list of chatrooms and return view of chat, since we dont have
            // components rolling side by side
            // For now we basically just copy paste the body of the index method above and add an extra
            // ChatVM to the list and return the view of the chat.
            var user = await _userManager.GetUserAsync(User);

            var chatRoomsEntities = await _dataService.GetAssociatedChatRoomsWithMessagesAsync(user.Id);

            if (chatRoomsEntities == null)
            {
                // TO-DO:
                // If ChatRoomsEntities is null, we will get an error on the load of messages
                // saying that it is trying to read from something without an object reference.
                // Probably alot of ways to fix this. We could put an if loop in the razor page? 
                // Maybe the fix actually lies somewhere else.... let me see... 
                // For now I just do this:
                chatRoomsEntities = new List<ChatRoom>();
            }

            // This could (should it?) be done in dataservice
            var chatRoomsVM = _mapper.Map<List<ChatRoomVM>>(chatRoomsEntities);

            ChatRoomVM chatRoomVMToBeAdded = new ChatRoomVM { NumberOfAssociatedUsers = 2 };

            UserViewModel recipientUserVM = new UserViewModel { UserName = recipientUserName };

            UserViewModel initiatingUserVM = new UserViewModel { UserName = User.Identity.Name };

            chatRoomVMToBeAdded.ParticipatingUsers.Add(recipientUserVM);
            chatRoomVMToBeAdded.ParticipatingUsers.Add(initiatingUserVM);

            chatRoomsVM.Add(chatRoomVMToBeAdded);

            // TO-DO:
            // Im not entirely sure if we should just return the list to the view here --- that would
            // require modification in the chat view. (It is expecting to handle an ienumerable collection.
            // I do it so far, just to make testing easier.
            IEnumerable<ChatRoomVM> chatRoomsToBeReturned = chatRoomsVM;

            return View("Index", chatRoomsToBeReturned);

        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string messageBody)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var chatRooms = await _dataService.GetAssociatedChatRoomsWithMessagesAsync(user.Id);
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