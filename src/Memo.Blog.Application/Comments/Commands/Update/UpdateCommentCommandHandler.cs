using Memo.Blog.Domain.Events.Articles;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Comments.Commands.Update;

public class UpdateCommentCommandHandler(
    // ILogger<UpdateCommentCommandHandler> logger,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<UpdateCommentCommand, Result>
{
    public async Task<Result> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await commentRepo.Select.Where(c => c.CommentId == request.CommentId).FirstAsync(cancellationToken) ??
            throw new ApplicationException("评论不存在");

        // 如果是文章评论，则需要更新mongodb数据
        if (comment.CommentType == Domain.Enums.BelongType.Article)
        {
            comment.AddDomainEvent(new UpdatedArticleCommentEvent(comment.BelongId));
        }

        // 更新评论
        comment.Content = request.Content;
        comment.Showable = request.Showable;
        var row = await commentRepo.UpdateAsync(comment, cancellationToken);

        return row <= 0 ? throw new ApplicationException("更新评论失败") : (Result)Result.Success(comment.CommentId);
    }
}
