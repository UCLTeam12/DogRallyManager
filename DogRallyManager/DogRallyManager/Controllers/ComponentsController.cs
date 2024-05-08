using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers;

public class ComponentsController : Controller
{
    public IActionResult Chat()
    {
        return View("Chat");
    }
}