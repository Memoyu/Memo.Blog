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
            .Where(c => c.CommentType == request.CommentType)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), c=> c.Nickname.Contains(request.Nickname!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Ip), c => c.Ip.Contains(request.Ip!))
            .WhereIf(request.DateBegin.HasValue && request.DateEnd.HasValue, c => c.CreateTime <= request.DateEnd && c.CreateTime >= request.DateBegin)
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<PageCommentResult>>(comments);

        return Result.Success(new PaginationResult<PageCommentResult>(results, total));
    }
}
