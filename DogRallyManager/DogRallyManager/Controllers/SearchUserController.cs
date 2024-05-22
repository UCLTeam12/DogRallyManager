using AutoMapper;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers
{
    public class SearchUserController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public SearchUserController(IDataService dataservice, 
            IMapper mapper)
        {
            _dataService = dataservice;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchUser(string username)
        {
            var users = await _dataService.GetSimilarNamedUsersAsync(username);
            var userViewModels = _mapper.Map<List<UserViewModel>>(users);
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
