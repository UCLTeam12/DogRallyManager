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

            }

            // This could be done in dataservice
            var chatRoomsVM = _mapper.Map<IEnumerable<ChatRoomVM>>(chatRoomsEntities);

            return View(chatRoomsVM);
        }

        [HttpGet]
        public async Task<IActionResult> StartChat(string recipientUserName)
        {

            var verifySearchedUser = await _dataService.GetUserAsync(recipientUserName);
            var user = await _userManager.GetUserAsync(User);

            if (verifySearchedUser == null)
            {
                return NotFound("A user with that name does not exist");
            }

            bool RoomAlreadyExist = await _dataService.RoomAlreadyExists(user.UserName, verifySearchedUser.UserName);

            if(RoomAlreadyExist)
            {
                return RedirectToAction("Index");
            }

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
        public async Task<IActionResult> SendMessageOnNewlyCreatedRoom([FromBody] SendMessageRequest request)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var newMessageVM = new ChatMessageVM
                {
                    MessageBody = request.MessageBody,
                    Sender = user,
                    TimeStamp = DateTime.UtcNow
                };

                ChatRoomVM chatRoomVM = new();
                chatRoomVM.ChatMessages.Add(newMessageVM);
                var chatRoomEntity = _mapper.Map<ChatRoom>(chatRoomVM);

                foreach (string ParticipatingUserName in request.RecipientUserNames)
                {
                    var recipientUser = await _userManager.FindByNameAsync(ParticipatingUserName);
                    if (recipientUser == null)
                    {
                        return Json(new { success = false, message = $"Error sending message: A user with username: {ParticipatingUserName} does not exist." });
                    }
                    chatRoomEntity.ParticipatingUsers.Add(recipientUser);
                }

                  chatRoomEntity.RoomName = $"Chatroom: {request.RecipientUserNames.ElementAt(0)} and {request.RecipientUserNames.ElementAt(1)}";

                await _dataService.AddChatRoomAsync(chatRoomEntity);

                return RedirectToAction("Index");
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
                var userSender = await _userManager.GetUserAsync(User);

                await _dataService.AddMessageAsync(messageBody, chatRoomId, userSender);

                return Json(new { success = true, message = "The message was succesfully persisted in database" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error sending message: {ex.Message}" });
            }

        }

        
    }
}