
namespace Memo.Blog.Application.Notes.Commands.Update;

public class UpdateGroupCommandHandler(IBaseDefaultRepository<NoteGroup> noteGroupRepo) : IRequestHandler<UpdateGroupCommand, Result>
{
    public async Task<Result> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await noteGroupRepo.Select.Where(c => c.GroupId == request.GroupId).FirstAsync(cancellationToken) ??
            throw new ApplicationException("分组不存在或已删除");

        group.Title = request.Title;
        var affrows = await noteGroupRepo.UpdateAsync(group, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新分组失败");
    }
}
