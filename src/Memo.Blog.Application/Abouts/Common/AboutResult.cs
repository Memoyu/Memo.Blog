namespace Memo.Blog.Application.Abouts.Common;
public record AboutResult(
    string Title,
    string Tags,
    string Content,
    bool Commentable);
