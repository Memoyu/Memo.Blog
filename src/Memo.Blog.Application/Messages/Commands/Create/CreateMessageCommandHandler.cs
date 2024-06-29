using Memo.Blog.Domain.Events.Messages;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Messages.Commands.Create;

public class CreateMessageCommandHandler(
    IMapper mapper,
    ILogger<CreateMessageCommandHandler> logger,
    IPublisher publisher,
    IBaseDefaultRepository<Message> messageRepo
    ) : IRequestHandler<CreateMessageCommand, Result>
{
    public async Task<Result> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var @event = mapper.Map<CreateMessageEvent>( request );

        try
        {
            await publisher.Publish(@event);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "发送消息异常");
            return Result.Failure("发送消息异常");
        }

        return Result.Success();
    }
}
