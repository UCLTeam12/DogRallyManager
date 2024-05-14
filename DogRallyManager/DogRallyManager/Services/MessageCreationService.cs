using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.ChatVMs;
using System;
using System.Threading.Tasks;

namespace DogRallyManager.Services
{
    public class MessageCreationService : IMessageCreationService
    {
        private readonly IMapper _mapper;
        private readonly IDataService _dataService; 

        public MessageCreationService(IMapper mapper, IDataService dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        public async Task CreateMessageAsync(ChatMessageVM chatMessageVM, ChatRoomVM chatRoom)
        {
            //// Check if the chat room exists in the database
            ////var existingRoom = await _dataService.GetChatRoomByIdAsync(chatRoom.ChatRoomName);
            //if (existingRoom == null)
            //{
            //    // If the chat room does not exist, create it
            //    var newRoom = _mapper.Map<ChatRoomVM, Room>(chatRoom);
            //    await _dataService.AddChatRoomAsync(newRoom);

            //    // Update the chat room view model with the newly created room's ID
            //    chatRoom.ChatId = newRoom.Id;
            //}
            //else
            //{
            //    // If the chat room already exists, update the chat room view model with its ID
            //    chatRoom.ChatId = existingRoom.Id;
            //}

            //// Now that we have the chat room ID, create the message entity
            //var message = _mapper.Map<ChatMessageVM, Message>(chatMessageVM);
            //message.SentAt = DateTime.UtcNow;
            //message.ChatRoomId = chatRoom.ChatId; // Assign the chat room ID to the message

            //// Save the message to the database
            //await _dataService.AddMessageAsync(message);
        }
    }
}