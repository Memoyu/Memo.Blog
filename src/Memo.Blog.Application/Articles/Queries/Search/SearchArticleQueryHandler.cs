using Memo.Blog.Application.Articles.Common;
using Memo.Blog.Application.Common.Text;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Driver;

namespace Memo.Blog.Application.Articles.Queries.Get;

public class SearchArticleQueryHandler(
    ISegmenterService segmenterService,
    IBaseDefaultRepository<Article> articleRepo,
    IBaseMongoRepository<ArticleCollection> articleMongoRepo
    ) : IRequestHandler<SearchArticleQuery, Result>
{
    public async Task<Result> Handle(SearchArticleQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.KeyWord)) return Result.Success();

        // 搜索的关键词进行分词
        var keyWordSegs = segmenterService.CutForSearch(request.KeyWord);

        var f = Builders<ArticleCollection>.Filter.Text(string.Join(" ", keyWordSegs));
        f &= Builders<ArticleCollection>.Filter.Eq(u => u.Status, ArticleStatus.Published);
        var sort = Builders<ArticleCollection>.Sort.Descending(x => x.CreateTime);

        var total = await articleMongoRepo.CountAsync(f, cancellationToken: cancellationToken);
        var searchResults = await articleMongoRepo.FindListByPageAsync(f, request.Page, request.Size, sort: sort, cancellationToken: cancellationToken);



        var dtos = new List<SearchArticleResult>();
        if (searchResults.Count != 0)
        {
            var articleIds = searchResults.Select(a => a.ArticleId).ToList();
            var articles = await articleRepo.Select.Where(a => articleIds.Contains(a.ArticleId)).ToListAsync(a => new { a.ArticleId, a.Title, a.Description }, cancellationToken);

            // TODO: 完善响应数据
            foreach (var result in searchResults)
            {
                // var index = result.Content.IndexOf(keyWordSegs);
                var article = articles.FirstOrDefault(a => a.ArticleId == result.ArticleId);
                dtos.Add(new SearchArticleResult
                {
                    ArticleId = result.ArticleId,
                    Title = article?.Title ?? result.Title,
                    Description = article?.Description ?? result.Description,
                    Category = result.Category,
                    Content = result.Content.Replace(" ", ""),
                });
            }
        }

        var page = new SearchArticlePageResult(dtos, total)
        {
            KeyWordSegs = keyWordSegs.ToList()
        };
        return Result.Success(page);
    }
}
