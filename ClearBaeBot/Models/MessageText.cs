using Microsoft.OpenApi.Extensions;

namespace ClearBaeBot.Models;

public class MessageText:Message
{
    // MessageResponse 底下要回復的訊息 
    // GetDisplayName=> 取得變數名
    public string type => MessageType.text.GetDisplayName();
    
    public string text { get; set; }
}