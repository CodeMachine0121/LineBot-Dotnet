namespace LineBotDemo.Models;

public class LineMessageReq
{
    public string ReplyToken { get; set; }
    public List<Message> Message { get; set; } = new List<Message>();
}