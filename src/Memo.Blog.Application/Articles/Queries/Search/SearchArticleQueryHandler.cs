using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Queries.Get;

public class SearchArticleQueryHandler(
    IMapper mapper,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo
    ) : IRequestHandler<SearchArticleQuery, Result>
{
    public async Task<Result> Handle(SearchArticleQuery request, CancellationToken cancellationToken)
    {
        var f = Builders<ArticleCollection>.Filter.Empty;
        f &= Builders<ArticleCollection>.Filter.Text(request.KeyWord);

        var searchs = await articleMongoRepo.FindListByPageAsync(f, request.Page, request.Size, sort: null, cancellationToken: cancellationToken);

        return Result.Success(searchs);
    }
}
