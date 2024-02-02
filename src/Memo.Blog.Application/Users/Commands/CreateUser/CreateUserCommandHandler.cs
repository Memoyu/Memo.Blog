using Memo.Blog.Application.Users.Common;

namespace Memo.Blog.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserResult>>
{
    public async Task<Result<UserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return Result.Success(new UserResult());
    }
}
