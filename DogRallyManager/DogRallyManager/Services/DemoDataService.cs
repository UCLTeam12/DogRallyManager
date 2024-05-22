using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.AccountVMs;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DogRallyManager.Services
{
    public class DemoDataService : IDataService
    {
        private readonly DogRallyDbContext _dogRallyDbContext;
        private readonly UserManager<RallyUser> _userManager;

        public DemoDataService(DogRallyDbContext dogRallyDbContext, UserManager<RallyUser> userManager)
        {
            _userManager = userManager;
            _dogRallyDbContext = dogRallyDbContext;

        }


        public async Task CreateGeneralChatRoomAsync()
        {
            var generalRoom = await _dogRallyDbContext.ChatRooms
            .FirstOrDefaultAsync(x => x.RoomName == "General" && x.Id == 1);

            if (generalRoom == null)
            {
                ChatRoom createdGeneralRoom = new ChatRoom { Id = 1, RoomName = "General"};
                await _dogRallyDbContext.ChatRooms.AddAsync(createdGeneralRoom);
                await _dogRallyDbContext.SaveChangesAsync();
            }

        }


        public async Task<UserViewModel> GetUserAsync(string userName)
        {
            var retrievedUser = await _dogRallyDbContext.Users
                .Where(x => x.UserName == userName)
                .FirstOrDefaultAsync();

            if (retrievedUser == null)
            {
                return null;
            }
            var existingUserViewModel = new UserViewModel { UserName = retrievedUser.UserName};

            return existingUserViewModel;
        }

        public async Task<List<UserViewModel>> GetSimilarNamedUsersAsync(string userName)
        {
            var users = await _dogRallyDbContext.Users
                .Where(x => x.UserName.StartsWith(userName))
                .Select(x => new UserViewModel
                {
                    UserName = x.UserName,
                })
                .ToListAsync();

            if (!users.Any())
            {
                return new List<UserViewModel>();
            }

            return users;
        }


        public async Task<List<UserViewModel>> GetAllUserNamesAsync()
        {
            var ListOfUserNames = await _dogRallyDbContext.Users
                .Select(u => u.UserName )
                .ToListAsync();

              List<UserViewModel> userViewModels = ListOfUserNames.Select(username => new UserViewModel { UserName = username } )  
             .ToList();

            return userViewModels;
               
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

        public async Task<List<ChatMessageVM>> GetMessagesForChatRoomAsync(int chatRoomId)
        {
            var messages = await _dogRallyDbContext.Messages
                                .Include(m => m.Sender)
                                .Where(m => m.ChatRoomId == chatRoomId)
                                .OrderBy(m => m.TimeStamp)
                                .ToListAsync();

            // Map to ChatMessageVM
            var messageVMs = messages.Select(message => new ChatMessageVM
            {
                Sender = message.Sender,
                MessageBody = message.MessageBody,
                ChatRoomId = message.ChatRoomId,
                TimeStamp = message.TimeStamp
            }).ToList();

            return messageVMs;
        }


        public async Task<List<Message>> GetAllMessagesAsync()
        {
            return await _dogRallyDbContext.Messages.ToListAsync();
        }




        public async Task AddMessageAsync(Message message)
        {
            await _dogRallyDbContext.Messages.AddAsync(message);
            await _dogRallyDbContext.SaveChangesAsync();
        }

        public async Task AddChatRoomAsync(ChatRoom chatRoom)
        {
            _dogRallyDbContext.ChatRooms.Add(chatRoom);
            await _dogRallyDbContext.SaveChangesAsync();
        }
    }
}
