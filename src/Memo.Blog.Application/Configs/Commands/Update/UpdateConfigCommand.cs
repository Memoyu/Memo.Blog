using Memo.Blog.Application.Configs.Common;

namespace Memo.Blog.Application.Configs.Commands.Update;

[Authorize(Permissions = ApiPermission.Config.Update)]
public record UpdateConfigCommand(AdminConfig Admin, BannerConfig Banner, ColorConfig Color) : IAuthorizeableRequest<Result>;

public class UpdateConfigCommandValidator : AbstractValidator<UpdateConfigCommand>
{
    public UpdateConfigCommandValidator()
    {
        RuleFor(x => x.Banner)
          .NotEmpty()
          .WithMessage("头图配置不能为空");

        RuleFor(x => x.Color)
        .Must(x => x != null && x.Primary.Count > 0)
        .WithMessage("主要颜色不能为空");

        RuleFor(x => x.Color)
         .Must(x => x != null && x.Secondary.Count > 0)
        .WithMessage("次要颜色不能为空");

        RuleFor(x => x.Color)
          .Must(x => x != null && x.Tertiary.Count > 0)
        .WithMessage("第三颜色不能为空");
    }
}

