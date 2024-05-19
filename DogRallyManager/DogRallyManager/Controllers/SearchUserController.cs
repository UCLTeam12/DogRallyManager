using AutoMapper;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers
{
    public class SearchUserController : Controller
    {
        private readonly IDataService _dataservice;
        private readonly IMapper _mapper;

        public SearchUserController(IDataService dataservice, 
            IMapper mapper)
        {
            _dataservice = dataservice;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var ListOfUserVMs = await _dataservice.GetAllUserNamesAsync();

            return View(ListOfUserVMs);
        }
        [HttpGet]
        public IActionResult StartChat(string recipientUsername)
        {
            // TO-DO: Verify if user exists before creating chatroom

            if (string.IsNullOrEmpty(recipientUsername))
            {
                return Json(new { success = false, message = "Recipient username is required." });
            }

            return RedirectToAction("StartChat", "ChatController", recipientUsername);
        }
    }
}
