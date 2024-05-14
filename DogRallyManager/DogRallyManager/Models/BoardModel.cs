using DogRallyManager.Database.Models.Signs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DogRallyManager.Models;

public class BoardModel : PageModel
{
    public List<Sign> Signs { get; set; }
}