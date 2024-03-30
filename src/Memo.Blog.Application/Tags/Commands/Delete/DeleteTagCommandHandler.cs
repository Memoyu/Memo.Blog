using Memo.Blog.Domain.Events.Articles;
using Memo.Blog.Domain.Events.Tags;

namespace Memo.Blog.Application.Tags.Commands.Delete;

public class DeleteTagCommandHandler(
    IBaseDefaultRepository<Tag> tagRepo
    ) : IRequestHandler<DeleteTagCommand, Result>
{
    public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepo.Select.Where(t => t.TagId == request.TagId).FirstAsync(cancellationToken) ?? throw new ApplicationException("标签不存在");
        tag.AddDomainEvent(new DeletedTagEvent(request.TagId));
        tag.AddDomainEvent(new UpdatedArticleTagEvent(request.TagId));

        var affrows = await tagRepo.DeleteAsync(tag, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("删除标签失败");
    }
}
