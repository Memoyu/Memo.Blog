using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Messages.Queries.Page;

public class PageMessageQueryHandler(
    IMapper mapper,
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<Message> messageRepo,
    IBaseDefaultRepository<MessageUser> messageUserRepo
    ) : IRequestHandler<PageMessageQuery, Result>
{
    public async Task<Result> Handle(PageMessageQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserProvider.GetCurrentUser().Id;

        var userMessages = await messageUserRepo.Select
            .Include(m => m.Message)
            .Where(m => m.UserId == userId && m.MessageType == request.Type)
            .OrderByDescending(a => a.CreateTime)
            .ToPageListAsync(request, out var total, cancellationToken);

        var dtos = mapper.Map<List<MessageResult>>(userMessages.Select(um => um.Message).ToList());

        return Result.Success(new PaginationResult<MessageResult>(dtos, total));

    }
}

