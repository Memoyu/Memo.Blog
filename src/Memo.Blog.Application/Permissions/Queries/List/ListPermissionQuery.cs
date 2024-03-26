﻿namespace Memo.Blog.Application.Permissions.Queries.List;

[Authorize(Permissions = ApiPermission.Permission.List)]

public record ListPermissionQuery(
    string? Name
    ) : IRequest<Result>;

public class ListPermissionQueryValidator : AbstractValidator<ListPermissionQuery>
{
    public ListPermissionQueryValidator()
    {
    }
}


