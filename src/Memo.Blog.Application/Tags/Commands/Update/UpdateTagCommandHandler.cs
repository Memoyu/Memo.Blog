namespace Memo.Blog.Application.Tags.Commands.Update;

public class UpdateCategoryCommandHandler(
    IBaseDefaultRepository<Tag> tagRepo
    ) : IRequestHandler<UpdateTagCommand, Result>
{
    public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepo.Select.Where(t => t.TagId == request.TagId).FirstAsync(cancellationToken);
        if (tag == null) throw new ApplicationException("标签不存在");

        var exist = await tagRepo.Select.AnyAsync(t => t.TagId != request.TagId && request.Name == t.Name, cancellationToken);
        if (exist) throw new ApplicationException("标签名已存在");

        tag.Name = request.Name;
        tag.Color = request.Color;
        var rows = await tagRepo.UpdateAsync(tag, cancellationToken);

        return rows > 0 ? Result.Success() : throw new ApplicationException("更新标签失败");
    }
}
