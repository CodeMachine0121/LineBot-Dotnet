namespace ClearBaeBot.Models;

public class Event
{
    public EventType type { get; set; }
    public Message message { get; set; }
    public Source source { get; set; }
    public string replyToken { get; set; }
    public string mode { get; set; }
    public string webhookEventId { get; set; }
}