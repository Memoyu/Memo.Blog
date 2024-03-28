namespace Memo.Blog.Application.Moments.Commands.Delete;

public class DeleteMomentCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
    ) : IRequestHandler<DeleteMomentCommand, Result>
{
    public async Task<Result> Handle(DeleteMomentCommand request, CancellationToken cancellationToken)
    {
        var moment = await momentRepo.Select.Where(c => c.MomentId == request.MomentId).FirstAsync(cancellationToken) ?? throw new ApplicationException("动态不存在");

        // TODO: 做评论的删除

        var rows = await momentRepo.DeleteAsync(moment, cancellationToken);

        return rows > 0 ? Result.Success() : throw new ApplicationException("删除动态失败");
    }
}
