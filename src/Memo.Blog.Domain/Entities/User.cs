namespace Memo.Blog.Domain.Entities;

public class User : BaseAuditEntity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    [Snowflake]
    [Description("业务Id")]
    public long UserId { get; set; }
}
