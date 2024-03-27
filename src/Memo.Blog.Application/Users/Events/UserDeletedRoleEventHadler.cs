using Memo.Blog.Domain.Events.Users;

namespace Memo.Blog.Application.Users.Events;

public class UserDeletedEventHadler(IBaseDefaultRepository<User> userRepo) : INotificationHandler<UserDeletedEvent>
{
    public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
