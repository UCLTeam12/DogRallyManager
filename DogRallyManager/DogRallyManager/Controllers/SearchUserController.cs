using AutoMapper;
using DogRallyManager.Entities;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers
{
    [Authorize]
    public class SearchUserController : Controller
    {
        private readonly IDataService _dataService;
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        private readonly UserManager<RallyUser> _userManager;

        public SearchUserController(IDataService dataservice, 
            IMapper mapper,
            ISearchService searchService,
            UserManager<RallyUser> userManager)
        {
            _dataService = dataservice;
            _mapper = mapper;
            _searchService = searchService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public IActionResult SearchComponent()
        {
            return View("/Views/Components/Search.cshtml"); 
        }

        [HttpGet]
        public async Task<IActionResult> SearchUser(string userName)
        {
            var userViewModels = await _searchService.SearchUser(userName);
            return Json(userViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToBoard(string username, int boardId)
        {
            bool isAdded = await _searchService.AddUserToBoardAsync(username, boardId);
            if (!isAdded)
            {
                return Json(new { success = false, message = "Wrong username or a board with that Id does not exist." });
            }
            else return Json(new { success = true, message = $"{username} added to board with id {boardId}"});
        }

        [HttpGet]
        public IActionResult StartChat(string recipientUsername)
        {
            
            if (string.IsNullOrEmpty(recipientUsername))
            {
                return Json(new { success = false, message = "Recipient username is required." });
            }

            return RedirectToAction("StartChat", "ChatController", recipientUsername);
        }
    }
}
