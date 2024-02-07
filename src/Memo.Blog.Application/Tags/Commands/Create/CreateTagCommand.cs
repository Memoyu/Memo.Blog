namespace Memo.Blog.Application.Tags.Commands.Create;

public record CreateTagCommand(
    string Name,
    string Color
    ) : IRequest<Result>;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator(
        IBaseDefaultRepository<Tag> tagResp
        )
    {
        RuleFor(x => x.Name)
            .MinimumLength(1)
            .MaximumLength(20)
            .WithMessage("标签名称长度在1-20个字符之间");

        RuleFor(x => x.Name)
            .MustAsync(async (x, ct) => !await tagResp.Select.AnyAsync(u => x == u.Name, ct))
            .WithMessage("标签已存在");

        RuleFor(x => x.Color)
            .NotEmpty()
            .WithMessage("标签颜色不能为空");

        RuleFor(x => x.Color)
            .Matches("^#(?:[0-9a-fA-F]{3}){1,2}$")
            .WithMessage("标签颜色格式不正确");
    }
}
