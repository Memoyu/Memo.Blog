﻿using Memo.Blog.Application.Comments.Common;
using Memo.Blog.Application.Common.Interfaces.Region;

namespace Memo.Blog.Application.Common.Mappings;

public class CommentRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Comment, PageCommentResult>()
            .Map(d => d.Belong, s => GetCommentBelog(s))
            .Map(d => d.Region, s => GetRegionFormat(s.Region));

        config.ForType<Comment, CommentResult>()
            .Map(d => d.Belong, s => GetCommentBelog(s))
            .Map(d => d.Region, s => GetRegionFormat(s.Region));

        config.ForType<Comment, CommentClientResult>()
            .Map(d => d.Region, s => GetRegionFormat(s.Region));
    }

    private string GetRegionFormat(string region)
    {
        var searcher = MapContext.Current.GetService<IRegionSearcher>() ?? throw new Exception("未注册IRegionSearcher服务");
        var regionIfon = searcher.ToRegionInfo(region);
        return regionIfon.GetRegion() ?? string.Empty;
    }

    private CommentBelongResult GetCommentBelog(Comment s)
    {
        var belong = new CommentBelongResult();
        switch (s.CommentType)
        {
            case Domain.Enums.CommentType.Article:
                belong = MapContext.Current.GetService<IBaseDefaultRepository<Article>>().Select
                    .Where(c => c.ArticleId == s.BelongId)
                    .First(a => new CommentBelongResult { BelongId = s.BelongId, Title = a.Title, Link = "" });
                break;
            case Domain.Enums.CommentType.Moment:
                belong = new CommentBelongResult { BelongId = s.BelongId, Title = "动态", Link = "" };
                break;
            case Domain.Enums.CommentType.About:
                belong = new CommentBelongResult { BelongId = s.BelongId, Title = "关于", Link = "" };
                break;
        }

        return belong; 
    }
}
