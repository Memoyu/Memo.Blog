namespace Memo.Blog.Application.Users.Commands.Update;

public class UpdateUserCommandHandler(
    IMapper mapper,
    IBaseDefaultRepository<User> userRepo
    ) : IRequestHandler<UpdateUserCommand, Result>
{
    public Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
