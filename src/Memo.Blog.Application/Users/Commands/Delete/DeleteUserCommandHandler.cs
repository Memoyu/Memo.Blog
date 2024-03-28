namespace Memo.Blog.Application.Users.Commands.Delete;

public class DeleteUserCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<User> userRepo
    ) : IRequestHandler<DeleteUserCommand, Result>
{
    public Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
