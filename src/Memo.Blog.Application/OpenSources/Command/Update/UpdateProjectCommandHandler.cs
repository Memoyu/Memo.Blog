using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace Memo.Blog.Application.OpenSources.Commands.Update;

public class UpdateProjectCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<OpenSource> openSourceRepo,
    IBaseMongoRepository<GitHubRepoCollection> githubRepo
    ) : IRequestHandler<UpdateProjectCommand, Result>
{
    public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await openSourceRepo.Select.Where(p => request.ProjectId == p.ProjectId).FirstAsync(cancellationToken) ?? throw new ApplicationException("项目不存在");

        var update = mapper.Map<OpenSource>(request);
        update.Id = project.Id;
        update.ProjectId = project.ProjectId;
        update.Star = project.Star;
        update.Fork = project.Fork;

        if (request.RepoId.HasValue)
        {
            var f = Builders<GitHubRepoCollection>.Filter.Empty;
            f = Builders<GitHubRepoCollection>.Filter.Eq(u => u.Id, request.RepoId.Value);

            var repo = (await githubRepo.FindListAsync(f, cancellationToken: cancellationToken))?.FirstOrDefault() ?? throw new ApplicationException("指定的GitHub源项目不存在");
            update.Star = repo.StargazersCount;
            update.Fork = repo.ForksCount;
            update.HtmlUrl = repo.HtmlUrl;
        }

        var affrows = await openSourceRepo.UpdateAsync(update, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新项目失败");
    }
}
