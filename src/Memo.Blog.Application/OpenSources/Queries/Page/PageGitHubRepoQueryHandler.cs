using Memo.Blog.Application.OpenSources.Common;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Memo.Blog.Application.OpenSources.Queries.Page;

public class PageGitHubRepoQueryHandler(
    IMapper mapper,
    IBaseMongoRepository<GitHubRepoCollection> githubRepo
    ) : IRequestHandler<PageGitHubRepoQuery, Result>
{
    public async Task<Result> Handle(PageGitHubRepoQuery request, CancellationToken cancellationToken)
    {
        var f = Builders<GitHubRepoCollection>.Filter.Empty;
        if (!string.IsNullOrWhiteSpace(request.KeyWord))
        {
            f = Builders<GitHubRepoCollection>.Filter.Or(
            Builders<GitHubRepoCollection>.Filter.Regex(u => u.Name, new BsonRegularExpression(request.KeyWord, "i")),
            Builders<GitHubRepoCollection>.Filter.Regex(u => u.FullName, new BsonRegularExpression(request.KeyWord, "i")),
            Builders<GitHubRepoCollection>.Filter.Regex(u => u.Description, new BsonRegularExpression(request.KeyWord, "i")),
            Builders<GitHubRepoCollection>.Filter.AnyIn(u => u.Topics, new List<string> { request.KeyWord })
            );
        }

        var sort = Builders<GitHubRepoCollection>.Sort.Descending(x => x.CreatedAt);

        var total = await githubRepo.CountAsync(f, cancellationToken: cancellationToken);
        var repos = await githubRepo.FindListByPageAsync(f, request.Page, request.Size, sort: sort, cancellationToken: cancellationToken);

        var results = mapper.Map<List<GitHubRepoPageResult>>(repos);

        return Result.Success(new PaginationResult<GitHubRepoPageResult>(results, total));
    }
}

