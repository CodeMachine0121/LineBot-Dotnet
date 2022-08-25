using ClearBaeBot.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.HttpStatusCode;

namespace ClearBaeBot.Controllers;
[Route("api/[controller]")]
[ApiController]

public class BaeBotController: ControllerBase
{
    public BaeBot baeBot;
    
    public BaeBotController()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var config = builder.Build();
        var tokenSetting = config.GetSection("TokenSetting").Get<TokenSetting>();

        baeBot = new BaeBot(tokenSetting);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetMessage(WebHookEvent request)
    {
        await baeBot.GetEventAsync(request.events);
        return Ok();
    }

  
}