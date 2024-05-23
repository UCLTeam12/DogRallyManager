using DogRallyManager.Database.Models.Signs;
using DogRallyManager.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DogRallyManager.Models;

public class BoardModel : PageModel
{
    public ChatRoom ChatRoom { get; set; }
    public List<Sign> Signs { get; set; }
}