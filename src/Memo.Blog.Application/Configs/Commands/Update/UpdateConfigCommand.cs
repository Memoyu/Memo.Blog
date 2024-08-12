namespace Memo.Blog.Application.Configs.Commands.Update;

[Authorize(Permissions = ApiPermission.Config.Update)]
public record UpdateConfigCommand(long CategoryId, string Name) : IAuthorizeableRequest<Result>;

public class UpdateConfigCommandValidator : AbstractValidator<UpdateConfigCommand>
{
    public UpdateConfigCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .Must(x => x > 0)
            .WithMessage("分类Id必须大于0");
    }
}

