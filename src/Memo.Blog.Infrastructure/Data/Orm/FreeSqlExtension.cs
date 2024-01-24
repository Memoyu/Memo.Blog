using FreeSql;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Memo.Blog.Infrastructure.Data.Orm;

public static class FreeSqlExtension
{
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

}
