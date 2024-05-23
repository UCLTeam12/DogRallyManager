using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.AccountVMs;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Runtime.Intrinsics.X86;

namespace DogRallyManager.Services
{
    public class DemoDataService : IDataService
    {
        private readonly DogRallyDbContext _dogRallyDbContext;
        private readonly UserManager<RallyUser> _userManager;

        public DemoDataService(DogRallyDbContext dogRallyDbContext, 
            UserManager<RallyUser> userManager)
        {
            _userManager = userManager;
            _dogRallyDbContext = dogRallyDbContext;

        }

        // Extract to chatservice
        public async Task<bool> RoomExists(string initiatingUserName, string recipientUserName)
            {
                var chatRoom = await _dogRallyDbContext.ChatRooms
            .Where(room => room.NumberOfAssociatedUsers == 2 &&
                        room.RoomName == $"Chatroom: {initiatingUserName} and {recipientUserName}" 
                        ||
                        room.RoomName == $"Chatroom: {recipientUserName} and {initiatingUserName}")
            .FirstOrDefaultAsync();

            if (chatRoom == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<ChatRoom?> GetChatRoomByNameAsync(string name)
        {
             var chatRoom = await _dogRallyDbContext.ChatRooms
             .Where(r => r.RoomName == name)
             .FirstOrDefaultAsync();

             return chatRoom;
        }

        public async Task<RallyUser?> GetUserByNameAsync(string userName)
        {
            var retrievedUser = await _userManager.FindByNameAsync(userName);
            return retrievedUser;
        }

        // TO-DO: Estract conversion from RallyUser to UserViewModel in SearchUser service
        // extract to Search User service
        public async Task<List<RallyUser>> GetSimilarNamedUsersAsync(string userName)
        {
            List<RallyUser> users = await _userManager.Users
            .Where(x => x.UserName.StartsWith(userName))
            .ToListAsync();
            return users;
        }

        // TO-DO:
        // Make use of UserManager instead?
        // Concider using UserManager instead of this method all together.
        public async Task<List<RallyUser>> GetAllUserNamesAsync()
        {
            var ListOfUsers = await _userManager.Users.ToListAsync();
            return ListOfUsers;
        }

        public async Task AddUserToChatRoomAsync(string userName, int chatRoomId)
        {
            var user = await _userManager.FindByNameAsync(userName);

            try
            {
                if (user == null)
                {
                    throw new InvalidOperationException("A user with that username does not exist");
                }

                var chatRoom = await _dogRallyDbContext.ChatRooms
                    .Include(cr => cr.ParticipatingUsers)
                    .FirstOrDefaultAsync(cr => cr.Id == chatRoomId);

                if (chatRoom == null)
                {
                    throw new InvalidOperationException($"Chatroom with ID '{chatRoomId}' was not found");
                }

                if (chatRoom.ParticipatingUsers.Any(u => u.Id == user.Id))
                {
                    throw new InvalidOperationException($"User '{userName}' is already in the chat room");
                }

                chatRoom.ParticipatingUsers.Add(user);
            }
            catch (InvalidOperationException ex)
            {
               // Lets make some fancy error handling here =)
            }

            await _dogRallyDbContext.SaveChangesAsync();
        }

        public async Task<List<ChatRoom>> GetAssociatedChatRoomsWithMessagesAsync(string userId)
        {
            var user = await _dogRallyDbContext.RallyUsers
                .Include(u => u.AssociatedChatRooms)
                .ThenInclude(c => c.ChatMessages)
                .ThenInclude(m => m.Sender)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                // Return the list of associated chat rooms
                return user.AssociatedChatRooms.ToList();
            }
            else
            {
               
                return new List<ChatRoom>();
            }
                
            
        }

        public async Task<List<Message>> GetMessagesForChatRoomAsync(int chatRoomId)
        {
            var messages = await _dogRallyDbContext.Messages
                                .Include(m => m.Sender)
                                .Where(m => m.ChatRoomId == chatRoomId)
                                .OrderBy(m => m.TimeStamp)
                                .ToListAsync();

            // TO-DO: Extract it to ChatService
            // Map to ChatMessageVM
            //var messageVMs = messages.Select(message => new ChatMessageVM
            //{
            //    Sender = message.Sender,
            //    MessageBody = message.MessageBody,
            //    ChatRoomId = message.ChatRoomId,
            //    TimeStamp = message.TimeStamp
            //}).ToList();

            return messages;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _dogRallyDbContext.Messages.ToListAsync();
        }

        public async Task AddMessageAsync(string messageBody, int chatRoomId, RallyUser sender)
        {
            Message message = new Message
            {
                MessageBody = messageBody,
                Sender = sender,
                ChatRoomId = chatRoomId
            };

            await _dogRallyDbContext.Messages.AddAsync(message);
            await _dogRallyDbContext.SaveChangesAsync();
        }

        // For create ChatRoom-functions. Extract this to ChatService?
        public async Task CreateChatRoom(ChatRoom chatRoom)
        {
            _dogRallyDbContext.ChatRooms.Add(chatRoom);
            await _dogRallyDbContext.SaveChangesAsync();
        }
    }
}
