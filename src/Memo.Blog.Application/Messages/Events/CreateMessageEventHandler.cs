using Memo.Blog.Domain.Events.Messages;

namespace Memo.Blog.Application.Messages.Events;

public class CreateMessageEventHandler(
    IMapper mapper,
    IBaseDefaultRepository<Message> messageRepo
    ) : INotificationHandler<CreateMessageEvent>
{
    public async Task Handle(CreateMessageEvent notification, CancellationToken cancellationToken)
    {
        var message = mapper.Map<Message>(notification);

        // 构建消息通知模型，并推送事件
        var @event = mapper.Map<MessageNotificationEvent>(message);
        message.AddDomainEvent(@event);

        // 写入消息数据
        message = await messageRepo.InsertAsync(message, cancellationToken);
        if (message.Id == 0) throw new ApplicationException("保存消息失败");
    }
}
