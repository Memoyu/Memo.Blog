namespace Memo.Blog.Application.Users.Queries.Get;

public  record GetUserQuery(long? UserId) : IAuthorizeableRequest<Result>;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
}
