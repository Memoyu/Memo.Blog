
namespace Memo.Blog.Application.Notes.Commands.Update;

public class UpdateGroupCommandHandler(
    IBaseDefaultRepository<Note> noteRepo,
    IBaseDefaultRepository<NoteGroup> noteGroupRepo) : IRequestHandler<UpdateGroupCommand, Result>
{
    public async Task<Result> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        if (request.GroupId.HasValue)
        {
            var group = await noteGroupRepo.Select.Where(c => c.GroupId == request.GroupId).FirstAsync(cancellationToken) ??
                throw new ApplicationException("分组不存在或已删除");
        }

        var affrows = 0;
        if (request.Type == 0)
        {
            var group = await noteGroupRepo.Select.Where(c => c.GroupId == request.Id).FirstAsync(cancellationToken) ??
              throw new ApplicationException("分组不存在或已删除");

            group.ParentId = request.GroupId;
            affrows = await noteGroupRepo.UpdateAsync(group, cancellationToken);
        }
        else
        {
            var note = await noteRepo.Select.Where(c => c.NoteId == request.Id).FirstAsync(cancellationToken) ??
                    throw new ApplicationException("笔记不存在或已删除");

            note.GroupId = request.GroupId;
            affrows = await noteRepo.UpdateAsync(note, cancellationToken);
        }

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新笔记/分组所属分组失败");
    }
}
