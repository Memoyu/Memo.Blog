using Memo.Blog.Application.Common.Hubs;
using Memo.Blog.Domain.Events.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Memo.Blog.Application.Notifications.Events;

public class CreateNotificationEventHandler(IHubContext<NotificationHub, IManagementHubClient> notificationHub) : INotificationHandler<CreateNotificationEvent>
{
    public Task Handle(CreateNotificationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
