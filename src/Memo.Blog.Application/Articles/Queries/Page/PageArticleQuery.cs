﻿using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Articles.Queries.Page;

[Authorize(Permissions = ApiPermission.Article.Page)]
public record PageArticleQuery : PaginationQuery, IAuthorizeableRequest<Result>
{
    public string? Title { get; set; }

    public long? CategoryId { get; set; }

    public List<long>? TagIds { get; set; }

    public ArticleStatus? Status { get; set; }
}

public class PageArticleQueryValidator : AbstractValidator<PageArticleQuery>
{
    public PageArticleQueryValidator()
    {
    }
}

public record PageArticleClientQuery : PaginationQuery, IRequest<Result>
{
    public long? CategoryId { get; set; }
}
