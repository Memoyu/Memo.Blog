using NSwag;
using NSwag.Generation.Processors.Security;

namespace Memo.Blog.WebApi;

/// <summary>
/// Web APA 依赖注入
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// 接口服务依赖注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        // 跨域配置
        services.AddCorsPolicy(configuration);

        services.AddControllers(options =>
            // 禁用隐式的[Required]，为了统一响应模型
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
        );
        services.AddEndpointsApiExplorer();

        // Swagger 接口文档
        services.AddOpenApiDoc();

        return services;
    }

    private static IServiceCollection AddOpenApiDoc(this IServiceCollection services)
    {
        // 注册API文档
        services.AddOpenApiDocument((configure, sp) =>
        {
            configure.Title = "Memo.Blog API";

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

    private static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
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

        return services;
    }
}
