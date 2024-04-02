namespace Memo.Blog.Application.Tags.Commands.Create;

[Authorize(Permissions = ApiPermission.Tag.Create)]
public record CreateTagCommand(
    string Name,
    string Color
    ) : IAuthorizeableRequest<Result>;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("标签名称不能为空");

        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(10)
            .WithMessage("标签名称长度在1-10个字符之间");


        RuleFor(x => x.Color)
            .NotEmpty()
            .WithMessage("标签颜色不能为空");

        RuleFor(x => x.Color)
            .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$")
            .WithMessage("标签颜色格式不正确");
    }
}
