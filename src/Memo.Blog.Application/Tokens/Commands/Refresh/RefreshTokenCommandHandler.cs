using EasyCaching.Core;
using Microsoft.Extensions.Logging;

namespace Memo.Blog.Application.Tokens.Commands.Refresh;

public class RefreshTokenCommandHandler(
    ILogger<RefreshTokenCommandHandler> logger,
    IJwtTokenGenerator jwtTokenGenerator,
    IEasyCachingProvider ecProvider,
    IBaseDefaultRepository<User> userRepo
    ) : IRequestHandler<RefreshTokenCommand, Result>
{
    public async Task<Result> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = await ecProvider.GetAsync<long>(CacheKeyConst.UserRefreshToken(request.RefreshToken), cancellationToken);

            if (!userId.HasValue)
                throw new ArgumentException("RefreshToken不存在或已过期");

            var user = await userRepo.Where(u => u.UserId == userId.Value).FirstAsync(cancellationToken) ??
                throw new ApplicationException($"{userId.Value}用户不存在");

            var token = await jwtTokenGenerator.RefreshTokenAsync(user, cancellationToken);

            return Result.Success(new GenerateTokenResult(user.UserId, user.Username, token.AccessToken, token.RefreshToken, token.ExpiredAt));
        }
        catch(Exception ex)
        {
            logger.LogError(ex, $"刷新token异常：{ex.Message}");
            return Result.Failure("请重新登录", ResultCode.AuthenticationFailure);
        }
    }
}
