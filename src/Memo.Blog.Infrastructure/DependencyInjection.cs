using FreeSql.Internal;
using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Memo.Blog.Domain.Common;
using FreeSql.DataAnnotations;
using Memo.Blog.Infrastructure.Data.Orm;
using Yitter.IdGenerator;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddFreeSql(services, configuration);

        return services;
    }

    private static IServiceCollection AddFreeSql(IServiceCollection services, IConfiguration configuration)
    {
        IFreeSql fsql = new FreeSqlBuilder()
          .UseMysqlConnectionString(configuration, true)
          .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
          .UseAutoSyncStructure(true)
          .UseNoneCommandParameter(true)
          .UseMonitorCommand(cmd =>
          {
              // Trace.WriteLine(cmd.CommandText + ";");
          })
          .Build()
          .SetDbContextOptions(opt =>
          {
              opt.EnableCascadeSave = true;
          });//联级保存功能开启（默认为关闭）

        fsql.Aop.CurdAfter += (s, e) =>
        {
            if (e.ElapsedMilliseconds > 200)
            {
                //记录日志
                //发送短信给负责人
            }
        };

        fsql.Aop.AuditValue += (s, e) =>
        {
            if (e.Column.CsType == typeof(long) && e.Property.GetCustomAttribute<SnowflakeAttribute>(false) != null && e.Value?.ToString() == "0")
                e.Value = YitIdHelper.NextId();
        };

        // fsql.GlobalFilter.Apply<IDeleteAduitEntity>("IsDeleted", a => a.IsDeleted == false); // 全局过滤字段

        services.AddSingleton(fsql);
        services.TryAddScoped<UnitOfWorkManager>();

        //在运行时直接生成表结构
        try
        {
            fsql.CodeFirst.SyncStructure(GetTypesByTableAttribute());
        }
        catch (Exception ex)
        {
            // 异常处理
        }

        return services;
    }

    private static Type[] GetTypesByTableAttribute()
    {
        List<Type> tableAssembies = new List<Type>();
        var types = Assembly.GetAssembly(typeof(BaseEntity))?.GetExportedTypes() ?? [];
        foreach (Type type in types)
        {
            foreach (Attribute attribute in type.GetCustomAttributes())
            {
                if (attribute is TableAttribute tableAttribute)
                {
                    if (tableAttribute.DisableSyncStructure == false)
                    {
                        tableAssembies.Add(type);
                    }
                }
            }
        };
        return tableAssembies.ToArray();
    }
}
