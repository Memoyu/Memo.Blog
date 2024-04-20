using Memo.Blog.Application.Common.Models.GitHub;

namespace Memo.Blog.Application.Common.Interfaces.Services.GitHubs;

public interface IGitHubRestApiService
{
    /// <summary>
    /// 获取owner仓库集合
    /// </summary>
    /// <returns></returns>
    Task<List<GitHubRepoResponse>> GetAllReposAsync();
}
