using Memo.Blog.Domain.Events.OpenSources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Common.Services.Background;

/// <summary>
/// 定时同步GitHub个人仓库数据
/// </summary>
/// <param name="serviceScopeFactory"></param>
/// <param name="logger"></param>
internal class GitHubRepoPullTaskService(
     IServiceScopeFactory serviceScopeFactory,
     ILogger<GitHubRepoPullTaskService> logger) : BaseTaskService(logger)
{

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await ExecuteScheduledTaskAsync(
          cancellationToken,
          ScheduledTaskTimeType.Time,
          new TimeSpan(1, 0, 0), // 每小时执行一次
          async () =>
          {
              await ExecuteJobAsync(cancellationToken);
          });
    }

    private async Task ExecuteJobAsync(CancellationToken cancellationToken)
    {
        try
        {
            // 解决BackgroundService is Singleton Service
            using IServiceScope scope = serviceScopeFactory.CreateScope();

            var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

            await publisher.Publish(new SyncGitHubRepoEvent(), cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"自动同步github项目异常：{ex.Message}");
        }
    }

}
