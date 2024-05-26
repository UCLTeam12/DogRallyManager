using AutoMapper;
using DogRallyManager.ViewModels.AccountVMs;

namespace DogRallyManager.Services
{
    public class SearchService : ISearchService
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;


        public SearchService(IDataService dataService,
            IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        public async Task<bool> AddUserToBoardAsync(string username, int boardId)
        {
            bool isAdded = await _dataService.AddUserToBoardAsync(username, boardId);
            return isAdded;
        }
        public async Task<List<UserViewModel>> SearchUser(string userName)
        {
            var users = await _dataService.GetSimilarNamedUsersAsync(userName);
            var userViewModels = _mapper.Map<List<UserViewModel>>(users);
            return userViewModels;
        }
    }
}
