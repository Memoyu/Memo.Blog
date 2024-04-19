using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Common.Services.Background;
internal class GitHubRepoPullTaskService : BaseTaskService
{
    private readonly ILogger _logger;

    public GitHubRepoPullTaskService(ILogger<GitHubRepoPullTaskService> logger) : base(logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ExecuteScheduledTaskAsync(
          stoppingToken,
          new TimeSpan(1, 0, 0),
          0,
          async () =>
          {
              await ExecuteJobAsync(stoppingToken);
          });
    }

    private async Task ExecuteJobAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"定时执行，时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        await Task.CompletedTask;
    }
}
