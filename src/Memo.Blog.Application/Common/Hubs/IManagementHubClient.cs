namespace Memo.Blog.Application.Common.Hubs;

/// <summary>
///  管理端 SignalR客户端方法定义
/// </summary>
public interface IManagementHubClient
{
    Task ReceivedNotification(MessageType type, string content);
}
