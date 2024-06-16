using Memo.Blog.Domain.Events.Notifications;

namespace Memo.Blog.Application.Messages.Commands.Create;

public class CreateMessageCommandHandler(
    IMapper mapper,
    IPublisher publisher,
    IBaseDefaultRepository<Message> messageRepo
    ) : IRequestHandler<CreateMessageCommand, Result>
{
    public async Task<Result> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        await publisher.Publish(new CreateNotificationEvent { Title = "通知标题", Content = "这是消息内容" });

        return Result.Success();
    }
}
