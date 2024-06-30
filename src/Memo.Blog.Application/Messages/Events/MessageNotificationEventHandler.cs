using Memo.Blog.Application.Common.Hubs;
using Memo.Blog.Domain.Events.Messages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Messages.Events;

internal class MessageNotificationEventHandler(ILogger<MessageNotificationEventHandler> logger, IHubContext<NotificationHub, IManagementHubClient> hubContext) : INotificationHandler<MessageNotificationEvent>
{
    public async Task Handle(MessageNotificationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.ToUsers == null || notification.ToUsers.Count < 1)
        {
            logger.LogError("消息推送时，接收方Ids为空，取消推送");
            return; 
        }

        // 指定用户发送
        await hubContext.Clients.Users(notification.ToUsers.Select(i => i.ToString())).ReceivedNotification(notification.Type, notification.Content);
    }
}
