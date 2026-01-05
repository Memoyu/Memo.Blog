namespace Memo.Blog.Application.Notes.Commands.Create;

public class CreateNoteCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<Note> noteRepo,
    IBaseDefaultRepository<NoteCatalog> noteCatalogRepo) : IRequestHandler<CreateNoteCommand, Result>
{
    public async Task<Result> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        if (!await noteCatalogRepo.Select.AnyAsync(c => c.CatalogId == request.CatalogId, cancellationToken))
            throw new ApplicationException("目录不存在或已删除");

        var note = mapper.Map<Note>(request);
        note = await noteRepo.InsertAsync(note, cancellationToken);

        return note == null || note.Id == 0 ? throw new ApplicationException("保存笔记失败") : Result.Success(note.NoteId);
    }
}
