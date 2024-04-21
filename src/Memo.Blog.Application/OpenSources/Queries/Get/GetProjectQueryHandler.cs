using Memo.Blog.Application.OpenSources.Common;
using Memo.Blog.Application.OpenSources.Queries.Get;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace Memo.Blog.Application.OpenSources.Queries.List;

public class GetProjectQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<OpenSource> openSourceRepo,
    IBaseMongoRepository<GitHubRepoCollection> githubRepo
    ) : IRequestHandler<GetProjectQuery, Result>
{
    public async Task<Result> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await openSourceRepo.Select.Where(p => p.ProjectId == request.ProjectId).FirstAsync(cancellationToken) ?? throw new ApplicationException("开源项目不存在");

        var result = mapper.Map<OpenSourceResult>(project);
        if (project.RepoId.HasValue)
        {
            var f = Builders<GitHubRepoCollection>.Filter.Empty;
            f = Builders<GitHubRepoCollection>.Filter.Eq(u => u.Id, project.RepoId.Value);

            var repo = (await githubRepo.FindListAsync(f, cancellationToken: cancellationToken))?.FirstOrDefault();
            if (repo != null)
            {
                result.Star = repo.StargazersCount;
                result.Fork = repo.ForksCount;
                result.HtmlUrl = repo.HtmlUrl;
                result.RepoFullName = repo.FullName;
            }
        }

        return Result.Success(result);
    }
}

public class GetProjectClientQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<OpenSource> openSourceRepo,
    IBaseMongoRepository<GitHubRepoCollection> githubRepo
    ) : IRequestHandler<GetProjectClientQuery, Result>
{
    public async Task<Result> Handle(GetProjectClientQuery request, CancellationToken cancellationToken)
    {
        var project = await openSourceRepo.Select.Where(p => p.ProjectId == request.ProjectId).FirstAsync(cancellationToken) ?? throw new ApplicationException("开源项目不存在");

        var result = mapper.Map<OpenSourceClientResult>(project);
        if (project.RepoId.HasValue)
        {
            var f = Builders<GitHubRepoCollection>.Filter.Empty;
            f = Builders<GitHubRepoCollection>.Filter.Eq(u => u.Id, project.RepoId.Value);

            var repo = (await githubRepo.FindListAsync(f, cancellationToken: cancellationToken))?.FirstOrDefault();
            if (repo != null)
            {
                result.Star = repo.StargazersCount;
                result.Fork = repo.ForksCount;
                result.HtmlUrl = repo.HtmlUrl;
            }
        }

        return Result.Success(result);
    }
}
