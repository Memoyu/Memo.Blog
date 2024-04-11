using Memo.Blog.Application.Loggers.Common;
using Memo.Blog.Domain.Entities.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Memo.Blog.Application.Logger.Queries.Visit.Page;

public class PageLoggerVisitQueryHandler(
    IMapper mapper,
    IBaseMongoRepository<LoggerVisitCollection> visitLogMongoRepo
    ) : IRequestHandler<PageLoggerVisitQuery, Result>
{
    public async Task<Result> Handle(PageLoggerVisitQuery request, CancellationToken cancellationToken)
    {
        var f = Builders<LoggerVisitCollection>.Filter.Empty;
        if (request.VisitId.HasValue)
            f &= Builders<LoggerVisitCollection>.Filter.Eq(nameof(LoggerVisitCollection.VisitId), ObjectId.Parse(request.VisitId.Value.ToString()));

        if (request.VisitorId.HasValue)
            f &= Builders<LoggerVisitCollection>.Filter.Eq(nameof(LoggerVisitCollection.VisitorId), request.VisitorId.Value);

        if (!string.IsNullOrWhiteSpace(request.Path))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.Path, new BsonRegularExpression(request.Path, "i"));

        if (request.Behavior.HasValue)
            f &= Builders<LoggerVisitCollection>.Filter.Eq(nameof(LoggerVisitCollection.Behavior), request.Behavior.Value);

        if (request.VisitedId.HasValue)
            f &= Builders<LoggerVisitCollection>.Filter.Eq(nameof(LoggerVisitCollection.VisitedId), request.VisitedId.Value);

        if (!string.IsNullOrWhiteSpace(request.Ip))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.Ip, new BsonRegularExpression(request.Ip, "i"));

        if (!string.IsNullOrWhiteSpace(request.Country))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.Country, new BsonRegularExpression(request.Country, "i"));

        if (!string.IsNullOrWhiteSpace(request.Region))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.Region, new BsonRegularExpression(request.Region, "i"));

        if (!string.IsNullOrWhiteSpace(request.Province))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.Province, new BsonRegularExpression(request.Province, "i"));

        if (!string.IsNullOrWhiteSpace(request.City))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.City, new BsonRegularExpression(request.City, "i"));

        if (!string.IsNullOrWhiteSpace(request.Isp))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.Isp, new BsonRegularExpression(request.Isp, "i"));


        if (!string.IsNullOrWhiteSpace(request.Os))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.Os, new BsonRegularExpression(request.Os, "i"));

        if (!string.IsNullOrWhiteSpace(request.Browser))
            f &= Builders<LoggerVisitCollection>.Filter.Regex(u => u.Isp, new BsonRegularExpression(request.Browser, "i"));

        if (request.DateBegin.HasValue && request.DateEnd.HasValue)
        {
            f &= Builders<LoggerVisitCollection>.Filter.And(
                Builders<LoggerVisitCollection>.Filter.Gte(u => u.VisitDate, request.DateBegin.Value),
                Builders<LoggerVisitCollection>.Filter.Lte(u => u.VisitDate, request.DateEnd.Value)
                );
        }

        var sort = Builders<LoggerVisitCollection>.Sort.Descending(x => x.VisitDate);

        var total = await visitLogMongoRepo.CountAsync(f, cancellationToken: cancellationToken);
        var logs = await visitLogMongoRepo.FindListByPageAsync(f, request.Page, request.Size, sort: sort, cancellationToken: cancellationToken);

        var results = mapper.Map<List<LoggerVisitPageResult>>(logs);
        return Result.Success(new PaginationResult<LoggerVisitPageResult>(results, total));
    }
}
