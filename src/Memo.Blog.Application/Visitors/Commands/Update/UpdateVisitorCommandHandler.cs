namespace Memo.Blog.Application.Visitors.Commands.Update;

public class UpdateVisitorCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<UpdateVisitorCommand, Result>
{
    public async Task<Result> Handle(UpdateVisitorCommand request, CancellationToken cancellationToken)
    {
        var entity = await visitorRepo.Select.Where(c => c.VisitorId == request.VisitorId).FirstAsync(cancellationToken) ?? throw new ApplicationException("访客不存在");

        var visitor = mapper.Map<Visitor>(request);
        visitor.Id = entity.Id;
        visitor.VisitorId = entity.VisitorId;
        var affrows = await visitorRepo.UpdateAsync(visitor, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新访客失败");
    }
}
