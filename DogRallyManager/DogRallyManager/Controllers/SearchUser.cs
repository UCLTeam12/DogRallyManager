using AutoMapper;
using DogRallyManager.Services;
using DogRallyManager.ViewModels.AccountVMs;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers
{
    public class SearchUser : Controller
    {
        private readonly IDataService _dataservice;
        private readonly IMapper _mapper;

        public SearchUser(IDataService dataservice, IMapper mapper)
        {
            _dataservice = dataservice;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var ListOfUserVMs = await _dataservice.GetAllUserNamesAsync();

            return View(ListOfUserVMs);
        }
    }
}
