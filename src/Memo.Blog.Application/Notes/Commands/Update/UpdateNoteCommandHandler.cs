namespace Memo.Blog.Application.Notes.Commands.Update;

public class UpdateNoteCommandHandler(
    IBaseDefaultRepository<Note> noteRepo,
    IBaseDefaultRepository<NoteGroup> noteGroupRepo) : IRequestHandler<UpdateNoteCommand, Result>
{
    public async Task<Result> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        if (request.GroupId.HasValue)
        {
            var group = await noteGroupRepo.Select.Where(c => c.GroupId == request.GroupId).FirstAsync(cancellationToken) ??
                throw new ApplicationException("分组不存在或已删除");
        }

        var note = await noteRepo.Select.Where(c => c.NoteId == request.NoteId).FirstAsync(cancellationToken) ??
           throw new ApplicationException("笔记不存在或已删除");

        note.GroupId = request.GroupId;
        note.Title = request.Title;
        note.Content = request.Content;
        var affrows = await noteRepo.UpdateAsync(note, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新笔记失败");
    }
}
