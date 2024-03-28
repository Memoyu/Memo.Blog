using Memo.Blog.Application.Common.Extensions;
using Memo.Blog.Application.Users.Common;

namespace Memo.Blog.Application.Users.Queries.Page;

public class PageUserQueryHandler(
    IMapper mapper,
    IBaseDefaultRepository<User> userRepo
    ) : IRequestHandler<PageUserQuery, Result>
{
    public async Task<Result> Handle(PageUserQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepo.Select
            .WhereIf(request.UserId.HasValue, u => u.UserId == request.UserId)
            .WhereIf(!string.IsNullOrWhiteSpace(request.Username), u => u.Username.Contains(request.Username!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Nickname), u => u.Nickname.Contains(request.Nickname!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.Email), u => u.Email.Contains(request.Email!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.PhoneNumber), u => u.PhoneNumber.Contains(request.PhoneNumber!))
            .ToPageListAsync(request, out var total, cancellationToken);

        var dtos = mapper.Map<List<PageUserResult>>(users);
        return Result.Success(new PaginationResult<PageUserResult>(dtos, total));
    }
}
