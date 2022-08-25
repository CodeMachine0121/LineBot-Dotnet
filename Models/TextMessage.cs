namespace LineBotDemo.Models;

public class TextMessage : Message
{
    public string Text { get; set; }
    public LineMessageType Type => LineMessageType.text;
}