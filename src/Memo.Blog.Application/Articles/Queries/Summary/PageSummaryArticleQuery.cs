﻿using Memo.Blog.Application.Articles.Queries.Page;

namespace Memo.Blog.Application.Articles.Queries.Summary;

[Authorize(Permissions = ApiPermission.Article.SummaryPage)]
public record PageSummaryArticleQuery : PageArticleQuery, IAuthorizeableRequest<Result>;

public class ListSummaryArticleQueryValidator : AbstractValidator<PageSummaryArticleQuery>
{
    public ListSummaryArticleQueryValidator()
    {
    }
}