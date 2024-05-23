using DogRallyManager.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace DogRallyManager.Controllers;

public class SignController(DogRallyDbContext dbContext) : Controller
{
    public record BoardData(int signId, int newX, int newY);

    [HttpPost]
    public IActionResult MoveSign([FromBody]BoardData boardData)
    {
        var sign = dbContext.Signs.Find(boardData.signId);
        sign.PositionY = boardData.newY;
        sign.PositionX = boardData.newX;
        dbContext.SaveChanges();
        return Ok();
    }

    [HttpGet]
    public IActionResult GetSigns()
    {
        var signs = dbContext.Signs.ToList();
        return Ok(signs);
    }
}