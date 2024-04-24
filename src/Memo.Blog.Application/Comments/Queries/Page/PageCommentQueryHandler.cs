using Memo.Blog.Application.Comments.Common;
using Memo.Blog.Application.Common.Extensions;

namespace Memo.Blog.Application.Comments.Queries.Page;

public class PageCommentQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<PageCommentQuery, Result>
{
    public async Task<Result> Handle(PageCommentQuery request, CancellationToken cancellationToken)
    {
        var comments = await commentRepo.Select
            .WhereIf(request.CommentType.HasValue, c => c.CommentType == request.CommentType)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), c => c.Nickname.Contains(request.Nickname!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Ip), c => c.Ip.Contains(request.Ip!))
            .WhereIf(request.DateBegin.HasValue && request.DateEnd.HasValue, c => c.CreateTime <= request.DateEnd && c.CreateTime >= request.DateBegin)
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<PageCommentResult>>(comments);

        return Result.Success(new PaginationResult<PageCommentResult>(results, total));
    }
}

public class PageCommentClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<PageCommentClientQuery, Result>
{
    public async Task<Result> Handle(PageCommentClientQuery request, CancellationToken cancellationToken)
    {
        var comments = await commentRepo.Select
            .Where(c => !c.ParentId.HasValue) // 以没有父评论的评论作为分页的根据
            .Where(c => c.CommentType == request.CommentType)
            .WhereIf(request.BelongId.HasValue, c => c.BelongId == request.BelongId)
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = new List<CommentClientResult>();

        var parentIds = comments.Select(c => c.CommentId).ToList();

        var allChilds = await commentRepo.Select
            .Where(c => c.ParentId.HasValue && parentIds.Contains(c.ParentId.Value))
            .ToListAsync(cancellationToken);

        var childIds = allChilds.Where(c => c.ReplyId.HasValue).Select(c => c.ReplyId).ToList();
        var replies = await commentRepo.Select
         .Where(c => c.ReplyId.HasValue && childIds.Contains(c.ReplyId))
         .ToListAsync(cancellationToken);

        foreach (var comment in comments)
        {
            var result = mapper.Map<CommentClientResult>(comment);
            result.FloorString = $"{result.Floor}楼";
            var childs = allChilds.Where(ac => ac.ParentId == comment.CommentId).ToList();
            foreach (var child in childs)
            {
                var childResult = mapper.Map<CommentClientResult>(child);

                if (child.ReplyId.HasValue)
                {
                    var reply = replies.FirstOrDefault(r => r.CommentId == child.ReplyId);
                    if (reply != null)
                    {
                        childResult. Reply = mapper.Map<CommentClientResult>(reply);
                        childResult.Reply.FloorString = $"{reply.Floor}#";
                    }
                }

                childResult.FloorString = $"{childResult.Floor}#";
                result.Childs.Add(childResult);
            }

            results.Add(result);
        }

        return Result.Success(new PaginationResult<CommentClientResult>(results, total));
    }
}
