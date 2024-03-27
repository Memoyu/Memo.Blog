using Memo.Blog.Domain.Events.Roles;

namespace Memo.Blog.Application.Roles.Commands.Delete;

public class DeleteRoleCommandHandler(IBaseDefaultRepository<Role> roleResp) : IRequestHandler<DeleteRoleCommand, Result>
{
    public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await roleResp.Select.Where(t => t.RoleId == request.RoleId).FirstAsync(cancellationToken) ?? throw new ApplicationException("标签不存在");

        role.AddDomainEvent(new RoleDeleteEvent(request.RoleId));

        var rows = await roleResp.DeleteAsync(role, cancellationToken);

        return rows > 0 ? Result.Success() : throw new ApplicationException("删除失败");
    }
}
