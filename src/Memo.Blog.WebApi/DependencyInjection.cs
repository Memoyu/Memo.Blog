using Memo.Blog.Application.Common.Interfaces;
using Memo.Blog.Application.Common.Models;
using Memo.Blog.Application.Common.Security;
using Memo.Blog.Domain.Constants;
using Memo.Blog.WebApi.Services;
using NSwag;
using NSwag.Generation.Processors.Security;
using ZymLabs.NSwag.FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 注册应用配置信息
        services.Configure<AppSettings>(configuration.GetSection(AppConst.AppSettingSection));

        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddScoped(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();
            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        // 跨域配置
        services.AddCors(options =>
        {
            var policyOrigins = configuration.GetValue<string>("CorsOrigins") ?? string.Empty;
            options.AddPolicy(AppConst.CorsPolicyName, builder =>
            {
                builder
                    .WithOrigins(policyOrigins.Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // 注册授权、认证
        services.AddAuthenticationAndAuthorization(configuration);

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // 注册API文档
        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "Memo.Blog API";

            // Add the fluent validations schema processor
            var fluentValidationSchemaProcessor =
                sp.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();
            configure.SchemaSettings.SchemaProcessors.Add(fluentValidationSchemaProcessor);

            // Add JWT
            configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: Bearer {your JWT token}."
            });

            configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        return services;
    }
}
