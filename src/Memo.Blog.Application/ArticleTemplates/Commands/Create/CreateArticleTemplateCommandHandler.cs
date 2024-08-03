namespace Memo.Blog.Application.ArticleTemplates.Commands.Create;

public class CreateArticleTemplateCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<ArticleTemplate> articleTemplateRepo
    ) : IRequestHandler<CreateArticleTemplateCommand, Result>
{
    public async Task<Result> Handle(CreateArticleTemplateCommand request, CancellationToken cancellationToken)
    {
        var exist = await articleTemplateRepo.Select.AnyAsync(c => request.Name == c.Name, cancellationToken);
        if (exist) throw new ApplicationException("同名模板已存在");

        var template = mapper.Map<ArticleTemplate>(request);
        template = await articleTemplateRepo.InsertAsync(template, cancellationToken);

        return template == null || template.Id == 0 ? throw new ApplicationException("保存模板失败") : Result.Success(template.TemplateId);
    }
}
