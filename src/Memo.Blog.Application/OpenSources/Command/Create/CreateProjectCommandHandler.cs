using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace Memo.Blog.Application.OpenSources.Commands.Create;

public class CreateProjectCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<OpenSource> openSourceRepo,
    IBaseMongoRepository<GitHubRepoCollection> githubRepo
    ) : IRequestHandler<CreateProjectCommand, Result>
{
    public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var exist = await openSourceRepo.Select.AnyAsync(c => request.Title == c.Title, cancellationToken);
        if (exist) throw new ApplicationException("同名项目已存在");

        var entity = mapper.Map<OpenSource>(request);
        if (request.RepoId.HasValue)
        {
            var f = Builders<GitHubRepoCollection>.Filter.Empty;
            f = Builders<GitHubRepoCollection>.Filter.Eq(u => u.Id, request.RepoId.Value);

            var repo = (await githubRepo.FindListAsync(f, cancellationToken: cancellationToken))?.FirstOrDefault() ?? throw new ApplicationException("指定的GitHub源项目不存在");
            entity.Star = repo.StargazersCount;
            entity.Fork = repo.ForksCount;
            entity.HtmlUrl = repo.HtmlUrl;
        }

        entity = await openSourceRepo.InsertAsync(entity, cancellationToken);
        return entity == null || entity.Id == 0 ? throw new ApplicationException("保存开源项目失败") : Result.Success(entity.ProjectId);
    }
}
