using AutoMapper;

namespace DogRallyManager.MapperProfiles
{
    public class ChatRoomProfile : Profile
    {
        public ChatRoomProfile()
        {
            CreateMap<Entities.ChatRoom, ViewModels.ChatVMs.ChatRoomVM>();
        }
    }
}
