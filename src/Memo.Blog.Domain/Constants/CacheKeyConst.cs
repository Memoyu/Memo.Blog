namespace Memo.Blog.Domain.Constants;

public static class CacheKeyConst
{
    /// <summary>
    /// 文章浏览记录缓存key
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="visitorId"></param>
    /// <returns></returns>
    public static string ArticleView(long articleId, long visitorId) => $"memo-blog:article:views:{articleId}:{visitorId}";

    /// <summary>
    /// 友链访问记录缓存key
    /// </summary>
    /// <param name="friendId"></param>
    /// <param name="visitorId"></param>
    /// <returns></returns>
    public static string FriendView(long friendId, long visitorId) => $"memo-blog:friend:views:{friendId}:{visitorId}";
}
