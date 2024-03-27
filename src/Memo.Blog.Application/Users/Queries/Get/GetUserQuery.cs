namespace Memo.Blog.Application.Users.Queries.Get;

public  record GetUserQuery(long UserId) : IRequest<Result>;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.UserId)
            .Must(x => x > 0)
            .WithMessage("用户Id必须大于0");
    }
}
