namespace Memo.Blog.Application.Visitors.Commands.Delete;

public class DeleteVisitorCommandHandler(
    IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<DeleteVisitorCommand, Result>
{
    public async Task<Result> Handle(DeleteVisitorCommand request, CancellationToken cancellationToken)
    {
        var visitor = await visitorRepo.Select.Where(c => c.VisitorId == request.VisitorId).FirstAsync(cancellationToken) ?? throw new ApplicationException("访客不存在");
        var affrows = await visitorRepo.DeleteAsync(visitor, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("删除访客失败");
    }
}
