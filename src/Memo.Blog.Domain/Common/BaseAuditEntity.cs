using System.ComponentModel;

namespace Memo.Blog.Domain.Common;

[Index("index_on_sid", nameof(Sid), false)]
public class BaseEntity
{
    /// <summary>
    /// 自增主键Id
    /// </summary>
    [Column(IsPrimary = true, IsIdentity = true, Position = 1)]
    [Description("主键Id")]
    public long Id { get; set; }

    /// <summary>
    /// 业务Id(雪花Id)
    /// </summary>
    [Column(Position = 2)]
    [Description("业务Id")]
    [Snowflake]
    public long Sid { get; set; }
}

public class BaseAuditEntity : BaseEntity
{
    /// <summary>
    /// 创建人Sid
    /// </summary>
    [Column(Position = -7)]
    [Description("创建人Sid")]
    public long CreateUserSid { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Column(Position = -6)]
    [Description("创建时间")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    [Column(Position = -5)]
    [Description("是否删除 0 未删除，1 已删除")]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 修改人Sid
    /// </summary>
    [Column(Position = -4)]
    [Description("修改人Sid")]
    public long? UpdateUserSid { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [Column(Position = -3)]
    [Description("修改时间")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 删除人Sid
    /// </summary>
    [Column(Position = -2)]
    [Description("删除人Sid")]
    public long? DeleteUserSid { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [Column(Position = -1)]
    [Description("删除时间")]
    public DateTime? DeleteTime { get; set; }

}
