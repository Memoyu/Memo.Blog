namespace Memo.Blog.Application.Tokens.Queries.Generate;

public class GenerateTokenHandler(
    IBaseDefaultRepository<User> userResp,
    IBaseDefaultRepository<UserIdentity> userIdentityResp,
    IJwtTokenGenerator jwtTokenGenerator
    ) : IRequestHandler<GenerateTokenQuery, Result>
{
    public async Task<Result> Handle(GenerateTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await userResp.Where(u => u.Username.Equals(request.Username)).FirstAsync();
        if (user is null)
            return Result.Failure<GenerateTokenResult>("用户名或密码错误");

        var identity = await userIdentityResp.Where(u => u.UserId == user.UserId).FirstAsync();
        if (identity is null || !identity.Credential.Equals(EncryptUtil.Encrypt(request.Password)))
            return Result.Failure<GenerateTokenResult>("用户名或密码错误");

        var token = jwtTokenGenerator.GenerateToken(user);

        return Result.Success(new GenerateTokenResult(user.UserId, user.Username, token.AccessToken, token.RefreshToken));
    }
}
