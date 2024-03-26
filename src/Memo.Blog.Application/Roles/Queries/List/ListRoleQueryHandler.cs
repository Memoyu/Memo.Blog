using Memo.Blog.Application.Roles.Common;
using Memo.Blog.Application.Roles.Queries.List;

namespace Memo.Blog.Application.Permissions.Queries.List;

public class ListRoleQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<Role> roleResp
    ) : IRequestHandler<ListRoleQuery, Result>
{
    public async Task<Result> Handle(ListRoleQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleResp.Select
                .WhereIf(!string.IsNullOrWhiteSpace(request.Name), p => p.Name.Contains(request.Name!))
                .ToListAsync(cancellationToken);

        var dtos = mapper.Map<List<RoleResult>>(roles);
        return Result.Success(dtos);
    }
}
