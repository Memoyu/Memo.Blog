using Memo.Blog.Application.Common.Hubs;
using Memo.Blog.Domain.Events.Messages;
using Microsoft.AspNetCore.SignalR;

namespace Memo.Blog.Application.Messages.Events;

internal class MessageNotificationEventHandler(IHubContext<NotificationHub, IManagementHubClient> hubContext) : INotificationHandler<MessageNotificationEvent>
{
    public async Task Handle(MessageNotificationEvent notification, CancellationToken cancellationToken)
    {
        // 指定用户发送
        if (notification.ToId.HasValue)
        {
            await hubContext.Clients.User(notification.ToId.Value.ToString()).ReceivedNotification(notification.Title, notification.Content);
        }

        // 全部推送
        await hubContext.Clients.All.ReceivedNotification(notification.Title, notification.Content);
    }
}
