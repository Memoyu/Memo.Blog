
namespace Memo.Blog.Application.Notes.Commands.Update;

public class UpdateTitleCommandHandler(
    IBaseDefaultRepository<Note> noteRepo,
    IBaseDefaultRepository<NoteGroup> noteGroupRepo) : IRequestHandler<UpdateTitleCommand, Result>
{
    public async Task<Result> Handle(UpdateTitleCommand request, CancellationToken cancellationToken)
    {
        var affrows = 0;
        if (request.Type == 0)
        {
            var group = await noteGroupRepo.Select.Where(c => c.GroupId == request.Id).FirstAsync(cancellationToken) ??
                     throw new ApplicationException("分组不存在或已删除");
            group.Title = request.Title;
            affrows = await noteGroupRepo.UpdateAsync(group, cancellationToken);
        }
        else
        {
            var note = await noteRepo.Select.Where(c => c.NoteId == request.Id).FirstAsync(cancellationToken) ??
                  throw new ApplicationException("笔记不存在或已删除");
            note.Title = request.Title;
            affrows = await noteRepo.UpdateAsync(note, cancellationToken);
        }

        return affrows > 0 ? Result.Success(request.Id) : throw new ApplicationException("更新标题失败");
    }
}
