using System.Text.Json.Serialization;

namespace Memo.Blog.Application.Common.Models.GitHub;

public class GitHubRepoReadmeResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("sha")]
    public string Sha { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("git_url")]
    public string GitUrl { get; set; }

    [JsonPropertyName("download_url")]
    public string DownloadUrl { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("encoding")]
    public string Encoding { get; set; }

    [JsonPropertyName("_links")]
    public GitHubRepoReadmeLink Links { get; set; }

}

public class GitHubRepoReadmeLink
{
    [JsonPropertyName("self")]
    public string Self { get; set; }

    [JsonPropertyName("git")]
    public string Git { get; set; }

    [JsonPropertyName("html")]
    public string Html { get; set; }

}

