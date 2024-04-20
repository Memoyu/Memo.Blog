using Memo.Blog.Domain.Events.OpenSources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Common.Services.Background;

internal class GitHubRepoPullTaskService(
     IServiceScopeFactory serviceScopeFactory,
     ILogger<GitHubRepoPullTaskService> logger
     ) : BaseTaskService(logger)
{


    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await ExecuteScheduledTaskAsync(
          cancellationToken,
          new TimeSpan(1, 0, 0),
          0,
          async () =>
          {
              await ExecuteJobAsync(cancellationToken);
          });
    }

    private async Task ExecuteJobAsync(CancellationToken cancellationToken)
    {
        // 解决BackgroundService is Singleton Service
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        await publisher.Publish(new SyncGitHubRepoEvent(), cancellationToken);
    }

}
