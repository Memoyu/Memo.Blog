namespace Memo.Blog.Application.Configs.Commands.Update;

[Authorize(Permissions = ApiPermission.Config.Update)]
public record UpdateConfigCommand(string Banner, string Color) : IAuthorizeableRequest<Result>;

public class UpdateConfigCommandValidator : AbstractValidator<UpdateConfigCommand>
{
    public UpdateConfigCommandValidator()
    {
        RuleFor(x => x.Banner)
          .NotEmpty()
          .WithMessage("头图配置不能为空");

        RuleFor(x => x.Color)
         .NotEmpty()
         .WithMessage("颜色配置不能为空");
    }
}

