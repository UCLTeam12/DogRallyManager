using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DogRallyManager.Models;
using Microsoft.AspNetCore.Authorization;
using DogRallyManager.Services;

namespace DogRallyManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDataService _dataService;

    public HomeController(ILogger<HomeController> logger,
        IDataService dataService)
    {
        _dataService = dataService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        // On first load of the app, it will create the General chatroom.
        // Does this belong somewhere else, would you say? You might say chatcontroller, but issue related to this is that
        // when a user is registered, they automatically gets added to the general chatroom.
        // if we place it in chatcontroller, there is no room for user to get added to, and it will give an error.
        _dataService.CreateGeneralChatRoomAsync();
        return View();
    }

    [Authorize]
    [HttpGet("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}