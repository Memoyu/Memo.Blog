using Memo.Blog.Domain.Events.Visitors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Common.Services.Background;

/// <summary>
/// 定时统计访客数据
/// </summary>
/// <param name="serviceScopeFactory"></param>
/// <param name="logger"></param>
internal class VisitStatisticsTaskService(
     IServiceScopeFactory serviceScopeFactory,
     ILogger<GitHubRepoPullTaskService> logger
     ) : BaseTaskService(logger)
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await ExecuteScheduledTaskAsync(
         cancellationToken,
         ScheduledTaskTimeType.Day,
         new TimeSpan(0, 1, 0), // 每天0点1分执行     
        async () =>
        {
            await ExecuteJobAsync(cancellationToken);
        });
    }

    private async Task ExecuteJobAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        await publisher.Publish(new VisitStatisticsForDayEvent(), cancellationToken);
    }
}
