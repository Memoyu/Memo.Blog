using Memo.Blog.Domain.Events.Categories;

namespace Memo.Blog.Application.Notes.Commands.Delete;

public class DeleteGroupCommandHandler(
    IBaseDefaultRepository<NoteGroup> noteGroupRepo
    ) : IRequestHandler<DeleteGroupCommand, Result>
{
    public async Task<Result> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await noteGroupRepo.Select.Where(c => c.GroupId == request.GroupId).FirstAsync(cancellationToken) ?? 
            throw new ApplicationException("分组不存在或已删除");

        group.AddDomainEvent(new DeletedCategoryEvent(group.GroupId));
        // 删除笔记分组
        var row = await noteGroupRepo.DeleteAsync(group, cancellationToken);
        return row <= 0 ? throw new ApplicationException("删除分组失败") : (Result)Result.Success(group.GroupId);
    }
}
