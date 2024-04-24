﻿namespace Memo.Blog.Application.Comments.Common;

public record CommentClientResult
{
    /// <summary>
    /// 父评论Id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 评论Id
    /// </summary>
    public long CommentId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    ///  头像url
    /// </summary>
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// 评论IP所属
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 楼层
    /// </summary>
    public int Floor { get; set; }

    /// <summary>
    /// 楼层
    /// </summary>
    public string FloorString { get; set; } = string.Empty;

    /// <summary>
    /// 回复的评论
    /// </summary>
    public CommentClientResult? Reply { get; set; }

    /// <summary>
    /// 子评论
    /// </summary>
    public List<CommentClientResult> Childs { get; set; } = [];
}