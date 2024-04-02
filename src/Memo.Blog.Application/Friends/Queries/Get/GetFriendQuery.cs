namespace Memo.Blog.Application.Friends.Queries.Get;

[Authorize(Permissions = ApiPermission.Friend.Get)]
public record GetFriendQuery(long FriendId) : IAuthorizeableRequest<Result>;


public class GetFriendQueryValidator : AbstractValidator<GetFriendQuery>
{
    public GetFriendQueryValidator()
    {
        RuleFor(x => x.FriendId)
            .Must(x => x > 0)
            .WithMessage("友链Id必须大于0");
    }
}

