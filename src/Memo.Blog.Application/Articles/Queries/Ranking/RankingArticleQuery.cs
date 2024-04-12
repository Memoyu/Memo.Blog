namespace Memo.Blog.Application.Articles.Queries.Ranking;

/// <summary>
/// 获取文章排名列表
/// </summary>
/// <param name="Quota">名额</param>
[Authorize(Permissions = ApiPermission.Article.Ranking)]
public record RankingArticleQuery(int Quota) : IAuthorizeableRequest<Result>;
