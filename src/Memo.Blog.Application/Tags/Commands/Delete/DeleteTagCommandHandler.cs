using Memo.Blog.Domain.Events.Tags;

namespace Memo.Blog.Application.Tags.Commands.Delete;

public class DeleteTagCommandHandler(
    IBaseDefaultRepository<Tag> tagResp
    ) : IRequestHandler<DeleteTagCommand, Result>
{
    public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagResp.Select.Where(t => t.TagId == request.TagId).ToOneAsync(cancellationToken);
        if (tag == null) return Result.Failure("标签不存在");

        tag.AddDomainEvent(new TagDeletedEvent(tag.TagId));

        var rows = await tagResp.DeleteAsync(tag, cancellationToken);

        return rows > 0 ? Result.Success() : Result.Failure("删除标签失败");
    }
}
