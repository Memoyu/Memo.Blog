﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Common.Services.Background;

internal abstract class BaseTaskService : BackgroundService
{
    private readonly ILogger _logger;

    public BaseTaskService(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 运行周期的任务
    /// </summary>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="time">TimeSpan</param>
    /// <param name="timeType">时间类型 0：小时、分钟、秒；1：天；</param>
    /// <param name="nextTickHandle">周期任务</param>
    /// <returns>returns</returns>
    public async Task ExecuteScheduledTaskAsync(CancellationToken cancellationToken, TimeSpan time, int timeType, Func<Task> nextTickHandle)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var delay = CalcDelayToNextTime(time, timeType);
            _logger.LogInformation($"{GetType().Name}自动化程序，将在{delay}后处理");
            await Task.Delay(delay, cancellationToken);
            await nextTickHandle();
        }
    }

    private TimeSpan CalcDelayToNextTime(TimeSpan momentTime, int timeType)
    {
        switch (timeType)
        {
            case 0:
                return DateTime.Now.Add(momentTime) - DateTime.Now;
            case 1:
                return DateTime.Now.TimeOfDay < momentTime ?
                        DateTime.Now.Date.Add(momentTime) - DateTime.Now : // 延迟到今天
                        DateTime.Now.Date.AddDays(1).Add(momentTime) - DateTime.Now; // 延迟到明天
            default:
                throw new Exception("未指定的时间类型");
        }
    }
}
