using ClearBaeBot.Models;

namespace ClearBaeBot;

public class BaeBot
{
    private readonly Properity properity;

    public BaeBot(TokenSetting _tokenSetting)
    {
        properity = new Properity(_tokenSetting);
    }
    
    public async Task GetEventAsync(IEnumerable<Event> events){
        foreach (var e in events)
        {
            var result = await properity.getUserProfile(e.source.userId);
            //Console.WriteLine("[+] Get message from "+ result.displayName);
            
            switch (e.type)
            {
                case EventType.message:
                    await ParseMessageAsync(e);
                    break;
                case EventType.follow:
                    break;
                case EventType.unfollow:
                    break;
                default:
                    break;
            }            
        }
    }

    public virtual async Task ParseMessageAsync(Event e)
    {
     
        switch (e.message.type)
        {
            case MessageType.text:
                Console.WriteLine("\t[-] message: "+e.message.text);
                var result = await properity.getUserProfile(e.source.userId);

                await properity.replyMessage(e.replyToken, $"哈囉 {result.displayName}");
                break;
            case MessageType.audio:
                break;
            case MessageType.file:
                break;
            case MessageType.image:
                break;
            case MessageType.location:
                break;
            case MessageType.sticker:
                break;
            case MessageType.video:
                break;
            default:
                break;
        }
        
        
    }

    public virtual async Task ParseFollowAsync(Event e)
    {
        
    }

    public virtual async Task ParseUnfollowAsync(Event e)
    {
        
    }
}