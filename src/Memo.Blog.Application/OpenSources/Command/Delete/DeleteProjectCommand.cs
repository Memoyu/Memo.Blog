namespace Memo.Blog.Application.OpenSources.Commands.Delete;

[Authorize(Permissions = ApiPermission.OpenSource.Delete)]
[Transactional]
public record DeleteProjectCommand(long CategoryId) : IAuthorizeableRequest<Result>;

public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .Must(x => x > 0)
            .WithMessage("分类Id必须大于0");

        RuleFor(x => x.CategoryId)
            .Must(x => x != InitConst.InitCategoryId)
            .WithMessage("初始分类无法删除");
    }
}

