namespace Memo.Blog.Application.Abouts.Common;
public record AboutResult(
    string Title,
    List<string> Tags,
    string Content,
    bool Commentable);
