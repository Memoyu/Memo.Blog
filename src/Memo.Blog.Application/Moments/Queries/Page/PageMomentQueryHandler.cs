using Memo.Blog.Application.Moments.Common;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Moments.Queries.Page;

public class PageMomentQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo,
    IBaseDefaultRepository<MomentLike> momentLikeRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<PageMomentQuery, Result>
{
    public async Task<Result> Handle(PageMomentQuery request, CancellationToken cancellationToken)
    {
        var selectMoment = momentRepo.Select;

        var tags = request.Tags ?? [];
        if (tags.Count > 0)
        {
            foreach (var tag in tags)
            {
                selectMoment.Where(m => m.Tags.Contains(tag));
            }
        }

        var moments = await selectMoment
            .Include(m => m.Announcer)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Content), m => m.Content.Contains(request.Content!))
            .WhereIf(request.DateBegin.HasValue && request.DateEnd.HasValue, m => m.CreateTime <= request.DateEnd && m.CreateTime >= request.DateBegin)
            .OrderByDescending(m => m.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var momentIdIds = moments.Select(m => m.MomentId).Distinct().ToList();
        var likes = await momentLikeRepo.Select
            .Where(ml => momentIdIds.Contains(ml.MomentId))
            .ToListAsync(cancellationToken);

        var results = mapper.Map<List<MomentResult>>(moments);

        foreach (var moment in results)
        {
            var comments = await commentRepo.Select
               .Where(m => m.Showable)
               .Where(c => c.CommentType == BelongType.Moment)
               .Where(c => c.BelongId == moment.MomentId)
               .CountAsync(cancellationToken);

            moment.Announcer = mapper.Map<MomentAnnouncerResult>(moment.Announcer);
            moment.Comments = (int)comments;
            var momentLikes = likes.Where(l => l.MomentId == moment.MomentId).ToList();
            moment.Likes = momentLikes.Count;
        }

        return Result.Success(new PaginationResult<MomentResult>(results, total));
    }
}

public class PageMomentClientQueryHandler(
    IMapper mapper,
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Moment> momentRepo,
    IBaseDefaultRepository<MomentLike> momentLikeRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<PageMomentClientQuery, Result>
{
    public async Task<Result> Handle(PageMomentClientQuery request, CancellationToken cancellationToken)
    {
        var visitorId = currentUserProvider.GetCurrentVisitor();

        var moments = await momentRepo.Select
            .Include(m => m.Announcer)
            .Where(m => m.Showable)
            .OrderByDescending(m => m.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var momentIdIds = moments.Select(m => m.MomentId).Distinct().ToList();
        var likes = await momentLikeRepo.Select
            .Where(ml => momentIdIds.Contains(ml.MomentId))
            .ToListAsync(cancellationToken);

        var results = new List<MomentClientResult>();
        foreach (var moment in moments)
        {
            var comments = await commentRepo.Select
               .Where(m => m.Showable)
               .Where(c => c.CommentType == BelongType.Moment)
               .Where(c => c.BelongId == moment.MomentId)
               .CountAsync(cancellationToken);

            var result = mapper.Map<MomentClientResult>(moment);
            result.Announcer = mapper.Map<MomentAnnouncerResult>(moment.Announcer);
            result.Comments = (int)comments;
            var momentLikes = likes.Where(l => l.MomentId == moment.MomentId).ToList();
            result.Likes = momentLikes.Count;
            result.IsLike = momentLikes.Any(l => l.VisitorId == visitorId); // 访客是否已点过赞
            results.Add(result);
        }

        return Result.Success(new PaginationResult<MomentClientResult>(results, total));
    }
}
