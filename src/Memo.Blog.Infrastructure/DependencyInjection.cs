using FreeSql.Internal;
using FreeSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Memo.Blog.Infrastructure;

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
          .UseConnectionString(DataType.MySql, "")
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
              opt.OnEntityChange = rep =>
              {
                  //进行审计
              };
          });//联级保存功能开启（默认为关闭）

        fsql.Aop.CurdAfter += (s, e) =>
        {
            if (e.ElapsedMilliseconds > 200)
            {
                //记录日志
                //发送短信给负责人
            }
        };

        // fsql.GlobalFilter.Apply<IDeleteAduitEntity>("IsDeleted", a => a.IsDeleted == false); // 全局过滤字段

        services.AddSingleton<IFreeSql>(fsql);
        services.TryAddScoped< UnitOfWorkManager>();

        //在运行时直接生成表结构
        try
        {
            fsql.CodeFirst.SyncStructure();
        }
        catch (Exception e)
        {
            // 异常处理
        }

        return services;
    }
}
