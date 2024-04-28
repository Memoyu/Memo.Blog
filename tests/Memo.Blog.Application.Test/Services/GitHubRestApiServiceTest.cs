using System.Text;
using Memo.Blog.Application.Common.Interfaces.Services.GitHubs;
using Memo.Blog.Application.Common.Models.Settings;
using Memo.Blog.Application.Common.Services.GitHubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Memo.Blog.Application.Test.Services;

public class GitHubRestApiServiceTest
{
    private readonly IGitHubRestApiService _gitHubRestApiService;

    public GitHubRestApiServiceTest()
    {
        var settings = new Dictionary<string, string>
        {
            ["Authorization:GitHub:Owner"] = "Memoyu",
            ["Authorization:GitHub:Token"] = "**"
        };

        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(settings!).Build();

        // 注册Http请求服务
        services.AddHttpClient();


        services.Configure<AuthorizationSettings>(configuration.GetSection("Authorization"));

        services.AddSingleton<IGitHubRestApiService, GitHubRestApiService>();
        var sp = services.BuildServiceProvider();

        _gitHubRestApiService = sp.GetRequiredService<IGitHubRestApiService>();
    }

    [Fact]
    public async void GetReposAsync_Should_Success()
    {
        var repos = await _gitHubRestApiService.GetAllReposAsync();
        Assert.NotNull(repos);
        Assert.True(repos.Count > 1);
    }

    [Fact]
    public async void GetRepoReadmeAsync_Should_Success()
    {
        var readme = await _gitHubRestApiService.GetRepoReadmeAsync("mbill_wechat_app");
        Assert.NotNull(readme);
        Assert.NotEmpty(readme.Content);

        var text = Encoding.UTF8.GetString(Convert.FromBase64String(readme.Content));

    }
}
