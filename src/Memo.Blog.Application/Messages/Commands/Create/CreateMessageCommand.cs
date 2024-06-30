namespace Memo.Blog.Application.Messages.Commands.Create;

[Authorize(Permissions = ApiPermission.Message.Create)]
public record CreateMessageCommand(
    List<long> ToUsers,
    List<long> ToRoles,
    string Content
    ) : IAuthorizeableRequest<Result>;

public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {

    }
}
