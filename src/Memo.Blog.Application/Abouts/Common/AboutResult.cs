namespace Memo.Blog.Application.Abouts.Common;
public record AboutResult(
    string Title,
    string Banner,
    List<string> Tags,
    string Content,
    bool Commentable);
