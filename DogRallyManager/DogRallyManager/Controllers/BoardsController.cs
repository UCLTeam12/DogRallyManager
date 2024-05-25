using AutoMapper;
using DogRallyManager.Database.Models.Boards;
using DogRallyManager.Database.Models.Signs;
using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.Models;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogRallyManager.Controllers;

public class BoardsController(DogRallyDbContext dbContext) : Controller
{
    
    public record BoardCreateModel(string name);

    public async Task<IActionResult> Details(int id)
    {
        var board = await dbContext.Boards
            .Include(u => u.AssociatedChatRoom)
            .ThenInclude(c => c.ChatMessages)
            .ThenInclude(m => m.Sender)
            .FirstOrDefaultAsync(b => b.Id == id);

        var boardModel = new BoardModel
        {
            Signs = dbContext.Signs.ToList(),
            Id = board.Id,
            ChatRoom = board.AssociatedChatRoom
        };

        return View(boardModel);
    }

    [HttpPost]
    public IActionResult CreateBoard([FromForm]BoardCreateModel boardCreateModel)
    {
        var signList = new List<Sign>();
        for (int i = 1; i < 300; i++)
        {
            signList.Add(

                new Sign()
                {
                    SignType = "exercise-"+i+".png"
                }
            );
        }
        
        var board = new Board()
        {
            Name = boardCreateModel.name,
            Sign = signList,
        };

        ChatRoom associatedChatRoom = new ChatRoom
        {
            RoomName = $"{boardCreateModel.name} chat"
        };

        board.AssociatedChatRoom = associatedChatRoom;
        dbContext.Boards.Add(board);
        dbContext.SaveChanges();
        return RedirectToAction("Details", new {id= board.Id});
    }
    public IActionResult Index()
    {
        var boards = dbContext.Boards.ToList();
        var pageModel = new DogRallyManager.Views.Boards.Index
        {
            BoardsList = boards
        };
        return View(pageModel);
    }
    
    
}