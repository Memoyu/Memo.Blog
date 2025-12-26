using Memo.Blog.Application.Configs.Common;

namespace Memo.Blog.Application.Configs.Commands.Update;

[Authorize(Permissions = ApiPermission.Config.Update)]
public record UpdateConfigCommand( BannerConfig Banner, ColorConfig Color) : IAuthorizeableRequest<Result>;

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

[Authorize(Permissions = ApiPermission.Config.Update)]
public record UpdateConfigBannerCommand(BannerConfig Banner) : IAuthorizeableRequest<Result>;

public class UpdateConfigBannerCommandValidator : AbstractValidator<UpdateConfigBannerCommand>
{
    public UpdateConfigBannerCommandValidator()
    {
        RuleFor(x => x.Banner)
          .NotEmpty()
          .WithMessage("头图配置不能为空");
    }
}



[Authorize(Permissions = ApiPermission.Config.UpdateVisitor)]
public record UpdateVisitorConfigCommand(long VisitorId) : IAuthorizeableRequest<Result>;

public class UpdateVisitorConfigCommandValidator : AbstractValidator<UpdateVisitorConfigCommand>
{
    public UpdateVisitorConfigCommandValidator()
    {
        RuleFor(x => x.VisitorId)
          .GreaterThan(0)
          .WithMessage("访客Id不能为空");
    }
}
