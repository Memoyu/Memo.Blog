using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Domain.Entities.Mongo;

namespace Memo.Blog.Application.Articles.Commands.CreateArticle;

public class CreateArticleCommandHandler(
    IMapper _mapper,
    IBaseDefaultRepository<Article> _articleResp,
    IBaseMongoRepository<ArticleCollection> _articleMongoResp
    ) : IRequestHandler<CreateArticleCommand, Result>
{
    public async Task<Result> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = _mapper.Map<Article>(request);
        article = await _articleResp.InsertAsync(article);

        var articleCollection = _mapper.Map<ArticleCollection>(article);
        await _articleMongoResp.InsertOneAsync(articleCollection);

        var result = _mapper.Map<ArticleResult>(article);

        return Result.Success(result);
    }
}
