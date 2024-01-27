using System.Text;
using Memo.Blog.Domain.Enums;
using Memo.Blog.Domain.Primitives;
using Memo.Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Memo.Blog.Application.Common.Security;

public static class JwtExtension
{
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetRequiredSection("JwtOptions").Get<JwtOptions>() ?? throw new ArgumentNullException("JwtOptions is not null");

        services.AddAuthentication(opts =>//认证方式
        {
            opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddCookie(options =>//Cookie
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.IsEssential = true;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>//配置JWT
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

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
    }
}
