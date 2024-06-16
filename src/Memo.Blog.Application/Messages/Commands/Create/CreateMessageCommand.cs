namespace Memo.Blog.Application.Messages.Commands.Create;

[Authorize(Permissions = ApiPermission.Message.Create)]
public record CreateMessageCommand(
    ) : IAuthorizeableRequest<Result>;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {

    }
}
