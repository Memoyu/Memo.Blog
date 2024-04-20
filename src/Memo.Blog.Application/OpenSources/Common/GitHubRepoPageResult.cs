﻿namespace Memo.Blog.Application.OpenSources.Common;
public class GitHubRepoPageResult
{
    public int Id { get; set; }

    public bool Private { get; set; }

    public string Name { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string HtmlUrl { get; set; } = string.Empty;

    public List<string> Topics { get; set; } = [];

    public DateTime CreatedAt { get; set; }
}
