namespace Memo.Blog.Application.Roles.Queries.List;

[Authorize(Permissions = ApiPermission.Role.List)]

public record ListRoleQuery(
    string? Name
    ) : IRequest<Result>;

public class ListRoleQueryValidator : AbstractValidator<ListRoleQuery>
{
    public ListRoleQueryValidator()
    {
    }
}


