using DogRallyManager.DbContexts;
using DogRallyManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogRallyManager.Controllers;

public class BoardsController(DogRallyDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        var boardModel = new BoardModel
        {
            ChatRoom = await dbContext.ChatRooms
                              .Include(cr => cr.ChatMessages)
                                .ThenInclude(m => m.Sender)
                              .SingleOrDefaultAsync(x => x.Id == 1),

            Signs = await dbContext.Signs.ToListAsync()
        };
        return View(boardModel);
    }
    
}