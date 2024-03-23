using Memo.Blog.Application.Comments.Common;

namespace Memo.Blog.Application.Common.Mappings;

public class CommentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Comment, PageCommentResult>()
            .Map(d => d.Belong, s => GetCommentBelog(s));

        config.ForType<Comment, CommentResult>()
            .Map(d => d.Belong, s => GetCommentBelog(s));
    }

    private CommentBelongResult GetCommentBelog(Comment s)
    {
        var belong = new CommentBelongResult();
        switch (s.CommentType)
        {
            case Domain.Enums.CommentType.Article:
                belong = MapContext.Current.GetService<IBaseDefaultRepository<Article>>().Select
                    .Where(c => c.ArticleId == s.BelongId)
                    .ToOne(a => new CommentBelongResult { BelongId = s.BelongId, Title = a.Title, Link = "" });
                break;
            case Domain.Enums.CommentType.Moment:
                belong = new CommentBelongResult { BelongId = s.BelongId, Title = "动态", Link = "" };
                break;
            case Domain.Enums.CommentType.About:
                belong = new CommentBelongResult { BelongId = s.BelongId, Title = "关于", Link = "" };
                break;
        }

        return belong; 
    }
}
