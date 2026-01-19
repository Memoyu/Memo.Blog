namespace Memo.Blog.Application.Notes.Commands.Create;

public class CreateGroupCommandHandler(
    IBaseDefaultRepository<NoteGroup> noteGroupRepo
    ) : IRequestHandler<CreateGroupCommand, Result>
{
    public async Task<Result> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentId.HasValue && !await noteGroupRepo.Select.AnyAsync(c => c.GroupId == request.ParentId, cancellationToken))
            throw new ApplicationException("父分组不存在或已删除");

        if (await noteGroupRepo.Select.AnyAsync(c => c.Title == request.Title, cancellationToken))
            throw new ApplicationException("同名分组已存在");

        var group = new NoteGroup
        {
            ParentId = request.ParentId,
            Title = request.Title,
        };
        group = await noteGroupRepo.InsertAsync(group, cancellationToken);

        return group == null || group.Id == 0 ? throw new ApplicationException("保存分组失败") : Result.Success(group.GroupId);
    }
}
