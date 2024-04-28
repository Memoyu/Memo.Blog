namespace Memo.Blog.Application.Visitors.Commands.Update;

public class UpdateVisitorCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<UpdateVisitorCommand, Result>
{
    public async Task<Result> Handle(UpdateVisitorCommand request, CancellationToken cancellationToken)
    {
        var entity = await visitorRepo.Select.Where(c => c.VisitorId == request.VisitorId).FirstAsync(cancellationToken);

        var affrows = 0;
        var visitor = mapper.Map<Visitor>(request);

        // 不存在时，则新建一个访客
        if (entity == null)
        {
            entity = await visitorRepo.InsertAsync(visitor, cancellationToken);
            if (entity.Id > 0) affrows = 1;
        }
        else
        {
            visitor.Id = entity.Id;
            visitor.VisitorId = entity.VisitorId;
            affrows = await visitorRepo.UpdateAsync(visitor, cancellationToken);
        }

        return affrows > 0 ? Result.Success(entity.VisitorId) : throw new ApplicationException("更新访客失败");
    }
}
