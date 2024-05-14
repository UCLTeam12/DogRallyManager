using DogRallyManager.DbContexts;
using DogRallyManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers;

public class BoardsController(DogRallyDbContext dbContext) : Controller
{
    public IActionResult Index()
    {
        var boardModel = new BoardModel
        {
            Signs = dbContext.Signs.ToList()
        };
        return View(boardModel);
    }
    
}