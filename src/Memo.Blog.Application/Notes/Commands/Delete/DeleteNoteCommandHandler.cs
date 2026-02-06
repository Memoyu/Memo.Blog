using Memo.Blog.Domain.Events.SubmissionRecord;

namespace Memo.Blog.Application.Notes.Commands.Delete;

public class DeleteNoteCommandHandler(
    IBaseDefaultRepository<Note> noteRepo
    ) : IRequestHandler<DeleteNoteCommand, Result>
{
    public async Task<Result> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await noteRepo.Select.Where(c => c.NoteId == request.NoteId).FirstAsync(cancellationToken) ?? 
            throw new ApplicationException("笔记不存在或已删除");
        // 删除笔记
        note.AddDomainEvent(new CreateSubmissionRecordEvent(note.NoteId, SubmissionRecordType.Note, SubmissionRecordOperate.Delete));
        var row = await noteRepo.DeleteAsync(note, cancellationToken);
        return row <= 0 ? throw new ApplicationException("删除笔记失败") : (Result)Result.Success(note.NoteId);
    }
}
