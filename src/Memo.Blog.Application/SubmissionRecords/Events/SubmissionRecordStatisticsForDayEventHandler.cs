using Memo.Blog.Domain.Events.SubmissionRecord;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.SubmissionRecords.Events;

public class SubmissionRecordStatisticsForDayEventHandler(
    ILogger<SubmissionRecordStatisticsForDayEventHandler> logger,
    IBaseDefaultRepository<SubmissionRecord> submissionRecordRepo,
    IBaseDefaultRepository<SubmissionRecordStatistics> subRecordStatisticsRepo
    ) : INotificationHandler<SubmissionRecordStatisticsForDayEvent>
{
    public async Task Handle(SubmissionRecordStatisticsForDayEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            var now = DateTime.Now.Date;
            var weekBegin = now.Date.AddDays(-7); // 往后7天
            var weekEnd = now.Date.AddDays(-1); // 到昨天
            var weeks = weekBegin.GetRanges(weekEnd) ?? [];
            var stats = await subRecordStatisticsRepo.Select
                .Where(srs => weeks.Contains(srs.Date.Date))
                .ToListAsync(cancellationToken);

            foreach (var day in weeks)
            {
                var stat = stats.FirstOrDefault(s => s.Date.Date == day.Date);
                if (stat != null) continue;

                var records = await submissionRecordRepo.Select
                    .Where(sr => sr.CreateTime.Date == day.Date)
                    .ToListAsync(cancellationToken);
                var entity = await subRecordStatisticsRepo.InsertAsync(
                    new SubmissionRecordStatistics
                    {
                        Date = day.Date,
                        Article = records.Count(r => r.Type == SubmissionRecordType.Article),
                        Moment = records.Count(r => r.Type == SubmissionRecordType.Moment),
                        Note = records.Count(r => r.Type == SubmissionRecordType.Note),
                    }, cancellationToken);

                if (entity.Id <= 0)
                    logger.LogError("写入提交记录汇总统计数据错误");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "写入提交记录汇总统计数据异常");
        }
    }
}
