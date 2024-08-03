namespace Memo.Blog.Application.ArticleTemplates.Commands.Update;

public class UpdateCategoryCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<ArticleTemplate> articleTemplateRepo
    ) : IRequestHandler<UpdateArticleTemplateCommand, Result>
{
    public async Task<Result> Handle(UpdateArticleTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await articleTemplateRepo.Select.Where(c => c.TemplateId == request.TemplateId).FirstAsync(cancellationToken) 
            ?? throw new ApplicationException("模板不存在");

        var exist = await articleTemplateRepo.Select.AnyAsync(c => c.TemplateId != request.TemplateId && request.Name == c.Name, cancellationToken);
        if (exist) throw new ApplicationException("模板名已存在");

        var entity = mapper.Map<ArticleTemplate>(request);
        entity.Id = template.Id;
        entity.TemplateId = template.TemplateId;
        var affrows = await articleTemplateRepo.UpdateAsync(entity, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("更新模板失败");
    }
}
