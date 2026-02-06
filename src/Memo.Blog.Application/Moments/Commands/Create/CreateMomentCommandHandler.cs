
using Memo.Blog.Domain.Events.SubmissionRecord;

namespace Memo.Blog.Application.Moments.Commands.Create;

public class CreateMomentCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<CreateMomentCommand, Result>
{
    public async Task<Result> Handle(CreateMomentCommand request, CancellationToken cancellationToken)
    {
        var moment = mapper.Map<Moment>(request);
        moment.AddDomainEvent(new CreateSubmissionRecordEvent(moment.MomentId, SubmissionRecordType.Moment, SubmissionRecordOperate.Create));
        moment = await momentRepo.InsertAsync(moment, cancellationToken);

        return moment == null || moment.Id == 0 ? Result.Failure("保存动态失败") : Result.Success(moment.MomentId);
    }
}
