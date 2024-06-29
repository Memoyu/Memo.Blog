using Memo.Blog.Domain.Enums;

namespace Memo.Blog.Application.Messages.Commands.Create;

[Authorize(Permissions = ApiPermission.Message.Create)]
public record CreateMessageCommand(
    long ToId,
    string Content
    ) : IAuthorizeableRequest<Result>;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {

    }
}
