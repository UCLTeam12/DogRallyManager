using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<ChatRoom>> GetAssociatedChatRoomsAsync(string userId)
        {
            var user = await _dogRallyDbContext.RallyUsers
                .Include(u => u.AssociatedChatRooms)
                .ThenInclude(c => c.ChatMessages)
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
