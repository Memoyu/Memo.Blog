using Memo.Blog.Application.Messages.Common;
using Memo.Blog.Application.Security;

namespace Memo.Blog.Application.Messages.Queries.Get;

public class GetUnreadMessageNumberQueryHandler(
    IMapper mapper,
    ICurrentUserProvider currentUserProvider,
    IBaseDefaultRepository<MessageUser> messageUserRepo
    ) : IRequestHandler<GetUnreadMessageNumberQuery, Result>
{
    public async Task<Result> Handle(GetUnreadMessageNumberQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserProvider.GetCurrentUser().Id;
        if (userId < 1) throw new ApplicationException("当前用户Id为空");

        var unreads = await messageUserRepo.Select
            .Where(m => m.UserId == userId && !m.IsRead)
            .ToListAsync(cancellationToken);

        return Result.Success(new UnreadMessageNumberResult
        {
            Total = unreads.GroupBy(u => u.MessageId).Count(),
            User = unreads.Where(u => u.MessageType == MessageType.User).Count(),
            Comment = unreads.Where(u => u.MessageType == MessageType.Comment).Count(),
            Like = unreads.Where(u => u.MessageType == MessageType.Like).Count(),
        });
    }
}
