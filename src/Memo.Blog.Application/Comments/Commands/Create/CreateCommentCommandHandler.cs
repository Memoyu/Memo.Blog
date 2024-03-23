using Memo.Blog.Application.Common.Interfaces.Region;
using Memo.Blog.Application.Security;
using Memo.Blog.Domain.Entities.Mongo;
using Memo.Blog.Domain.Events.Articles;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Comments.Commands.Create;

public class CreateCommentCommandHandler(
    ILogger<CreateCommentCommandHandler> logger,
    IMapper mapper,
    ICurrentUserProvider currentUserProvider,
    IRegionSearcher searcher,
    IBaseDefaultRepository<Comment> commentRepo,
    IBaseDefaultRepository<Article> articleRepo,
     IBaseMongoRepository<ArticleCollection> articleMongoResp
    ) : IRequestHandler<CreateCommentCommand, Result>
{
    public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var isArticleComment = false;
        if (request.CommentType == Domain.Enums.CommentType.Article)
        {
            var article = await articleRepo.Select.Where(a => a.ArticleId == request.BelongId).FirstAsync(cancellationToken);
            if (article == null) return Result.Failure("评论文章不存在");
            isArticleComment = true;
        }

        var comment = mapper.Map<Comment>(request);
        // 如果是文章评论，则需要更新mongodb数据
        if (isArticleComment)
        {
            comment.AddDomainEvent(new ArticleUpdateCommentEvent(request.BelongId));
        }

        var ip = currentUserProvider.GetClientIp();
        comment.Ip = ip;
        var region = searcher.Search(ip);
        comment.Region = region.GetRegion();
        comment = await commentRepo.InsertAsync(comment, cancellationToken);
        if (comment.Id == 0) throw new Exception("保存评论失败");

        return Result.Success(comment.CommentId);
    }
}
