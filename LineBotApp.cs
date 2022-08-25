using System.Xml.Serialization;
using LineBotDemo.Models;
using Microsoft.Extensions.Options;

namespace LineBotDemo;

public class LineBotApp
{
    public LineProfileUtility lineprofileUtility;

    public LineBotApp()
    {
    }
    
    public async Task RunAsync(IEnumerable<Event> events)
    {
        foreach (var e in events)
        {
            switch (e.type)
            {
                case WebhookEventType.message:
                    await OnMessageAsync(e);
                    break;
                case WebhookEventType.follow:
                    Console.WriteLine(e.source.userId+" followed");
                    await OnFollowAsync(e);
                    break;
                case WebhookEventType.unfollow:
                    break;
                default:
                    break;
            }
        }
    }

    protected virtual async Task OnMessageAsync(Event ev)
    {
        switch (ev.message.type)
        {
            case LineMessageType.text:
                Console.WriteLine(ev.source.userId);
                var user = await lineprofileUtility.GetUserProfile(ev.source.userId);
                await lineprofileUtility.ReplyMessageAsync(ev.replyToken, $@"{user.displayName} Test Message");
                break;
            case LineMessageType.audio:
                break;
            case LineMessageType.flex:
                break;
            case LineMessageType.image:
                break;
            case LineMessageType.location:
                break;
            case LineMessageType.sticker:
                break;
            case LineMessageType.template:
                break;
            case LineMessageType.video:
                break;
        }
    }

    protected virtual async Task OnFollowAsync(Event ev)
    {
        var user = await lineprofileUtility.GetUserProfile(ev.source.userId);
        await lineprofileUtility.ReplyMessageAsync(ev.replyToken, $"@ Hi {user.displayName}, 歡迎加入");
        
    }

    protected virtual async Task onUnfollowAsync(Event ev)
    {
        
    }
}