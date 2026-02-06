using Memo.Blog.Domain.Events.SubmissionRecord;

namespace Memo.Blog.Application.SubmissionRecords.Events;

public class CreateSubmissionRecordEventHandler(
    IMapper mapper,
    IBaseDefaultRepository<SubmissionRecord> submissionRecordRepo
    ) : INotificationHandler<CreateSubmissionRecordEvent>
{
    public async Task Handle(CreateSubmissionRecordEvent notification, CancellationToken cancellationToken)
    {
        var record = mapper.Map<SubmissionRecord>(notification);
        record = await submissionRecordRepo.InsertAsync(record, cancellationToken);
    }
}
