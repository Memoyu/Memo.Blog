using Memo.Blog.Application.SubmissionRecords.Common;

namespace Memo.Blog.Application.SubmissionRecords.Queries.List;

public class ListSubmissionRecordQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<SubmissionRecord> submissionRecordRepo,
    IBaseDefaultRepository<SubmissionRecordStatistics> submissionStatRecordRepo
    ) : IRequestHandler<ListSubmissionRecordQuery, Result>
{
    public async Task<Result> Handle(ListSubmissionRecordQuery request, CancellationToken cancellationToken)
    {
        DateTime start, end;
        var now = DateTime.Now.Date;
        if (request.Year <= 0)
        {
            // 查询过去一年数据
            end = now.AddDays(1 - now.Day).AddMonths(1).AddDays(-1);
            start = DateTime.Parse($"{now.Year - 1}-{now.Month + 1}-01");
        }
        else
        {
            // 查询指定年份数据
            start = DateTime.Parse($"{request.Year}-01-01");
            end = start.AddYears(1).AddDays(-1);
        }

        // 获取提交记录统计数据
        var records = await submissionStatRecordRepo.Select
            .Where(r => r.Date >= start && r.Date <= end)
            .ToListAsync(cancellationToken);

        // 当天的提交数据也需要统计
        if (now >= start && now <= end)
        {
            var todayRecords = await submissionRecordRepo.Select
                .Where(r => r.CreateTime >= now && r.CreateTime <= now.AddDays(1).AddSeconds(-1))
                .ToListAsync(cancellationToken);
            records.Add(new SubmissionRecordStatistics
            {
                Date = now,
                Article = todayRecords.Count(r => r.Type == SubmissionRecordType.Article),
                Moment = todayRecords.Count(r => r.Type == SubmissionRecordType.Moment),
                Note = todayRecords.Count(r => r.Type == SubmissionRecordType.Note),
            });
        }

        var res = new SubmissionRecordListResult();
        var dic = new Dictionary<string, int>();
        records = [.. records.OrderBy(r => r.Date)];
        foreach (var re in records)
        {
            var sum = re.Article + re.Moment + re.Note;
            dic.Add(re.Date.ToString("yyyy-MM-dd"), sum);
            res.Total += sum;
        }
        res.Record = dic;

        return Result.Success(res);
    }
}
