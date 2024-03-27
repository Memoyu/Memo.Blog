﻿using Memo.Blog.Domain.Events.Articles;

namespace Memo.Blog.Application.Articles.Commands.Delete;

public class DeleteArticleCommandHandler(
    IBaseDefaultRepository<Article> articleResp
    ) : IRequestHandler<DeleteArticleCommand, Result>
{
    public async Task<Result> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await articleResp.Select.Where(t => t.ArticleId == request.ArticleId).FirstAsync(cancellationToken);
        if (article == null) throw new ApplicationException("文章不存在");

        article.AddDomainEvent(new ArticleDeleteEvent(article.ArticleId));

        var rows = await articleResp.DeleteAsync(article, cancellationToken);

        return rows > 0 ? Result.Success() : throw new ApplicationException("删除文章失败");
    }
}

