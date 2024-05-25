using AutoMapper;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers
{
    [Authorize]
    public class SearchUserController : Controller
    {
        private readonly IDataService _dataService;
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;

        public SearchUserController(IDataService dataservice, 
            IMapper mapper,
            ISearchService searchService)
        {
            _dataService = dataservice;
            _mapper = mapper;
            _searchService = searchService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchUser(string userName)
        {
            var userViewModels = await _searchService.SearchUser(userName);
            return Json(userViewModels);
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
