
namespace Memo.Blog.Application.Notes.Commands.Update;

public class UpdateNoteCommandHandler(
    IBaseDefaultRepository<Note> noteRepo,
    IBaseDefaultRepository<NoteCatalog> noteCatalogRepo) : IRequestHandler<UpdateNoteCommand, Result>
{
    public async Task<Result> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var catalog = await noteCatalogRepo.Select.Where(c => c.CatalogId == request.CatalogId).FirstAsync(cancellationToken) ??
         throw new ApplicationException("目录不存在或已删除");

        var note = await noteRepo.Select.Where(c => c.NoteId == request.NoteId).FirstAsync(cancellationToken) ??
           throw new ApplicationException("笔记不存在或已删除");

        note.CatalogId = request.CatalogId;
        note.Title = request.Title;
        note.Content = request.Content;
        var affrows = await noteRepo.UpdateAsync(note, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新笔记失败");
    }
}
