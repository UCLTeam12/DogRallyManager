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

            var verifySearchedUser = await _dataService.GetUserAsync(recipientUserName);

            if (verifySearchedUser == null)
            {
                return NotFound("A user with that name does not exist");
            }

            var user = await _userManager.GetUserAsync(User);

            var chatRoomsEntities = await _dataService.GetAssociatedChatRoomsWithMessagesAsync(user.Id);

            if (chatRoomsEntities == null)
            {
                chatRoomsEntities = new List<ChatRoom>();
            }

            // This could (should it?) be done in dataservice
            var chatRoomsVM = _mapper.Map<List<ChatRoomVM>>(chatRoomsEntities);

            ChatRoomVM chatRoomVMToBeAdded = new ChatRoomVM { NumberOfAssociatedUsers = 2, Id = 0 };

            UserViewModel recipientUserVM = new UserViewModel { UserName = recipientUserName };

            UserViewModel initiatingUserVM = new UserViewModel { UserName = User.Identity.Name };

            chatRoomVMToBeAdded.ParticipatingUsers.Add(recipientUserVM);
            chatRoomVMToBeAdded.ParticipatingUsers.Add(initiatingUserVM);

            chatRoomVMToBeAdded.RoomName = "Your chat with "+recipientUserName;

            chatRoomsVM.Add(chatRoomVMToBeAdded);

            // TO-DO:
            // Im not entirely sure if we should just return the list to the view here --- that would
            // require modification in the chat view. (It is expecting to handle an ienumerable collection.
            // I do it so far, just to make testing easier.
            IEnumerable<ChatRoomVM> chatRoomsToBeReturned = chatRoomsVM;

            return View("Index", chatRoomsToBeReturned);

        }

        [HttpPost]
        public async Task<IActionResult> SendMessageOnNewlyCreatedRoom([FromBody] dynamic data)
        {
            try { 
                var user = await _userManager.GetUserAsync(User);

                var newMessageVM = new ChatMessageVM
                {
                    MessageBody = data.messageBody,
                    Sender = user,
                    TimeStamp = DateTime.UtcNow
                };

                ChatRoomVM chatRoomVM = new();

                List<string> recipientUserNames = data.recipientUserNames.ToObject<List<string>>();

                foreach (string ParticipatingUserName in recipientUserNames)
                {
                    UserViewModel ParticipatingUserVM = new UserViewModel { UserName = ParticipatingUserName };
                    chatRoomVM.ParticipatingUsers.Add(ParticipatingUserVM);
                }

                chatRoomVM.ChatMessages.Add(newMessageVM);

                var chatRoomEntity = _mapper.Map<ChatRoom>(chatRoomVM);

                await _dataService.AddChatRoomAsync(chatRoomEntity);

                return Json(new { success = true, message = "Message sent successfully to the new room." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error sending message: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string messageBody, int chatRoomId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                // If the ChatRoomId is 0, this means that the ChatRoom only exists on an object-context
                // as a ChatRoomVM. 
                if (chatRoomId == 0)
                {
                    var newMessageVM = new ChatMessageVM
                    {
                        MessageBody = messageBody,
                        Sender = user,
                        TimeStamp = DateTime.UtcNow
                    };
                    var newEntityMessage = _mapper.Map<Message>(newMessageVM);

                    var entityChatRoom = new ChatRoom();
                    entityChatRoom.ParticipatingUsers.Add(user);
                    entityChatRoom.ChatMessages.Add(newEntityMessage);
                    await _dataService.AddChatRoomAsync(entityChatRoom);
                }

                //var entityMessage = _mapper.Map<Message>(newMessage);
                //entityChatRoom.ChatMessages.Add(entityMessage);

                //await _dataService.AddMessageAsync(entityMessage);

                return Json(new { success = true, message = "The message was succesfully persisted in database" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error sending message: {ex.Message}" });
            }

        }

        
    }
}