using System.Xml.Serialization;
using LineBotDemo.Models;

namespace LineBotDemo;

public class LineBotApp
{
    public async Task RunAsync(IEnumerable<Event> events)
    {
        foreach (var e in events)
        {
            switch (e.type)
            {
                case WebhookEventType.message:
                    break;
                case WebhookEventType.follow:
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
        
    }

    protected virtual async Task onUnfollowAsync(Event ev)
    {
        
    }
}