using DogRallyManager.Database.Models.Signs;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers;

public class SignController(ISignService signService) : Controller
{
    public IActionResult Index()
    {
        var signs = signService.GetAllSigns();
        return View(signs);
    }

    public IActionResult MoveSign(int signId, int newX, int newY)
    {
        signService.MoveSigns(signId, newX, newY);
        return RedirectToAction("Index");
    }
}