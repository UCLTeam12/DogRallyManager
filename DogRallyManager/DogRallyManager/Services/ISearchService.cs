using DogRallyManager.ViewModels.AccountVMs;

namespace DogRallyManager.Services
{
    public interface ISearchService
    {
        Task<List<UserViewModel>> SearchUser(string userName);
        Task<bool> AddUserToBoardAsync(string username, int boardId);
    }
}