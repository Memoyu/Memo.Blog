namespace Memo.Blog.Application.Tokens.Queries.Generate;

public class GenerateTokenHandler(
    IBaseDefaultRepository<User> userRepo,
    IBaseDefaultRepository<UserIdentity> userIdentityRepo,
    IJwtTokenGenerator jwtTokenGenerator
    ) : IRequestHandler<GenerateTokenQuery, Result>
{
    public async Task<Result> Handle(GenerateTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepo.Where(u => u.Username.Equals(request.Username)).FirstAsync(cancellationToken);
        if (user is null)
            throw new ApplicationException("用户名或密码错误");

        var identity = await userIdentityRepo.Where(u => u.UserId == user.UserId).FirstAsync(cancellationToken);
        if (identity is null || !identity.Credential.Equals(EncryptUtil.Encrypt(request.Password)))
            throw new ApplicationException("用户名或密码错误");

        var token = jwtTokenGenerator.GenerateToken(user);

        return Result.Success(new GenerateTokenResult(user.UserId, user.Username, token.AccessToken, token.RefreshToken));
    }
}
