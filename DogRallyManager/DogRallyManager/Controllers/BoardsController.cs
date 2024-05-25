using AutoMapper;
using DogRallyManager.Database.Models.Boards;
using DogRallyManager.Database.Models.Signs;
using DogRallyManager.DbContexts;
using DogRallyManager.Entities;
using DogRallyManager.Models;
using DogRallyManager.ViewModels.ChatVMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogRallyManager.Controllers;

[Authorize]
public class BoardsController : Controller
{
    private readonly DogRallyDbContext _dbContext;
    private readonly UserManager<RallyUser> _userManager;
    private readonly IMapper _mapper;
    public BoardsController(DogRallyDbContext dbContext,
        UserManager<RallyUser> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public record BoardCreateModel(string name);

    public async Task<IActionResult> Details(int id)
    {
        var board = await _dbContext.Boards
            .Include(cr => cr.AssociatedChatRoom)
            .ThenInclude(c => c.ChatMessages)
            .ThenInclude(m => m.Sender)
            .Include(u => u.ParticipatingUsers)
            .FirstOrDefaultAsync(b => b.Id == id);

        var boardModel = new BoardModel
        {
            Signs = _dbContext.Signs.ToList(),
            Id = board.Id,
            AssociatedChatRoom = board.AssociatedChatRoom,
            ParticipatingUsers = board.ParticipatingUsers
        };

        return View(boardModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoard([FromForm]BoardCreateModel boardCreateModel)
    {

        var user = await _userManager.GetUserAsync(User);

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

        board.ParticipatingUsers.Add(user);
        board.AssociatedChatRoom = associatedChatRoom;
        
        _dbContext.Boards.Add(board);
        _dbContext.SaveChanges();
        return RedirectToAction("Details", new {id= board.Id});
    }

    public IActionResult Index()
    {
        var boards = _dbContext.Boards.ToList();
        var pageModel = new DogRallyManager.Views.Boards.Index
        {
            BoardsList = boards
        };
        return View(pageModel);
    }
    
    public async Task<IActionResult> AddUserToBoard(string username, int boardId)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return Json("No user with that username was found");
        }

        var board = await _dbContext.Boards
                             .Where(b => b.Id == boardId)
                             .FirstOrDefaultAsync();
        if(board == null)
        {
            return Json("No board with such an id exists in the database");
        }

        if(board.ParticipatingUsers  == null)
        {
            board.ParticipatingUsers = new List<RallyUser>();
            board.ParticipatingUsers.Add(user); 
        }
        board.ParticipatingUsers.Add(user);
        await _dbContext.SaveChangesAsync();

        return Json("User was succesfully persisted to the database");

    }
    
}