using Memo.Blog.Application.Common.Text;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Queries.Get;

public class SearchArticleQueryHandler(
    IMapper mapper,
    ISegmenterService segmenterService,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo
    ) : IRequestHandler<SearchArticleQuery, Result>
{
    public async Task<Result> Handle(SearchArticleQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.KeyWord)) return Result.Success();

        // 搜索的关键词进行分词
        var searchKeyWord = segmenterService.CutWithSplitForSearch(request.KeyWord);

        var f = Builders<ArticleCollection>.Filter.Empty;
        f &= Builders<ArticleCollection>.Filter.Text(searchKeyWord);

        var searchs = await articleMongoRepo.FindListByPageAsync(f, request.Page, request.Size, sort: null, cancellationToken: cancellationToken);

        return Result.Success(searchs);
    }
}
