using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.ChatVMs;

namespace DogRallyManager.MapperProfiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatRoom, ChatRoomVM>();
            CreateMap<Message, ChatMessageVM>();
            CreateMap<ChatRoomVM, ChatRoom>();
            CreateMap<ChatMessageVM, Message>();
        }
    }
}
