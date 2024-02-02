using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Memo.Blog.Infrastructure;

/// <summary>
/// Infrastructure 依赖注入
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthorization() // 注册认证
            .AddAuthentication(configuration) // 注册授权
            .AddPersistence(configuration); // 注册持久化组件（FreeSql）

        return services;
    }

    private static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        // services.AddSingleton<IPolicyEnforcer, PolicyEnforcer>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Section));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services
            .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>//配置JWT
            {
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        //Token 过期
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    },
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        string message;
                        ResultCode code;

                        if (context.Error == "invalid_token"
                            && !string.IsNullOrWhiteSpace(context.ErrorDescription)
                            && context.ErrorDescription.StartsWith("The token expired at"))//Token过期
                        {
                            message = "令牌过期";
                            code = ResultCode.TokenExpired;
                        }
                        else if (context.Error == "invalid_token"
                            && context.ErrorDescription.IsNullOrEmpty())//Token失效
                        {
                            message = "令牌失效";
                            code = ResultCode.TokenInvalidation;
                        }
                        else
                        {
                            message = "认证失败，请先登录！";
                            code = ResultCode.AuthenticationFailure;
                        }

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(Result.Failure(message, code).ToString()!);
                    }
                };
            });

        return services;
    }
}
