using Memo.Blog.Application.Common.Interfaces.Services.GitHubs;
using Memo.Blog.Application.Common.Models.GitHub;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.OpenSources;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Memo.Blog.Application.OpenSources.Events;

public class SyncGitHubRepoEventHandler(
    IMapper mapper,
    ILogger<SyncGitHubRepoEventHandler> logger,
    IGitHubRestApiService githubRestApiService,
    IBaseMongoRepository<GitHubRepoCollection> githubRepo,
    IBaseDefaultRepository<OpenSource> openSourceRepo
    ) : INotificationHandler<SyncGitHubRepoEvent>
{
    public async Task Handle(SyncGitHubRepoEvent notification, CancellationToken cancellationToken)
    {
        var githubRepos = new List<GitHubRepoResponse>();
        try
        {
            githubRepos = await githubRestApiService.GetAllReposAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "同步GitHub仓库数据失败：获取仓库数据异常");
        }

        if (githubRepos == null || githubRepos.Count <= 0) return;

        var repos = mapper.Map<List<GitHubRepoCollection>>(githubRepos);

        var f = Builders<GitHubRepoCollection>.Filter.Empty;
        var currentRepos = await githubRepo.FindListAsync(f, null, null, cancellationToken);

        var adds = new List<GitHubRepoCollection>();
        var updates = new List<GitHubRepoCollection>();

        foreach (var repo in repos)
        {
            var currentRepo = currentRepos.FirstOrDefault(cr => cr.Id == repo.Id);
            if (currentRepo == null)
            {
                adds.Add(repo);
            }
            else
            {
                updates.Add(repo);
                currentRepos.Remove(currentRepo);
            }
        }

        // 添加仓库
        if (adds.Count != 0)
            await githubRepo.InsertManyAsync(adds, null, cancellationToken);

        // 更新仓库
        if (updates.Count != 0)
        {
            foreach (var update in updates)
            {
                var updateFilter = Builders<GitHubRepoCollection>.Filter.Eq(b => b.Id, update.Id);
                await githubRepo.ReplaceOneAsync(update, updateFilter, null, cancellationToken);

                // 更新已添加到开源列表的项目数据
                var project = await openSourceRepo.Select.Where(o => o.RepoId == update.Id).FirstAsync(cancellationToken);
                if (project is null) continue;
                // 前端可调整，则不进行自动更新
                // project.ReadmeUrl = $"https://raw.githubusercontent.com/{update.FullName}/{update.DefaultBranch}/README.md";
                project.HtmlUrl = update.HtmlUrl;
                project.Star = update.StargazersCount;
                project.Fork = update.ForksCount;
            }
        }

        // 删除不存在的仓库
        if (currentRepos.Count != 0)
        {
            var deleteIds = currentRepos.Select(c => c.Id);
            var deleteFilter = Builders<GitHubRepoCollection>.Filter.In(b => b.Id, deleteIds);

            await githubRepo.DeleteManyAsync(deleteFilter, null, cancellationToken);
        }
    }
}
