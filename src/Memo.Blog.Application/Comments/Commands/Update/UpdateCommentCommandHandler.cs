
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Comments.Commands.Update;

public class UpdateCommentCommandHandler(
    ILogger<UpdateCommentCommandHandler> logger,
    IMapper mapper,
    IBaseDefaultRepository<Comment> commentRepo,
     IBaseMongoRepository<ArticleCollection> articleMongoResp
    ) : IRequestHandler<UpdateCommentCommand, Result>
{
    public async Task<Result> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await commentRepo.Select.Where(c => c.CommentId == request.CommentId).FirstAsync(cancellationToken);
        if (comment == null) return Result.Failure("评论不存在");


        // 如果是文章评论，则需要更新mongodb数据
        if (comment.CommentType == Domain.Enums.CommentType.Article)
        {
            comment.AddDomainEvent(new ArticleUpdateCommentEvent(comment.BelongId));
        }

        // 更新评论
        comment.Nickname = request.Nickname;
        comment.Avatar = request.Avatar;
        comment.Email = request.Email;
        comment.Content = request.Content;
        comment.Showable = request.Showable;
        var row = await commentRepo.UpdateAsync(comment, cancellationToken);
        if (row <= 0) return Result.Failure("更新评论失败");

        return Result.Success(comment.CommentId);
    }
}
