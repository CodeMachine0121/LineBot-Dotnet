using System.ComponentModel;

namespace ClearBaeBot.Models;

public enum EventType
{
    [Description("message")]
    message,
    [Description("follow")]
    follow,
    [Description("unfloow")]
    unfollow
}