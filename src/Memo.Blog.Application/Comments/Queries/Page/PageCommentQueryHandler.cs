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
        var layer = (int)total - ((request.Page - 1) * request.Size);
        foreach (var comment in comments)
        {
            var childs = await commentRepo.Select.Where(c => c.ParentId.HasValue && c.ParentId == comment.CommentId).ToListAsync(cancellationToken);

            var result = mapper.Map<CommentClientResult>(comment);
            result.Layer = layer --;
            result.Childs = mapper.Map<List<CommentClientResult>>(childs);
            results.Add(result);
        }

        return Result.Success(new PaginationResult<CommentClientResult>(results, total));
    }
}
