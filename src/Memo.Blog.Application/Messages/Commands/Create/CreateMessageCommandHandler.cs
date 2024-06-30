using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Events.Messages;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Messages.Commands.Create;

public class CreateMessageCommandHandler(
    IMapper mapper,
    ILogger<CreateMessageCommandHandler> logger,
    ICurrentUserProvider currentUserProvider,
    IPublisher publisher,
    IBaseDefaultRepository<Message> messageRepo
    ) : IRequestHandler<CreateMessageCommand, Result>
{
    public async Task<Result> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserProvider.GetCurrentUser().Id;

        try
        {
            await publisher.Publish(new CreateMessageEvent
            {
                UserId = userId,
                ToUsers = request.ToUsers,
                ToRoles = request.ToRoles,
                Content = new UserMessageContent
                {
                    Content = request.Content,
                }.ToJson()
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "发送消息异常");
            return Result.Failure("发送消息异常");
        }

        return Result.Success();
    }
}
