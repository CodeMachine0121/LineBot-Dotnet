namespace ClearBaeBot.Models;

public class WebHookEvent
{
    public string destination { get; set; }
    public Event[] events { get; set; }
}