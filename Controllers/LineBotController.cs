using LineBotDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LineBotDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LineBotController : Controller
{
    private LineBotApp app = new LineBotApp();
    
    [HttpPost]
    public async Task<IActionResult> Post(WebhookEvent reqeust)
    {
        await app.RunAsync(reqeust.events);
        return Ok(reqeust);
    }

}