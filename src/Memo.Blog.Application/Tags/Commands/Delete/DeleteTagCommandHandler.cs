using Memo.Blog.Domain.Events.Tags;

namespace Memo.Blog.Application.Tags.Commands.Delete;

public class DeleteTagCommandHandler(
    IBaseDefaultRepository<Tag> tagResp
    ) : IRequestHandler<DeleteTagCommand, Result>
{
    public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagResp.Select.Where(t => t.TagId == request.TagId).FirstAsync(cancellationToken);
        if (tag == null) throw new ApplicationException("标签不存在");

        tag.AddDomainEvent(new TagDeletedEvent(request.TagId));

        var rows = await tagResp.DeleteAsync(tag, cancellationToken);

        return rows > 0 ? Result.Success() : throw new ApplicationException("删除标签失败");
    }
}
