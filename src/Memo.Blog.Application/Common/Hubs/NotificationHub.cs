using Microsoft.AspNetCore.SignalR;


namespace Memo.Blog.Application.Common.Hubs;

public class NotificationHub : Hub<IManagementHubClient>
{
    public NotificationHub()
    {
    }
}
