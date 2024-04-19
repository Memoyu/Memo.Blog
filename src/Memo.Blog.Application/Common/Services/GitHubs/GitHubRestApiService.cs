﻿using System.Text.Json;
using Memo.Blog.Application.Common.Interfaces.Services.GitHubs;
using Memo.Blog.Application.Common.Models.GitHub;
using Memo.Blog.Application.Common.Models.Settings;
using Microsoft.Extensions.Options;

namespace Memo.Blog.Application.Common.Services.GitHubs;

public class GitHubRestApiService : IGitHubRestApiService
{
    private readonly HttpClient _client;
    private readonly GitHubOptions _options;

    public GitHubRestApiService(IOptionsMonitor<AuthorizationSettings> authOptions, IHttpClientFactory httpClientFactory)
    {
        var githubOptions = authOptions.CurrentValue?.GitHub ?? throw new Exception("未配置GitHub授权信息");
        if (string.IsNullOrWhiteSpace(githubOptions.Token)) throw new Exception("未配置GitHub授权信息");
        _options = githubOptions;

        _client = httpClientFactory.CreateClient();
        
        _client.BaseAddress = new Uri("https://api.github.com/");
        _client.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_options.Token}");
        _client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        _client.DefaultRequestHeaders.UserAgent.TryParseAdd("request"); //Set the User Agent to "request" 不然会报403
    }

    public async Task<List<GitHubRepoResponse>> GetReposAsync()
    {
        var resp = await _client.GetAsync($"users/{_options.Owner}/repos");
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<GitHubRepoResponse>>(json) ?? [];
    }
}
