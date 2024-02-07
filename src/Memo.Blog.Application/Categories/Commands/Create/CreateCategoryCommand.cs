namespace Memo.Blog.Application.Categories.Commands.Create;

[Authorize(Permissions = ApiPermission.Category.Create)]
public record CreateCategoryCommand(
    string Name
    ) : IRequest<Result>;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator(
        IBaseDefaultRepository<Category> categoryResp
        )
    {
        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(20)
            .WithMessage("分类名称长度在1-20个字符之间");

        RuleFor(x => x.Name)
            .MustAsync(async (x, ct) => !await categoryResp.Select.AnyAsync(u => x == u.Name, ct))
            .WithMessage("分类已存在");
    }
}
