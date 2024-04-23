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
            .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), c=> c.Nickname.Contains(request.Nickname!))
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
            .Where(c => c.CommentType == request.CommentType)
            .WhereIf(request.BelongId.HasValue, c => c.BelongId == request.BelongId)
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        // var results = mapper.Map<List<CommentClientResult>>(comments);        
        var results = new List<CommentClientResult>();
        int idx = 1;
        var layers = comments.Where(c => !c.ParentId.HasValue).ToList();
        foreach (var layer in layers)
        {
            var childs = comments.Where(c => c.ParentId.HasValue && c.ParentId.Value == layer.CommentId).ToList();

            var result = mapper.Map<CommentClientResult>(layer);
            result.Layer = idx++;
            result.Childs = mapper.Map<List<CommentClientResult>>(childs);
            results.Add(result);
        }

        return Result.Success(new PaginationResult<CommentClientResult>(results, total));
    }
}
