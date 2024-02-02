namespace Memo.Blog.Application.Tokens.Queries.Generate;

public record GenerateTokenQuery(string Username, string Password) : IRequest<Result<GenerateTokenResult>>;

public class GenerateTokenHandler(
    IBaseDefaultRepository<User> _userResp,
    IBaseDefaultRepository<UserIdentity> _userIdentityResp,
    IJwtTokenGenerator _jwtTokenGenerator
    ) : IRequestHandler<GenerateTokenQuery, Result<GenerateTokenResult>>
{
    public async Task<Result<GenerateTokenResult>> Handle(GenerateTokenQuery query, CancellationToken cancellationToken)
    {
        var user = await _userResp.Where(u => u.Username.Equals(query.Username)).FirstAsync();
        if (user is null)
            return Result.Failure<GenerateTokenResult>("用户名或密码错误");

        var identity = await _userIdentityResp.Where(u => u.UserId == user.UserId).FirstAsync() ;
        if (identity is null)
            return Result.Failure<GenerateTokenResult>("用户名或密码错误");



        var token = _jwtTokenGenerator.GenerateToken(user);

        return Result.Success(new GenerateTokenResult(user.UserId, user.Username, token.AccessToken, token.RefreshToken));
    }
}

