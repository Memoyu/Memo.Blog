namespace Memo.Blog.Application.ArticleTemplates.Commands.Delete;

public class DeleteArticleTemplateCommandHandler(
    IBaseDefaultRepository<ArticleTemplate> articleTemplateRepo
    ) : IRequestHandler<DeleteArticleTemplateCommand, Result>
{
    public async Task<Result> Handle(DeleteArticleTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await articleTemplateRepo.Select.Where(c => c.TemplateId == request.TemplateId).FirstAsync(cancellationToken) 
            ?? throw new ApplicationException("模板不存在");

        var affrows = await articleTemplateRepo.DeleteAsync(template, cancellationToken);

        return affrows > 0 ? Result.Success() : throw new ApplicationException("删除模板失败");
    }
}
