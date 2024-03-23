using Memo.Blog.Application.Comments.Commands.Update;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Comments.Commands.Delete;
public class DeleteCommentCommandHandler(
    ILogger<UpdateCommentCommandHandler> logger,
    IMapper mapper,
    IBaseDefaultRepository<Comment> commentRepo,
     IBaseMongoRepository<ArticleCollection> articleMongoResp
    ) : IRequestHandler<DeleteCommentCommand, Result>
{
    public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await commentRepo.Select.Where(c => c.CommentId == request.CommentId).FirstAsync(cancellationToken);
        if (comment == null) return Result.Failure("评论不存在");

        // 如果是文章评论，则需要更新mongodb数据
        if (comment.CommentType == Domain.Enums.CommentType.Article)
        {
            comment.AddDomainEvent(new ArticleDeleteCommentEvent(comment.BelongId, comment.CommentId));
        }

        // 删除评论
        var row = await commentRepo.DeleteAsync(comment, cancellationToken);
        if (row <= 0) throw new Exception("删除评论失败");

        return Result.Success(comment.CommentId);
    }
}
