using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

public class ChatController : Controller
{
    private static List<Message> _messages = new List<Message>();

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Messages = _messages;
        return View("Chat");
    }

    [HttpPost]
    public IActionResult SendMessage(string content)
    {
        _messages.Add(new Message { Content = content, Timestamp = DateTime.UtcNow });
        return Ok();
    }

    [HttpGet]
    public IActionResult GetMessages()
    {
        return Json(_messages);
    }
}

public class Message
{
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}