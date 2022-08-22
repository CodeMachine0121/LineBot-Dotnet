using LineBotDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace LineBotDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LineBotController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Post(WebhookEvent reqeust)
    {
        
        Console.WriteLine(reqeust.events[0].type);
        Console.WriteLine(reqeust.events[0].message.text);
        return Ok(reqeust);
    }
}