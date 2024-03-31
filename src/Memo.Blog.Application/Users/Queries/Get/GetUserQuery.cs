namespace Memo.Blog.Application.Users.Queries.Get;

public  record GetUserQuery(long? UserId) : IRequest<Result>;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
}
