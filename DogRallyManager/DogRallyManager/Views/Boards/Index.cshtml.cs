using DogRallyManager.Database.Models.Boards;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DogRallyManager.Views.Boards;

public class Index : PageModel
{
    public void OnGet()
    {
        
    }
    public required List<Board> BoardsList { get; init; }
}