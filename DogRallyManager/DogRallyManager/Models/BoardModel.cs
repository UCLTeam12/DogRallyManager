using DogRallyManager.Database.Models.Signs;
using DogRallyManager.Entities;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DogRallyManager.Models;

public class BoardModel : PageModel
{
    public int Id { get; set; }
    public List<Sign> Signs { get; set; }
    public ChatRoom AssociatedChatRoom { get; set; }
    public ICollection<RallyUser> ParticipatingUsers { get; set; } = new List<RallyUser>();
}