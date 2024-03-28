namespace Memo.Blog.Application.Moments.Commands.Update;

public class UpdateMomentCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<UpdateMomentCommand, Result>
{
    public async Task<Result> Handle(UpdateMomentCommand request, CancellationToken cancellationToken)
    {
        var moment = await momentRepo.Select.Where(c => c.MomentId == request.MomentId).FirstAsync(cancellationToken);
        if (moment == null) throw new ApplicationException("动态不存在");

        var update = mapper.Map<Moment>(request);
        update.Id = moment.Id;
        var rows = await momentRepo.UpdateAsync(update, cancellationToken);

        return rows > 0 ? Result.Success() : throw new ApplicationException("更新动态失败");
    }
}
