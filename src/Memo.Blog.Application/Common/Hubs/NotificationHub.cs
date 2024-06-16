using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;


namespace Memo.Blog.Application.Common.Hubs;

public class NotificationHub : Hub<IManagementHubClient>
{
    private readonly ILogger _logger;

    public NotificationHub(ILogger<NotificationHub> logger)
    {
        _logger = logger;
    }

    public override Task OnConnectedAsync()
    {
        var id = Context.ConnectionId;
        _logger.LogError("当前连接：" + id);
        return base.OnConnectedAsync();
    }
}
