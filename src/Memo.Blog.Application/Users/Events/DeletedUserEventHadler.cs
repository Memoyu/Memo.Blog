using Memo.Blog.Domain.Events.Users;

namespace Memo.Blog.Application.Users.Events;

public class DeletedUserEventHadler(IBaseDefaultRepository<User> userRepo) : INotificationHandler<DeletedUserEvent>
{
    public Task Handle(DeletedUserEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
