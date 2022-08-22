namespace LineBotDemo.Models;

public class WebhookEvent
{
    public string destination { get; set; }
    public Event[] events { get; set; }
}


public class Event
{
    public string replyToken { get; set; }
    public WebhookEventType type { get; set; }
    public string mode { get; set; }
    public long timestamp { get; set; }
    
    public Source source { get; set; }
    public Message message { get; set; }
    
}

public class Source
{
    public string type { get; set; }
    public string userId { get; set; }
}

public class Message
{
    public string id { get; set; }
    public LineMessageType type { get; set; }
    public string text { get; set; }
}