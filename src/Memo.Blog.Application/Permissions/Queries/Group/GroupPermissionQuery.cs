namespace Memo.Blog.Application.Permissions.Queries.Group;

[Authorize(Permissions = ApiPermission.Permission.Group)]
public record GroupPermissionQuery(
    string? Name
    ) : IRequest<Result>;

public class GroupPermissionQueryValidator : AbstractValidator<GroupPermissionQuery>
{
    public GroupPermissionQueryValidator()
    {
    }
}


