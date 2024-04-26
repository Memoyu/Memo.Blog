using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Moments.Common;

namespace Memo.Blog.Application.Moments.Queries.Page;

public class PageMomentQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo
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
            .WhereIf(!string.IsNullOrWhiteSpace(request.Content), m => m.Content.Contains(request.Content!))
            .WhereIf(request.DateBegin.HasValue && request.DateEnd.HasValue, m => m.CreateTime <= request.DateEnd && m.CreateTime >= request.DateBegin)
            .OrderByDescending(m => m.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var results = mapper.Map<List<MomentResult>>(moments);

        return Result.Success(new PaginationResult<MomentResult>(results, total));
    }
}

public class PageMomentClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Moment> momentRepo,
    IBaseDefaultRepository<User> userRepo,
    IBaseDefaultRepository<Comment> commentRepo
    ) : IRequestHandler<PageMomentClientQuery, Result>
{
    public async Task<Result> Handle(PageMomentClientQuery request, CancellationToken cancellationToken)
    {
        var moments = await momentRepo.Select
            .Where(m => m.Showable)
            .OrderByDescending(m => m.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var userIds = moments.Select(m => m.CreateUserId).Distinct().ToList();
        var announcers = await userRepo.Select
         .Where(u => userIds.Contains(u.UserId))
         .ToListAsync(cancellationToken);


        var results = new List<MomentClientResult>();
        foreach (var moment in moments)
        {
            var comments = await commentRepo.Select
               .Where(m => m.Showable)
               .Where(c => c.CommentType == Domain.Enums.CommentType.Moment)
               .Where(c => c.BelongId == moment.MomentId)
               .CountAsync(cancellationToken);

            var result = mapper.Map<MomentClientResult>(moment);
            result.Announcer = mapper.Map<MomentAnnouncerResult>(announcers.FirstOrDefault(a => a.UserId == moment.CreateUserId) ?? new());
            result.Comments = (int)comments;
            results.Add(result);
        }

        return Result.Success(new PaginationResult<MomentClientResult>(results, total));
    }
}
