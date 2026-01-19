using Memo.Blog.Domain.Events.Notes;

namespace Memo.Blog.Application.Notes.Events;

public class DeletedNoteGroupEventHandler(
    IBaseDefaultRepository<Note> noteRepo
    ) : INotificationHandler<DeletedNoteGroupEvent>
{
    public async Task Handle(DeletedNoteGroupEvent notification, CancellationToken cancellationToken)
    {
        // 获取分组相关笔记，更新为默认分组
        var notes = await noteRepo.Select.Where(a => a.GroupId == notification.groupId).ToListAsync(cancellationToken) ?? new();
        notes.ForEach(n => n.GroupId = null);
        var affrows = await noteRepo.UpdateAsync(notes, cancellationToken);
    }
}
