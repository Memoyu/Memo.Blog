namespace Memo.Blog.Application.Tags.Commands.Update;

[Authorize(Permissions = ApiPermission.Tag.Update)]
[Transactional]
public record UpdateTagCommand(long TagId, string Name, string Color) : IRequest<Result>;

public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(x => x.TagId)
            .Must(x => x > 0)
            .WithMessage("标签Id必须大于0");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("分类名称不能为空");

        RuleFor(x => x.Name)
           .MinimumLength(1)
           .MaximumLength(10)
           .WithMessage("标签名称长度在1-10个字符之间");
    }
}

