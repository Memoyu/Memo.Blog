using Mapster;
using MapsterMapper;
using Memo.Blog.Application.Common.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Memo.Blog.Application;

/// <summary>
/// Application 依赖注入
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 配置雪花ID生成
        SnowFlakeUtil.Init();

        services.Configure<AppSettings>(configuration.GetSection(AppConst.AppSettingSection));

        // 注册Validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // 注册消息组件
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        // 注册实体映射组件
        services.AddMapper();

        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        // 获取全局映射配置
        var config = TypeAdapterConfig.GlobalSettings;

        // 扫描 IRegister 接口的对象映射配置
        config.Scan(Assembly.GetExecutingAssembly());

        // 配置默认全局映射（支持覆盖）
        config.Default
              .NameMatchingStrategy(NameMatchingStrategy.Flexible)
              .PreserveReference(true);

        // 配置默认全局映射（忽略大小写敏感）
        config.Default
              .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
              .PreserveReference(true);

        // 配置支持依赖注入
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
