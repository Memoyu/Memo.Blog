using Memo.Blog.Application.Security;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Common.Hubs;

[Authorize]
public class NotificationHub(
    ILogger<NotificationHub> logger,
    IBaseDefaultRepository<User> userRepo,
    ICurrentUserProvider currentUserProvider) : Hub<IManagementHubClient>
{
   // private static Dictionary<long, string> _connections = [];

    /// <summary>
    /// 发送信息 to all
    /// </summary>
    /// <param name="message">信息</param>
    /// <returns></returns>
    public async Task SendAllMessage(string message)
    {
        var form = currentUserProvider.GetCurrentUser().Id;
        if (form <= 0) return;

        var formUser = await userRepo.Select.Where(u => u.UserId == form).FirstAsync();
        //await Clients.All.ReceivedNotification($"{formUser.Nickname} 发来消息", $"内容为：{message}");
    }

    /// <summary>
    /// 发送信息 to 指定用户
    /// </summary>
    /// <param name="to">用户</param>
    /// <param name="message">信息</param>
    /// <returns></returns>
    public async Task SendUserMessage(long? to, string message)
    {
        if (to == null) return;
        var form = currentUserProvider.GetCurrentUser().Id;
        if (form <= 0) return;

        var toUser = await userRepo.Select.Where(u => u.UserId == to).FirstAsync();
        var formUser = await userRepo.Select.Where(u => u.UserId == form).FirstAsync();
        //await Clients.All.ReceivedNotification($"{formUser.Nickname} 发来消息", $"接收方：{toUser.Nickname}，内容为：{message}");
    }

    public override Task OnConnectedAsync()
    {
        var userId = currentUserProvider.GetCurrentUser().Id;

        logger.LogInformation("用户连接：" + userId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {

        var userId = currentUserProvider.GetCurrentUser().Id;
        logger.LogInformation("用户断开：" + userId);
        return base.OnDisconnectedAsync(exception);
    }
}
