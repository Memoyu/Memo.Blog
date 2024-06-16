namespace Memo.Blog.Application.Messages.Commands.Update;

[Authorize(Permissions = ApiPermission.Message.Read)]
public record ReadMessageCommand(List<long> MessageIds) : IAuthorizeableRequest<Result>;

public class ReadMessageCommandValidator : AbstractValidator<ReadMessageCommand>
{
    public ReadMessageCommandValidator()
    {
        RuleFor(x => x.MessageIds)
           .NotEmpty()
           .WithMessage("消息Id不能为空");
    }
}
