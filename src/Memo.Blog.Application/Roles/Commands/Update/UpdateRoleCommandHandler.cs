namespace Memo.Blog.Application.Roles.Commands.Update;

public class UpdateRoleCommandHandler(
    IBaseDefaultRepository<Role> roleRepo
    ) : IRequestHandler<UpdateRoleCommand, Result>
{
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
}
