using Memo.Blog.Application.ArticleTemplates.Common;

namespace Memo.Blog.Application.ArticleTemplates.Queries.Get;

public class GetArticleTemplateQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<ArticleTemplate> articleTemplateRepo
    ) : IRequestHandler<GetArticleTemplateQuery, Result>
{
    public async Task<Result> Handle(GetArticleTemplateQuery request, CancellationToken cancellationToken)
    {
        var template = await articleTemplateRepo.Select.Where(t => t.TemplateId == request.TemplateId).FirstAsync(cancellationToken);
        return template is null 
            ? throw new ApplicationException("模板不存在") 
            : (Result)Result.Success(mapper.Map<ArticleTemplateResult>(template));
    }
}
