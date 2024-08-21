namespace Memo.Blog.Application.Configs.Common;

public record ConfigAdminResult
{
    public AdminConfig Admin { get; set; } = new();

}
