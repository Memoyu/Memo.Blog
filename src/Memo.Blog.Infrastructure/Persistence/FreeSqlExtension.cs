using System.Reflection;
using FreeSql;
using FreeSql.DataAnnotations;
using FreeSql.Internal;
using Memo.Blog.Application.Common.Interfaces.Persistence.Repositories;
using Memo.Blog.Application.Common.Utils;
using Memo.Blog.Domain.Common;
using Memo.Blog.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySql.Data.MySqlClient;

namespace Memo.Blog.Infrastructure.Persistence;

public static class FreeSqlExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
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

        // 属性配置
        fsql.Aop.ConfigEntityProperty += (s, e) =>
        {
            if (e.Property.PropertyType.IsEnum)
                e.ModifyResult.MapType = typeof(int);
        };

        fsql.Aop.AuditValue += (s, e) =>
        {
            if (e.Column.CsType == typeof(long) && e.Property.GetCustomAttribute<SnowflakeAttribute>(false) != null && e.Value?.ToString() == "0")
                e.Value = SnowFlakeUtil.NextId();
        };

        // fsql.GlobalFilter.Apply<IDeleteAduitEntity>("IsDeleted", a => a.IsDeleted == false); // 全局过滤字段

        // 注册FreeSql and UnitOfWorkManager
        services.AddSingleton(fsql);
        services.TryAddScoped<UnitOfWorkManager>();

        // 批量注册复合主键的 Repository
        services.TryAddScoped(typeof(IBaseDefaultRepository<>), typeof(BaseDefaultRepository<>));

        //在运行时直接生成表结构
        try
        {
            fsql.CodeFirst.SyncStructure(GetTypesByTableAttribute());
        }
        catch (Exception ex)
        {
            // TODO：异常处理
        }

        return services;
    }

    public static FreeSqlBuilder UseMysqlConnectionString(this FreeSqlBuilder builder, IConfiguration configuration, bool createDatabaseIfNotExists = false)
    {
        var connectionString = configuration.GetConnectionString("MySql") ?? string.Empty;
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, "数据库连接不能为空");

        builder.UseConnectionString(DataType.MySql, connectionString);

        if (createDatabaseIfNotExists)
            CreateDatabaseIfNotExistsMySql(connectionString);

        return builder;
    }

    private static void CreateDatabaseIfNotExistsMySql(string connectionString)
    {
        MySqlConnectionStringBuilder conStrBuilder = new MySqlConnectionStringBuilder(connectionString);
        string createDatabaseSql =
            $"USE mysql;CREATE DATABASE IF NOT EXISTS `{conStrBuilder.Database}` CHARACTER SET '{conStrBuilder.CharacterSet}' COLLATE 'utf8mb4_general_ci'";

        using MySqlConnection cnn = new MySqlConnection(
            $"Data Source={conStrBuilder.Server};Port={conStrBuilder.Port};User ID={conStrBuilder.UserID};Password={conStrBuilder.Password};Initial Catalog=mysql;Charset=utf8;SslMode=none;Max pool size=1");
        cnn.Open();
        using (MySqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandText = createDatabaseSql;
            cmd.ExecuteNonQuery();
        }
    }

    private static Type[] GetTypesByTableAttribute()
    {
        List<Type> tableAssembies = new List<Type>();
        var types = Assembly.GetAssembly(typeof(BaseAuditEntity))?.GetExportedTypes() ?? [];
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
