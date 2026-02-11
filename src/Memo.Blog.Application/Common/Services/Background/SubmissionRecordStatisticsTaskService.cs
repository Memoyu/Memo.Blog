using Memo.Blog.Domain.Events.SubmissionRecord;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Common.Services.Background;

/// <summary>
/// 定时统计提交记录数据
/// </summary>
/// <param name="serviceScopeFactory"></param>
/// <param name="logger"></param>
internal class SubmissionRecordStatisticsTaskService(
     IServiceScopeFactory serviceScopeFactory,
     ILogger<SubmissionRecordStatisticsTaskService> logger
    ) : BaseTaskService(logger)
{
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await ExecuteScheduledTaskAsync(
        cancellationToken,
        ScheduledTaskTimeType.Day,
        new TimeSpan(0, 10, 0), // 每天0点1分执行     
       async () =>
       {
           await ExecuteJobAsync(cancellationToken);
       });
    }

    private async Task ExecuteJobAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

        await publisher.Publish(new SubmissionRecordStatisticsForDayEvent(), cancellationToken);
    }
}
