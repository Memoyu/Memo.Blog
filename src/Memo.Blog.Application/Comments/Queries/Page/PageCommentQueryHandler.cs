using Memo.Blog.Application.Comments.Common;
using Memo.Blog.Application.Common.Extensions;

namespace Memo.Blog.Application.Comments.Queries.Page;

public class PageCommentQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Comment> commentRepo,
    IBaseDefaultRepository<Visitor> visitorRepo
    ) : IRequestHandler<PageCommentQuery, Result>
{
    public async Task<Result> Handle(PageCommentQuery request, CancellationToken cancellationToken)
    {
        var isHasFilter = false;
        var comments = new List<Comment>();
        var total = 0L;
        var results = new List<PageCommentResult>();

        var select = commentRepo.Select
            .Include(c => c.Visitor)
            .WhereIf(request.CommentType.HasValue, c => c.CommentType == request.CommentType)
            .WhereIf(request.DateBegin.HasValue && request.DateEnd.HasValue, c => c.CreateTime <= request.DateEnd && c.CreateTime >= request.DateBegin)
            .OrderByDescending(a => a.CreateTime);

        // 存在查询条件情况下，平层展示
        if (!string.IsNullOrWhiteSpace(request.Nickname) || (!string.IsNullOrWhiteSpace(request.Content) || !string.IsNullOrWhiteSpace(request.Ip)))
        {
            // 根据昵称取符合条件的访客
            var visitorIds = new List<long>();
            if (!string.IsNullOrWhiteSpace(request.Nickname))
            {
                visitorIds = await visitorRepo.Select
               .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), v => v.Nickname.Contains(request.Nickname!))
               .ToListAsync(v => v.VisitorId, cancellationToken);
            }

            comments = await select
                 .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), c => visitorIds.Count > 0 && visitorIds.Contains(c.VisitorId))
                 .WhereIf(!string.IsNullOrWhiteSpace(request.Content), c => c.Content.Contains(request.Content!))
                 .WhereIf(!string.IsNullOrWhiteSpace(request.Ip), c => c.Ip.Contains(request.Ip!))
                 .ToPageListAsync(request, out total, cancellationToken);

            isHasFilter = true;
            results = mapper.Map<List<PageCommentResult>>(comments);
        }

        // 否则，树形结构化展示
        if (!isHasFilter)
        {
            comments = await select
                .Where(c => !c.ParentId.HasValue) // 以没有父评论的评论作为分页的根据
                .ToPageListAsync(request, out total, cancellationToken);

            var parentIds = comments.Select(c => c.CommentId).ToList();

            var allChilds = await commentRepo.Select
                .Where(c => c.ParentId.HasValue && parentIds.Contains(c.ParentId.Value))
                .Include(c => c.Visitor)
                .ToListAsync(cancellationToken);

            var childIds = allChilds.Where(c => c.ReplyId.HasValue).Select(c => c.ReplyId).ToList();
            var replies = await commentRepo.Select
             .Where(c => c.ReplyId.HasValue && childIds.Contains(c.ReplyId))
             .ToListAsync(cancellationToken);

            foreach (var comment in comments)
            {
                var result = mapper.Map<PageCommentResult>(comment);
                var childs = allChilds.Where(ac => ac.ParentId == comment.CommentId).ToList();
                foreach (var child in childs)
                {
                    var childResult = mapper.Map<PageCommentResult>(child);

                    if (child.ReplyId.HasValue)
                    {
                        var reply = replies.FirstOrDefault(r => r.CommentId == child.ReplyId);
                        if (reply != null)
                            childResult.Reply = mapper.Map<CommentReplyResult>(reply);
                    }

                    result.Children.Add(childResult);
                }

                results.Add(result);
            }
        }

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
            .Where(m => m.Showable)
            .Where(c => !c.ParentId.HasValue) // 以没有父评论的评论作为分页的根据
            .Include(c => c.Visitor)
            .Where(c => c.CommentType == request.CommentType)
            .WhereIf(request.BelongId.HasValue, c => c.BelongId == request.BelongId)
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = new List<CommentClientResult>();

        var parentIds = comments.Select(c => c.CommentId).ToList();

        var allChilds = await commentRepo.Select
            .Where(c => c.ParentId.HasValue && parentIds.Contains(c.ParentId.Value))
            .Include(c => c.Visitor)
            .ToListAsync(cancellationToken);

        var childReplyIds = allChilds.Where(c => c.ReplyId.HasValue).Select(c => c.ReplyId).Distinct().ToList();
        var replies = await commentRepo.Select
            .Include(c => c.Visitor)
            .Where(c => childReplyIds.Contains(c.CommentId))
            .ToListAsync(cancellationToken);

        foreach (var comment in comments)
        {
            var result = mapper.Map<CommentClientResult>(comment);
            var childs = allChilds.Where(ac => ac.ParentId == comment.CommentId).ToList();
            foreach (var child in childs)
            {
                var childResult = mapper.Map<CommentClientResult>(child);

                if (child.ReplyId.HasValue)
                {
                    var reply = replies.FirstOrDefault(r => r.CommentId == child.ReplyId);
                    if (reply != null)
                        childResult.Reply = mapper.Map<CommentReplyResult>(reply);
                }

                result.Childs.Add(childResult);
            }

            results.Add(result);
        }

        return Result.Success(new PaginationResult<CommentClientResult>(results, total));
    }
}
