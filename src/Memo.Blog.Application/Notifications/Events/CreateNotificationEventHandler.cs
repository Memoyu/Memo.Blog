using Memo.Blog.Application.Common.Hubs;
using Memo.Blog.Domain.Events.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Memo.Blog.Application.Notifications.Events;

public class CreateNotificationEventHandler(IHubContext<NotificationHub, IManagementHubClient> notificationHub) : INotificationHandler<CreateNotificationEvent>
{
    public async Task Handle(CreateNotificationEvent notification, CancellationToken cancellationToken)
    {
        await notificationHub.Clients.All.ReceivedNotification(notification.Title, notification.Content);
    }
}
