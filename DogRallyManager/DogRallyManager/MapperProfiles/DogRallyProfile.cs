using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.AccountVMs;
using DogRallyManager.ViewModels.ChatVMs;

namespace DogRallyManager.MapperProfiles
{
    public class DogRallyProfile : Profile
    {
        public DogRallyProfile()
        {
            CreateMap<ChatRoom, ChatRoomVM>();
            CreateMap<ChatRoomVM, ChatRoom>();

            CreateMap<Message, ChatMessageVM>()
            .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => src.Sender));
            CreateMap<ChatMessageVM, Message>();

            CreateMap<UserViewModel, RallyUser>();
            CreateMap<RallyUser, UserViewModel>();

            // Could be concidered redundant, but it contributes to a streamlining of AutoMapper-usage throughout the app.
            CreateMap<string, UserViewModel>();

        }
    }
}
