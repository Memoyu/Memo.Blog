using Memo.Blog.Application.Tags.Common;

namespace Memo.Blog.Application.Tags.Queries.List;

public class ListTagQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Tag> tagRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo
    ) : IRequestHandler<ListTagQuery, Result>
{
    public async Task<Result> Handle(ListTagQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .OrderByDescending(t => t.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        var articleTags = await articleTagRepo.Select.ToListAsync(cancellationToken);
        var dtos = mapper.Map<List<TagWithArticleCountResult>>(tags);
        foreach (var tag in dtos)
        {
            var total = articleTags.Where(a => a.TagId == tag.TagId).Count();
            tag.Articles = total;
        }

        return Result.Success(dtos);
    }
}

public class ListTagClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Tag> tagRepo,
    IBaseDefaultRepository<ArticleTag> articleTagRepo
    ) : IRequestHandler<ListTagClientQuery, Result>
{
    public async Task<Result> Handle(ListTagClientQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagRepo.Select
            .WhereIf(!string.IsNullOrWhiteSpace(request.Name), t => t.Name.Contains(request.Name))
            .OrderByDescending(t => t.CreateTime)
            .ToListAsync(cancellationToken) ?? [];

        var articleTags = await articleTagRepo.Select.ToListAsync(cancellationToken);
        var dtos = mapper.Map<List<TagWithArticleCountResult>>(tags);
        foreach (var tag in dtos)
        {
            var total = articleTags.Where(a => a.TagId == tag.TagId).Count();
            tag.Articles = total;
        }

        return Result.Success(dtos);
    }
}
