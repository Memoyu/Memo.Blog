using Memo.Blog.Application.Common.Hubs;
using Memo.Blog.Domain.Events.Messages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Messages.Events;

internal class MessageNotificationEventHandler(
    ILogger<MessageNotificationEventHandler> logger,
    IHubContext<NotificationHub, IManagementHubClient> hubContext) : INotificationHandler<MessageNotificationEvent>
{
    public async Task Handle(MessageNotificationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.ToUsers == null || notification.ToUsers.Count < 1)
        {
            logger.LogError("消息推送时，接收方Ids为空，取消推送");
            return;
        }

        // 通过SignalR发送给指定用户
        await hubContext.Clients
            .Users(notification.ToUsers.Select(i => i.ToString()))
            .ReceivedNotification(notification.Type, notification.MessageId.ToString(), notification.Content); // 源于前端number最大值问题，所以，简单点,MessageId直接字符串
    }
}
