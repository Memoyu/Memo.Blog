using Memo.Blog.Domain.Events.OpenSources;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.OpenSources.Command.Sync;

internal class SynchronousCommandHandler(ILogger<SynchronousCommandHandler> logger, IPublisher publisher) : IRequestHandler<SynchronousCommand, Result>
{
    public async Task<Result> Handle(SynchronousCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await publisher.Publish(new SyncGitHubRepoEvent(), cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"手动同步github项目异常：{ex.Message}");
            return Result.Failure($"同步失败：{ex.Message}");
        }
        return Result.Success();
    }
}
